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

        public override QueryResult CheckCondition(Vertex v)
        {
            _logger.Info("Checking if scenario: " + this._scenario + " with parameters:\nstate: " + v.ActualState + "\naction: " + v.ActualWorldAction);

            var result = QueryResult.Undefined;

            if (!v.IsPossible)
            {
                result = QueryResult.False;
            }
            else if (v.ActualWorldAction == null && (v.NextActions == null || v.NextActions.Count == 0) && !_scenario.Actions.Any(a => a.Time >= v.Time))
            {
                result = QueryResult.True;
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

        public override string ToString()
        {
            var stringBuilder = new StringBuilder("Executable Scenario Query:\nscenario: ", 77);
            stringBuilder.Append(_scenario);
            return stringBuilder.ToString();
        }

        public override QueryResult CheckCondition(World.State state, World.WorldAction worldAction, int time)
        {
            throw new NotImplementedException();
        }
    }
}
