namespace KnowledgeRepresentationReasoning.Queries
{
    using System.Text;

    using KnowledgeRepresentationReasoning.Expressions;
    using KnowledgeRepresentationReasoning.Scenario;

    public class AccesibleConditionQuery : Query
    {
        private readonly string _condition; //condition to check 
        private ExecutableScenarioQuery ExecQuery;
        private ConditionAtTimeQuery CondAtTimeQuery;

        private readonly ScenarioDescription _scenario;
        private QueryResult _answerForCondAtTimeResult;

        public AccesibleConditionQuery(QuestionType questionType, string condition, ScenarioDescription scenario)
            : base(QueryType.AccesibleCondition, questionType)
        {
            _queryType = QueryType.AccesibleCondition;
            _condition = condition;
            var logicExp = new SimpleLogicExpression(this._condition);
            _scenario = scenario;

            _answerForCondAtTimeResult = QueryResult.Undefined;
            ExecQuery = new ExecutableScenarioQuery(questionType, _scenario);
            CondAtTimeQuery = new ConditionAtTimeQuery(questionType, _condition);

            _logger.Info("Creates:\n " + this);
        }

        public override QueryResult CheckCondition(Logic.Vertex v, int time)
        {
            _logger.Info("Checking condition: " + this._condition + "\n accesible with parameters:\nstate: " + v.ActualState + "\naction: " + v.ActualWorldAction);

            QueryResult condAtTimeResult = CondAtTimeQuery.CheckCondition(v.ActualState, v.ActualWorldAction, time);
            QueryResult execResult = ExecQuery.CheckCondition(v);
            QueryResult result = QueryResult.Undefined;

            if (condAtTimeResult == QueryResult.True || condAtTimeResult == QueryResult.False)
            {
                _answerForCondAtTimeResult = condAtTimeResult;
            }

            if (_answerForCondAtTimeResult == QueryResult.False || execResult == QueryResult.False)
            {
                result = QueryResult.False;
            }
            else if(execResult == QueryResult.True)
            {
                if (_answerForCondAtTimeResult == QueryResult.True)
                {
                    result = QueryResult.True;
                }
                else if (_answerForCondAtTimeResult == QueryResult.False)
                {
                    result = QueryResult.False;
                }
            }

            string logResult = "Accesible: " + result;

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
            StringBuilder stringBuilder = new StringBuilder("Accesible Condition Query:\ncondition: ", 77);
            stringBuilder.Append(_condition);
            stringBuilder.Append("\nscenario:\n");
            stringBuilder.Append(_scenario);
            return stringBuilder.ToString();
        }

        public override QueryResult CheckCondition(World.State state, World.WorldAction worldAction, int time)
        {
            throw new System.NotImplementedException();
        }
    }
}