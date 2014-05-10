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

        public List<Vertex> LastLevel {get; private set;}
        private List<Vertex> _allVertices;

        private int _TInf;

        public Tree(int timeInf)
        {
            _logger = Microsoft.Practices.ServiceLocation.ServiceLocator.Current.GetInstance<ILog>();
            _logicExpression = new SimpleLogicExpression();

            LastLevel = new List<Vertex>();
            _allVertices = new List<Vertex>();

            _TInf = timeInf;
        }

        //missing:
        //- add T_inf
        //- action can't be before first observation -> interface validation!
        public int AddFirstLevel(World.WorldDescription WorldDescription, Scenario.ScenarioDescription ScenarioDescription, out int numberOfImpossibleLeaf)
        {
            numberOfImpossibleLeaf = 0;
            int t = 0;

            //states
            List<string> fluentNames = WorldDescription.GetFluentNames().ToList<string>();
            List<State> states = CreateStatesBasedOnObservations(fluentNames, ScenarioDescription, ref t);

            foreach (var state in states)
            {
                Vertex newVertex = new Vertex(state, null, t, null);
                LastLevel.Add(newVertex);
            }

            //action
            WorldAction worldAction = ScenarioDescription.GetActionAtTime(t);
            if (worldAction != null)
                for (int i = 0; i < LastLevel.Count; ++i)
                {
                    LastLevel[i].WorldAction = (WorldAction)worldAction.Clone();
                    if (!WorldDescription.Validate(LastLevel[i]))
                    {
                        ++numberOfImpossibleLeaf;
                    }
                }
            return t;
        }

        private List<State> CreateStatesBasedOnObservations(List<string> fluentNames, ScenarioDescription scenarioDescription, ref int time)
        {
            List<State> states = new List<State>();

            time = scenarioDescription.GetNextObservationTime(0);
            ScenarioObservationRecord observation = scenarioDescription.GetObservationFromTime(time);
            if (observation == null)
            {
                _logger.Warn("Scenario has no observations!");
                State state = new State();
                state.AddFluents(fluentNames);
                states.Add(state);
            }
            else
            {
                time = observation.Time;
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
                        catch (System.Exception)
                        {
                            _logger.Error("Fluent " + fluent.Name + " doesn't exist!");
                        }
                        
                    }
                    states.Add(state);
                }
            }

            return states;
        }


        public int LastLevelCount()
        {
            return LastLevel.Count();
        }

        internal void SaveLastLevel()
        {
            Vertex[] last = new Vertex[LastLevel.Count];
            LastLevel.CopyTo(last);
            _allVertices.AddRange(last);
            LastLevel = new List<Vertex>();
        }

        internal void Add(Vertex leaf)
        {
            LastLevel.Add(leaf);
        }

        internal void SaveChild(Vertex leaf)
        {
            Vertex one = new Vertex(leaf);
            _allVertices.Add(one);
        }

        internal void SaveChild(int i)
        {
            SaveChild(LastLevel[i]);
        }

        internal void DeleteChild(int i)
        {
            LastLevel.RemoveAt(i);
        }
    }
}
