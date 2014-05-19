using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowledgeRepresentationReasoning.Expressions;
using KnowledgeRepresentationReasoning.Scenario;


namespace KnowledgeRepresentationReasoning.Queries
{
    public class AccesibleConditionQuery : Query
    {
        private string _condition;      //condition to check 
        private SimpleLogicExpression _logicExp;
        private string[] _fluentNames;
        ScenarioDescription _scenario;


        public AccesibleConditionQuery(QuestionType questionType, string condition, ScenarioDescription scenario)
            : base(QueryType.AccesibleCondition, questionType)
        {
            _queryType = QueryType.AccesibleCondition;
            _condition = condition;
            _logicExp = new SimpleLogicExpression(_condition);
            _fluentNames = _logicExp.GetFluentNames();
            _scenario = scenario;
            _logger.Info("Creates:\n " + this.ToString());

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
            _logger.Info("Checking condition: " + _condition + "\n accesible with parameters:\nstate: " + state.ToString() + "\naction: " + worldAction ?? worldAction.ToString());
            QueryResult result = QueryResult.Undefined;

            Query query = new ConditionAtTimeQuery(QuestionType.Ever, _condition, time);

            result = query.CheckCondition(state, worldAction, time);
            if (result == QueryResult.Undefined || result == QueryResult.Error || result == QueryResult.False)
            {

                return result;
            }

            query = new ExecutableScenarioQuery(QuestionType.Ever, _scenario);
            result = query.CheckCondition(state, worldAction, time);
            if (result == QueryResult.Undefined || result == QueryResult.Error || result == QueryResult.False) return result;

            string logResult = "Accesible: " + result;

            if (QueryResult.Undefined == result)
                _logger.Warn(logResult);
            else
                _logger.Info(logResult);

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