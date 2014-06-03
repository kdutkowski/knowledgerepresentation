namespace KnowledgeRepresentationReasoning.Queries
{
    using System.Text;
    using KnowledgeRepresentationReasoning.Scenario;

    public class AccesibleConditionQuery : Query
    {
        private readonly string _condition; //condition to check 
        private readonly ConditionAtTimeQuery _condAtTimeQuery;
        private readonly ScenarioDescription _scenario;

        public AccesibleConditionQuery(QuestionType questionType, string condition, ScenarioDescription scenario)
            : base(QueryType.AccesibleCondition, questionType)
        {
            _queryType = QueryType.AccesibleCondition;
            _condition = condition;
            _scenario = scenario;
            _condAtTimeQuery = new ConditionAtTimeQuery(questionType, _condition);
            _logger.Info("Creates:\n " + this);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder("Accesible Condition Query:\ncondition: ", 77);
            stringBuilder.Append(_condition);
            stringBuilder.Append("\nscenario:\n");
            stringBuilder.Append(_scenario);
            return stringBuilder.ToString();
        }

        public override QueryResult CheckCondition(World.State state, World.WorldAction worldAction, int time)
        {
            _logger.Info("Checking condition: " + _condition + "\n accesible with parameters:\nstate: " + state + "\naction: " + worldAction);

             QueryResult condAtTimeResult = _condAtTimeQuery.CheckCondition(state, worldAction, time);

            string logResult = "Accesible: " + condAtTimeResult;

            if (QueryResult.Undefined == condAtTimeResult)
            {
                _logger.Warn(logResult);
            }
            else
            {
                _logger.Info(logResult);
            }

            return condAtTimeResult;
        }
    }
}