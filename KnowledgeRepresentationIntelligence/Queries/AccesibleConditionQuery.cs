namespace KnowledgeRepresentationReasoning.Queries
{
    using System.Text;

    using KnowledgeRepresentationReasoning.Expressions;
    using KnowledgeRepresentationReasoning.Scenario;

    public class AccesibleConditionQuery : Query
    {
        private readonly string _condition; //condition to check 
        ExecutableScenarioQuery ExecQuery;
        ConditionAtTimeQuery CondAtTimeQuery;


        // TODO: Czy to do czegoś potrzebne skoro jest nieużywane?
        private string[] _fluentNames;

        private readonly ScenarioDescription _scenario;

        public AccesibleConditionQuery(QuestionType questionType, string condition, ScenarioDescription scenario)
            : base(QueryType.AccesibleCondition, questionType)
        {
            _queryType = QueryType.AccesibleCondition;
            _condition = condition;
            var logicExp = new SimpleLogicExpression(this._condition);
            _fluentNames = logicExp.GetFluentNames();
            _scenario = scenario;


            ExecQuery = new ExecutableScenarioQuery(QuestionType.Ever, _scenario);
            CondAtTimeQuery = new ConditionAtTimeQuery(QuestionType.Ever, _condition);


            _logger.Info("Creates:\n " + this);
        }

        /// <summary>
        /// Checks given parameters with conditionAtTimeQuery and ExecutableScenarioQuery
        /// </summary>
        /// <param name="state"></param>
        /// <param name="worldAction"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public override QueryResult CheckCondition(World.State state, World.WorldAction worldAction, int time)
        {
            _logger.Info("Checking condition: " + this._condition + "\n accesible with parameters:\nstate: " + state + "\naction: " + worldAction);

            QueryResult condAtTimeResult = CondAtTimeQuery.CheckCondition(state, worldAction, time);
            QueryResult execResult  = ExecQuery.CheckCondition(state, worldAction, time);



            QueryResult result;

            if (condAtTimeResult == QueryResult.Error || condAtTimeResult == QueryResult.Error)
            {
                result = QueryResult.Error;
            }
            else
            {
                if (condAtTimeResult == QueryResult.False || condAtTimeResult == QueryResult.False)
                {
                    result = QueryResult.False;
                }
                else
                {
                    if (condAtTimeResult == QueryResult.Undefined || condAtTimeResult == QueryResult.Undefined)
                    {
                        result = QueryResult.Undefined;
                    }
                    else
                    {
                        result = QueryResult.True;

                    }
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
    }
}