namespace KnowledgeRepresentationReasoning.Queries
{
    using System.Text;
    using KnowledgeRepresentationReasoning.Scenario;
    using KnowledgeRepresentationReasoning.Logic;

    public class AccesibleConditionQuery : Query
    {
        private readonly string _condition; //condition to check 
        private readonly ConditionAtTimeQuery _condAtTimeQuery;
        private readonly ExecutableScenarioQuery _executableScenarioQuery;

        private QueryResult _condAtTimeResult;

        public AccesibleConditionQuery(QuestionType questionType, string condition, ScenarioDescription scenario)
            : base(QueryType.AccesibleCondition, questionType)
        {
            _queryType = QueryType.AccesibleCondition;
            _condition = condition;
            _condAtTimeQuery = new ConditionAtTimeQuery(questionType, _condition);
            _executableScenarioQuery = new ExecutableScenarioQuery(questionType);

            _condAtTimeResult = QueryResult.Undefined;

            _logger.Info("Creates:\n " + this);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder("Accesible Condition Query:\ncondition: ", 77);
            stringBuilder.Append(_condition);
            stringBuilder.Append("\nscenario:\n");
            return stringBuilder.ToString();
        }

        public override QueryResult CheckCondition(Vertex vertex)
        {
            _logger.Info("Checking condition: " + _condition + "\n accesible with parameters:\nstate: " + vertex.ActualState + "\naction: " + vertex.ActualWorldAction);

            QueryResult condAtTimeResult = _condAtTimeQuery.CheckCondition(vertex);
            QueryResult executableScenarioResult = _executableScenarioQuery.CheckCondition(vertex);
            QueryResult result = QueryResult.Undefined;

            string logResult = "Accesible: " + condAtTimeResult;

            if (_condAtTimeResult == QueryResult.Undefined && condAtTimeResult == QueryResult.True)
            {
                _condAtTimeResult = condAtTimeResult;
            }

            if (executableScenarioResult == QueryResult.False)
            {
                result = QueryResult.False;
            }
            else if (executableScenarioResult == QueryResult.True)
            {
                result = _condAtTimeResult;
            }

            return result;
        }
    }
}