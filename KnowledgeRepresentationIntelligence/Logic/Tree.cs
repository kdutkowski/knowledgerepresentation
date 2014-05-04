using KnowledgeRepresentationReasoning.Expressions;
using KnowledgeRepresentationReasoning.Scenario;
using KnowledgeRepresentationReasoning.World;
using log4net;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;

[assembly: InternalsVisibleTo("KnowledgeRepresentationReasoning.Test")]
namespace KnowledgeRepresentationReasoning.Logic
{
    internal class Tree : ITree
    {
        ILogicExpression _logicExpression;
        private ILog _logger;

        private List<Vertex> _actualLevel;
        private List<Vertex> _allVertices;

        private int _TInf;

        public Tree(int timeInf)
        {
            _logger = Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<ILog>();
            _logicExpression = new SimpleLogicExpression();

            _actualLevel = new List<Vertex>();
            _allVertices = new List<Vertex>();

            _TInf = timeInf;
        }

        public int AddFirstLevel(World.WorldDescription WorldDescription, Scenario.ScenarioDescription ScenarioDescription)
        {
            int t = 0;

            //states
            List<string> fluentNames = (List<string>)WorldDescription.GetFluentNames();
            List<State> states = CreateStatesBasedOnObservations(fluentNames, ScenarioDescription, ref t);
            foreach (var state in states)
            {
                Vertex newVertex = new Vertex(state, null, t, null);
                _actualLevel.Add(newVertex);
            }

            //action
            Action action = ScenarioDescription.GetActionAtTime(t);
            if (action != null)
                for (int i = 0; i < _actualLevel.Count; ++i)
                    _actualLevel[0].Action = (Action)action.Clone();

            return t;
        }

        private List<State> CreateStatesBasedOnObservations(List<string> fluentNames, Scenario.ScenarioDescription scenarioDescription, ref int time)
        {
            List<State> states = new List<State>();

            List<ScenarioObservationRecord> observations = scenarioDescription.GetNextObservationFromTime(time);
            if (observations.Count == 0)
            {
                _logger.Warn("Scenario has no observations!");
            }
            else
            {
                time = observations[0].Time;
                _logicExpression = new SimpleLogicExpression();
                foreach (var observation in observations)
                {
                    _logicExpression.AddExpression(observation.Expr);
                }

                List<Fluent[]> possibleInitialValues = _logicExpression.CalculatePossibleFluents();
                foreach (var valuation in possibleInitialValues)
                {
                    State state = new State();
                    state.AddFluents(fluentNames);
                    foreach (var fluent in valuation)
                    {
                        state.Fluents.First(f => f.Name == fluent.Name).Value = fluent.Value;
                    }
                    states.Add(state);
                }
            }

            return states;
        }
    }
}
