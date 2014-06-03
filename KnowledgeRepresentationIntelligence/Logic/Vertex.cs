using System.Diagnostics;
using KnowledgeRepresentationReasoning.Queries;
using KnowledgeRepresentationReasoning.Scenario;
using KnowledgeRepresentationReasoning.World;
using log4net;
using System.Collections.Generic;
using System.Linq;

namespace KnowledgeRepresentationReasoning.Logic
{
    public class Vertex
    {
        public State ActualState { get; private set; }
        public WorldAction ActualWorldAction { get; set; }
        public int Time { get;  set; }
        private Vertex Root { get; set; }
        public bool IsPossible { get; set; }
        public SortedDictionary<int, WorldAction> NextActions { get; set; }
        private ILog _logger;

        public Vertex()
        {
            IsPossible = true;
            NextActions = new SortedDictionary<int, WorldAction>();
            _logger = Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<ILog>();
        }

        public Vertex(State state, WorldAction worldAction, int time, Vertex root)
        {
            ActualState = state;
            ActualWorldAction = worldAction;
            Time = time;
            Root = root;
        }

        public void AddScenarioActions(IEnumerable<WorldAction> actions)
        {
            foreach (var worldAction in actions)
            {
                Debug.Assert(worldAction.StartAt != null, "worldAction.StartAt != null");
                NextActions.Add((int)worldAction.StartAt, worldAction);
            }
        }

        public List<Vertex> CreateChildsBasedOnImplications(List<Implication> implications)
        {
            var result = new List<Vertex>();

            foreach (var implication in implications)
            {
                
            }

            return result;
        }

        internal List<Vertex> GenerateChildsForLeaf(WorldDescription worldDescription, ScenarioDescription scenarioDescription, int inf)
        {
            // implications are supposed to be not empty and not null !!
            var implications = worldDescription.GetImplications(this);

            // now we add triggeredActions to next actions 
            var triggeredActions = implications.First().TriggeredActions;

            foreach (var triggeredAction in triggeredActions)
            {
                Debug.Assert(triggeredAction.StartAt != null, "triggeredAction.StartAt != null");
                NextActions.Add((int)triggeredAction.StartAt, triggeredAction);
            }

            var vertices = CreateChildsBasedOnImplications(implications);

            foreach (var vertex in vertices)
            {
                if (!Validate(vertex, worldDescription, scenarioDescription))
                    vertex.IsPossible = false;
 
            }

            return vertices;
        }

        private bool Validate(Vertex vertex, WorldDescription worldDescription, ScenarioDescription scenarioDescription)
        {
            var observationsValidationResult = CheckObservations(vertex, scenarioDescription);
            var actionsValidationResult = CheckActions(vertex, scenarioDescription);
            var worldDescriptionValidationResult = worldDescription.Validate(vertex);
            var nextActionsValidationResult = ValidateNextActions();
            return observationsValidationResult && actionsValidationResult && worldDescriptionValidationResult && nextActionsValidationResult;
        }

        private bool CheckObservations(Vertex vertex, ScenarioDescription scenarioDescription)
        {
            int fromTime = vertex.Time;
            int toTime = vertex.Time;
            
            if (vertex.ActualWorldAction != null)
            {
                toTime += vertex.ActualWorldAction.Duration ?? 0;
            }
            
            var observations = scenarioDescription.Observations.Where(t => t.Time >= fromTime && t.Time < toTime);
            
            return observations.All(observation => observation.CheckState(vertex.ActualState));
        }

        private bool CheckActions(Vertex vertex, ScenarioDescription scenarioDescription)
        {
            int fromTime = vertex.Time;
            int toTime = vertex.Time;

            if (vertex.ActualWorldAction != null)
            {
                toTime += vertex.ActualWorldAction.Duration ?? 0;
            }

            var actions = scenarioDescription.Actions.Where(t => t.Time >= fromTime && t.Time < toTime);

            // if any actions exists while running action from vertex there is a collision and scenario is wrong
            return !actions.Any();
        }

        private bool ValidateNextActions()
        {
            bool result = true;

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
    }
}
