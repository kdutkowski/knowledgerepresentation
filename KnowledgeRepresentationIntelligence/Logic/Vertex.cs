using KnowledgeRepresentationReasoning.Queries;
using KnowledgeRepresentationReasoning.Scenario;
using KnowledgeRepresentationReasoning.World;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KnowledgeRepresentationReasoning.Logic
{
    public class Vertex
    {
        public State ActualState { get; private set; }
        public WorldAction ActualWorldAction { get; set; }
        public int Time { get;  set; }
        public List<WorldAction> NextActions { get; set; }
        
        private Vertex Root { get; set; }
        public bool IsPossible { get; set; }
        public bool IsActive { get; set; }

        private ILog _logger;

        private static Query _query;

        public void SetQuery(Query query)
        {
            _query = query;
        }

        public Vertex(State state, WorldAction worldAction, int time, Vertex root)
        {
            ActualState = state;
            this.ActualWorldAction = worldAction;
            Time = time;
            Root = root;

            Initialize();
        }

        public Vertex(Vertex leaf)
        {
            Initialize();

            ActualState = (State)leaf.ActualState.Clone();
            if (leaf.ActualWorldAction == null)
            {
                ActualWorldAction = null;
            }
            else
            {
                ActualWorldAction = (WorldAction)leaf.ActualWorldAction.Clone();
            }
            Time = leaf.Time;
            Root = leaf.Root;
            IsPossible = leaf.IsPossible;
            NextActions = leaf.NextActions.ToList();
        }

        public Vertex()
        {
            Initialize();
        }

        private void Initialize()
        {
            IsPossible = true;
            IsActive = true;
            NextActions = new List<WorldAction>();

            _logger = Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<ILog>();
        }

        public int GetNextActionTime()
        {
            int nextTimeAction = -1;

            if (NextActions.Count > 0)
            {
                int? nextTime = NextActions.Min(x => x.StartAt);
                if (nextTime.HasValue)
                {
                    nextTimeAction = nextTime.Value;
                }
            }

            return nextTimeAction;
        }

        public List<Vertex> CreateChildsBasedOnImplications(List<Implication> implications, World.WorldAction nextActionFromScenario, int nextTime)
        {
            List<Vertex> childs = new List<Vertex>();

            if (ActualWorldAction != null)
            {
                if (ActualWorldAction.GetEndTime() < nextTime)
                {
                    return GetImpossibleChilds();
                }

                if (ActualWorldAction.GetEndTime() == nextTime)
                {
                    ActualWorldAction = null;
                }
            }

            WorldAction leafAction = ActualWorldAction;
            if (NextActions.Count > 0)
            {
                SortActionsByStartTime(NextActions);
                if (NextActions[0].StartAt < nextTime)
                {
                    leafAction = NextActions[0];
                }
            }

            foreach (var implication in implications)
            {
                Vertex child = new Vertex(this);
                child.Root = this;

                child.ActualState = implication.FutureState;

                WorldAction actualAction = nextActionFromScenario;
                if (actualAction != null)
                {
                    actualAction.StartAt = nextTime;
                }

                if (implication.TriggeredActions.Count > 0)
                {
                    WorldAction triggeredAction = implication.TriggeredActions[0];
                    foreach (var action in implication.TriggeredActions)
                    {
                        if (triggeredAction.StartAt > action.StartAt)
                        {
                            triggeredAction = (WorldAction)action.Clone();
                        }
                    }

                    if (nextActionFromScenario == null)
                    {
                        if (triggeredAction != null)
                        {
                            actualAction = triggeredAction;
                        }
                    }
                    else
                    {
                        if (triggeredAction != null && nextActionFromScenario.StartAt == triggeredAction.StartAt)
                        {
                            this.IsPossible = false;
                            return childs;
                        }
                    }
                }
                child.ActualWorldAction = actualAction;

                child.NextActions = new List<WorldAction>();
                child.NextActions.AddRange(implication.TriggeredActions);

                child.Time = nextTime;
                child.IsPossible = child.ValidateActions();

                childs.Add(child);
            }
            return childs;
        }

        public bool ValidateActions()
        {
            bool result = true;
            SortActionsByStartTime(NextActions);

            for (int i = 0; i < NextActions.Count; ++i)
            {
                WorldAction nextAction = NextActions[i];

                if (nextAction.GetEndTime() != nextAction.StartAt + nextAction.Duration)
                {
                    result = false;
                    break;
                }

                if (ActualWorldAction != null && nextAction.StartAt < ActualWorldAction.GetEndTime())
                {
                    result = false;
                    break;
                }

                if (i < NextActions.Count - 1)
                {
                    if (NextActions[i + 1].StartAt.HasValue && NextActions[i + 1].StartAt < nextAction.GetEndTime())
                    {
                        result = false;
                        break;
                    }
                }
            }
            return result;
        }

        private void SortActionsByStartTime(List<World.WorldAction> NextActions)
        {
            NextActions.Sort(new WorldActionTimeComparer());
        }

        private class WorldActionTimeComparer : IComparer<WorldAction>
        {
            public int Compare(WorldAction a, WorldAction b)
            {
                if (a.StartAt == b.StartAt) return 0;
                if (a.StartAt > b.StartAt) return 1;
                else return -1;
            }
        }

        internal List<Vertex> GenerateChildsForLeaf(WorldDescription worldDescription, ScenarioDescription scenarioDescription, int TInf, out QueryResult result)
        {
            result = QueryResult.Undefined;

            List<Vertex> vertices = new List<Vertex>();

            int nextTime = GetNextTimestamp(scenarioDescription, TInf);

            if (nextTime > _query.Time)
            {
                result = _query.CheckCondition(this, _query.Time);
            }

            // TODO Run  .Validate(vertex)
            var implications = worldDescription.GetImplications(this);

            // TODO: Implement validation including: WorldDescription.Validate(), CheckNearestObservations() and Check triggered actions...
            // It will be big function Validate(); it should also check if actions from scenario are valid

            if (!CheckNearestObservations(scenarioDescription, nextTime))
                return GetImpossibleChilds();

            WorldAction nextAction = scenarioDescription.GetActionAtTime(nextTime);

            vertices = CreateChildsBasedOnImplications(implications, nextAction, nextTime);

            return vertices;
        }

        private bool CheckNearestObservations(ScenarioDescription scenarioDescription, int nextTime)
        {
            int actualTime = Time;
            while (actualTime <= nextTime)
            {
                int nextObservationTime = scenarioDescription.GetNextObservationTime(actualTime);
                if (nextObservationTime == -1)
                {
                    return true;
                }
                if (!CheckNearestObservation(scenarioDescription, actualTime, nextObservationTime, nextTime))
                    return false;

                actualTime = nextObservationTime + 1;
            }

            return true;
        }

        private bool CheckNearestObservation(ScenarioDescription scenarioDescription, int actualTime, int nextObservationTime, int nextTime)
        {
            if (actualTime <= nextObservationTime && nextObservationTime < nextTime)
            {
                ScenarioObservationRecord nextObservation = scenarioDescription.GetObservationFromTime(nextObservationTime);
                if (!nextObservation.CheckState(ActualState, actualTime))
                {
                    _logger.Warn("Leaf is incopatibile with observation!\n" +
                                    "State: " + ActualState.ToString() +
                                    "Observation: " + nextObservation);
                    return false;
                }
            }
            return true;
        }

        private List<Vertex> GetImpossibleChilds()
        {
            Vertex child = new Vertex();
            child.IsPossible = false;
            List<Vertex> impossibleChilds = new List<Vertex>() { child };
            return impossibleChilds;
        }

        private int GetNextTimestamp(ScenarioDescription scenarioDescription, int TInf)
        {
            int actualActionEndTime = int.MaxValue;

            if (ActualWorldAction != null)
            {
                actualActionEndTime = ActualWorldAction.GetEndTime() < 0 ? int.MaxValue : ActualWorldAction.GetEndTime();
            }
            int nextActionStartTime = GetNextActionTime() < 0 ? int.MaxValue : GetNextActionTime();

            int nextTimeForAction = Time == 0 ? 0 : Time + 1;
            int nextActionTime = scenarioDescription.GetNextActionTime(nextTimeForAction);

            nextActionTime = nextActionTime < 0 ? int.MaxValue : nextActionTime;

            int min = Math.Min(nextActionTime, Math.Min(actualActionEndTime, nextActionStartTime));
            min = min > TInf ? TInf : min;
            
            return min;
        }
    }
}
