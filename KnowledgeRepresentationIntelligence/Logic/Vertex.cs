using KnowledgeRepresentationReasoning.World;
using System.Collections.Generic;
using System.Linq;

namespace KnowledgeRepresentationReasoning.Logic
{
    public class Vertex
    {
        public State State { get; private set; }
        public WorldAction ActualWorldAction { get; set; }
        public int Time { get; private set; }
        private Vertex Root { get; set; }
        public bool IsPossible { get; set; }
        public bool IsEnded { get; set; }

        public List<WorldAction> NextActions { get; set; }

        public Vertex(State state, WorldAction worldAction, int time, Vertex root)
        {
            State = state;
            this.ActualWorldAction = worldAction;
            Time = time;
            Root = root;

            Initialize();
        }

        public Vertex(Vertex leaf)
        {
            Initialize();

            State = (State)leaf.State.Clone();
            ActualWorldAction = (WorldAction)leaf.ActualWorldAction.Clone();
            Time = leaf.Time;
            Root = leaf.Root;
            IsPossible = leaf.IsPossible;
            IsEnded = leaf.IsEnded;
            NextActions = leaf.NextActions.ToList();
        }

        public Vertex()
        {
            Initialize();
        }

        private void Initialize()
        {
            IsPossible = true;
            IsEnded = false;
            NextActions = new List<WorldAction>();
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

        public List<Vertex> CreateChildsBasedOnImplications(List<Implication> implications, World.WorldAction nextAction, int nextTime)
        {
            List<Vertex> childs = new List<Vertex>();

            if (ActualWorldAction.GetEndTime() <= nextTime)
            {
                ActualWorldAction = null;
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
                child.State = implication.FutureState;

                if (nextAction != null)
                {
                    if (nextTime == nextAction.StartAt)
                    {
                        this.ActualWorldAction = (WorldAction)nextAction.Clone();
                    }
                    this.IsPossible = false;
                    return childs;
                }

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

        public bool CheckIsFinished(int implicationCounter, WorldAction nextAction)
        {
            return implicationCounter == 0 
                    && nextAction == null 
                    && NextActions.Count == 0 
                    && ActualWorldAction.GetEndTime() == Time;
        }
    }
}
