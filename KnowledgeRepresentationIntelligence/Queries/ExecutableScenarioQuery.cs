using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowledgeRepresentationReasoning.Logic;
using KnowledgeRepresentationReasoning.Scenario;

namespace KnowledgeRepresentationReasoning.Queries
{
    class ExecutableScenarioQuery : Query
    {

        ScenarioDescription _scenario;
        public ExecutableScenarioQuery(QuestionType questionType, ScenarioDescription scenario)
            : base(QueryType.ExecutableScenario, questionType)
        {
            _scenario = scenario;
        }
        /// <summary>
        /// Checks whetever actions and observations in this query are consistent with those 
        /// </summary>
        /// <param name="state"></param>
        /// <param name="worldAction"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override QueryResult CheckCondition(World.State state, World.WorldAction worldAction, int time)
        {

            _logger.Info("Checking if scenario: " + _scenario.ToString() + " with parameters:\nstate: " + state.ToString() + "\naction: " + worldAction ?? worldAction.ToString() + " is executable");

            QueryResult result = QueryResult.Undefined;
            foreach (ScenarioObservationRecord sor in _scenario.observations)
                if (sor.Time == time)
                    if (!sor.Expr.Evaluate(state)) result = QueryResult.False;

            if (result == QueryResult.True)
                foreach (ScenarioActionRecord sar in _scenario.actions)
                    if (sar.Time == time)
                        if (sar.CheckIfActiveAt(time) && !sar.WorldAction.Equals(worldAction)) result = QueryResult.False;

            string logResult = "Executable: " + result;

            if (QueryResult.Undefined == result)
                _logger.Warn(logResult);
            else
                _logger.Info(logResult);

            return result;
            //            throw new NotImplementedException();
        }

        public override QueryResult CheckCondition(Vertex v){
            _logger.Info("Checking if scenario: " + _scenario.ToString() + " with parameters:\nstate: " + v.ActualState.ToString() + "\naction: " + v.ActualWorldAction ?? v.ActualWorldAction.ToString() + " is executable");

            QueryResult result = QueryResult.Undefined;
            if (!v.IsPossible) result = QueryResult.False;

            if (v.NextActions == null || v.NextActions.Count==0) return QueryResult.True;



            return result;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder("Executable Scenario Query:\nscenario: ", 77);
            stringBuilder.Append(_scenario);
            return stringBuilder.ToString();
        }
    }
 }

