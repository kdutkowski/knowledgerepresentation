using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowledgeRepresentationReasoning.Logic;
using KnowledgeRepresentationReasoning.Scenario;


﻿namespace KnowledgeRepresentationReasoning.Queries
{
    using System.Linq;
    using System.Text;

    using KnowledgeRepresentationReasoning.Scenario;

    public class ExecutableScenarioQuery : Query
    {
        private readonly ScenarioDescription _scenario;

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
            _logger.Info("Checking if scenario: " + this._scenario + " with parameters:\nstate: " + state + "\naction: " + worldAction);

            var result = QueryResult.Undefined;

            if (this._scenario.observations.Any(sor => sor.Time.Equals(time) && !sor.Expr.Evaluate(state)))
            {
                result = QueryResult.False;
            }

            if (result == QueryResult.True)
            {
                if (this._scenario.actions.Any(sar => sar.Time.Equals(time) && sar.CheckIfActiveAt(time) && !sar.WorldAction.Equals(worldAction)))
                {
                    result = QueryResult.False;
                }
            }

            string logResult = "Executable: " + result;

            if (QueryResult.Undefined == result)
            {
                _logger.Warn(logResult);
            }
            else
            {
                _logger.Info(logResult);
            }

            return result;
        }

        public override QueryResult CheckCondition(Vertex v){

            _logger.Info("Checking if scenario: " + _scenario.ToString() + " with parameters:\nstate: " + v.ActualState.ToString() + "\naction: " + v.ActualWorldAction ?? v.ActualWorldAction.ToString() + " is executable");

            QueryResult result;
            
            if (!v.IsPossible)
                if (QuestionType.Always == questionType) result = QueryResult.False;
                else result = QueryResult.Undefined;
            else
                if (v.NextActions == null || v.NextActions.Count==0)
                    result= QueryResult.True;
                else
                    result=QueryResult.False;
    
            string logResult = "Executable: " + result;

            if (QueryResult.Undefined == result)
            {
                _logger.Warn(logResult);
            }
            else
            {
                _logger.Info(logResult);
            }

            return result;
        }


        public override string ToString()
        {
            var stringBuilder = new StringBuilder("Executable Scenario Query:\nscenario: ", 77);
            stringBuilder.Append(_scenario);
            return stringBuilder.ToString();
        }
    }
}
