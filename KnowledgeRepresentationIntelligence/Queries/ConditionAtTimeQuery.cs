using KnowledgeRepresentationReasoning.Expressions;
using KnowledgeRepresentationReasoning.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnowledgeRepresentationReasoning.Queries
{
    /* [condition] at ([time]) */
    public class ConditionAtTimeQuery : Query
    {
        private string _condition;      //condition to check 
        private int _time;              //Variable time in query, -1 means no time.

        private SimpleLogicExpression _logicExp;
        private string[] _fluentNames;

        public ConditionAtTimeQuery(QuestionType questionType, string condition, int time = -1)
            : base(QueryType.SatisfyConditionAtTime, questionType)
        {
            _condition = condition;
            _time = time;
            _logicExp = new SimpleLogicExpression(_condition);
            _fluentNames = _logicExp.GetFluentNames();

            _logger.Info("Creates:\n "+ this.ToString());
        }

        public override QueryResult CheckCondition(World.State state, World.WorldAction worldAction, int time)
        {
            _logger.Info("Checking condition: " + _condition + "\nwith parameters:\nstate: " + state.ToString() + "\naction: " + worldAction??worldAction.ToString() + "\ntime: " + time);
            QueryResult result = QueryResult.None;

            if (time == _time || _time == -1)
                result = CheckValuation(state);
            else if (_time < time)
                result = QueryResult.False;
            else if (_time > time)
                result = QueryResult.Undefined;

            string logResult = "Method result: " + result;
            if (QueryResult.None == result)
                _logger.Warn(logResult);
            else
                _logger.Info(logResult);
 
            return result;
        }

        private QueryResult CheckValuation(State state)
        {
            QueryResult result = QueryResult.None;
            bool valuation = CalculateCondition(state);

            if (true == valuation)
                result = QueryResult.True;
            else result = QueryResult.False;

            _logger.Info("Condition value:\ncondition: " + _condition + "result: " + result);
            return result;
        }

        private bool CalculateCondition(World.State state)
        {
            List<Tuple<string, bool>> values = new List<Tuple<string, bool>>();
            foreach (var name in _fluentNames)
            {
                Fluent fluent = state.Fluents.Where( x => x.Name == name).FirstOrDefault<Fluent>();
                if (fluent.Equals(default(Fluent)))
                {
                    _logger.Warn("Fluent '" + fluent.Name + "' does not exist!"); 
                    continue;
                }
                Tuple<string, bool> pair = new Tuple<string,bool>(fluent.Name, fluent.Value);
                values.Add(pair);
            }

            return _logicExp.Evaluate(values);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder("Condition at Time Query:\ncondition: ", 77);
            stringBuilder.Append(_condition);
            stringBuilder.Append("\ntime:");
            stringBuilder.Append(_time);

            return stringBuilder.ToString();
        }
    }
}
