using KnowledgeRepresentationReasoning.Expressions;
using KnowledgeRepresentationReasoning.Scenario;
using KnowledgeRepresentationReasoning.World;
using log4net;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("KnowledgeRepresentationReasoning.Test")]
namespace KnowledgeRepresentationReasoning.Logic
{
    internal class Tree : ITree
    {
        ILogicExpression _logicExpression;
        private ILog _logger;

        public List<Vertex> LastLevel { get; private set; }
        private List<Vertex> _oldVertices;

        private int _TInf;

        public Tree(int timeInf)
        {
            _logger = Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<ILog>();
            _logicExpression = new SimpleLogicExpression();

            LastLevel = new List<Vertex>();
            _oldVertices = new List<Vertex>();

            _TInf = timeInf;
        }

        public bool AddFirstLevel(World.WorldDescription WorldDescription, Scenario.ScenarioDescription ScenarioDescription, out int numberOfImpossibleLeaf)
        {
            numberOfImpossibleLeaf = 0;
            int startTime = 0;

            //states
            List<string> fluentNames = WorldDescription.GetFluentNames().ToList<string>();
            List<State> states = CreateStatesBasedOnObservations(fluentNames, ScenarioDescription, out startTime);

            if (states.Count == 0)
            {
                return false;
            }

            foreach (var state in states)
            {
                Vertex newVertex = new Vertex(state, null, startTime, null);
                LastLevel.Add(newVertex);
            }

            //actions
            for (int i = 0; i < LastLevel.Count; ++i)
            {
                LastLevel[i].NextActions.AddRange(
                    ScenarioDescription.actions.Select(item => (WorldAction)item.WorldAction.Clone()).ToList()
                    );

                if (!WorldDescription.Validate(LastLevel[i]))
                {
                    ++numberOfImpossibleLeaf;
                }
            }

            return startTime > -1;
        }

        private List<State> CreateStatesBasedOnObservations(List<string> fluentNames, ScenarioDescription scenarioDescription, out int startTime)
        {
            List<State> states = new List<State>();

            startTime = scenarioDescription.GetNextObservationTime(0);
            if (startTime == -1 || startTime > _TInf)
            {
                return states;
            }

            ScenarioObservationRecord observation = scenarioDescription.GetObservationFromTime(startTime);
            if (observation == null)
            {
                _logger.Warn("Scenario has no observations!");
                State state = new State();
                state.AddFluents(fluentNames);
                states.Add(state);
            }
            else
            {
                _logicExpression = new SimpleLogicExpression(observation.Expr as SimpleLogicExpression);

                List<Fluent[]> possibleInitialValues = _logicExpression.CalculatePossibleFluents();
                foreach (var valuation in possibleInitialValues)
                {
                    State state = new State();
                    state.AddFluents(fluentNames);
                    foreach (var fluent in valuation)
                    {
                        try
                        {
                            state.Fluents.First(f => f.Name == fluent.Name).Value = fluent.Value;
                        }
                        catch (System.ArgumentNullException)
                        {
                            _logger.Error("Fluent " + fluent.Name + " doesn't exist!");
                        }

                    }
                    states.Add(state);
                }
            }

            return states;
        }

        internal void SaveLastLevel()
        {
            Vertex[] last = new Vertex[LastLevel.Count];
            LastLevel.CopyTo(last);
            _oldVertices.AddRange(last);
            LastLevel = new List<Vertex>();
        }

        public void Add(Vertex leaf)
        {
            LastLevel.Add(leaf);
        }

        internal void SaveChild(Vertex leaf)
        {
            Vertex one = new Vertex(leaf);
            _oldVertices.Add(one);
        }

        internal void SaveChild(int i)
        {
            SaveChild(LastLevel[i]);
        }

        public void DeleteChild(int i)
        {
            SaveChild(i);
            LastLevel.RemoveAt(i);
        }

        internal void SetQuery(Queries.Query query)
        {
            if (LastLevel.Count > 0)
            {
                foreach (var child in LastLevel)
                {
                    child.SetQuery(query);
                }
            }
        }
    }
}
