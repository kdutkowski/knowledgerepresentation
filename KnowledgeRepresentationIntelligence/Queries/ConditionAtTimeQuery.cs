namespace KnowledgeRepresentationReasoning.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using KnowledgeRepresentationReasoning.Expressions;
    using KnowledgeRepresentationReasoning.World;

    /* [condition] at ([time]) */

    public class ConditionAtTimeQuery : Query
    {
        private readonly string _condition; //condition to check 

        private readonly int _time; //Variable time in query, -1 means no time.

        private readonly SimpleLogicExpression _logicExp;

        private readonly string[] _fluentNames;

        public ConditionAtTimeQuery(QuestionType questionType, string condition, int time = -1)
            : base(QueryType.SatisfyConditionAtTime, questionType)
        {
            _condition = condition;
            _time = time;
            _logicExp = new SimpleLogicExpression(_condition);
            _fluentNames = _logicExp.GetFluentNames();

            _logger.Info("Creates:\n " + this);
        }

        public override QueryResult CheckCondition(State state, WorldAction worldAction, int actualTime)
        {
            _logger.Info("Checking condition: " + this._condition + "\nwith parameters:\nstate: " + state + "\naction: " + worldAction);

            QueryResult result = this.CheckTime(state, actualTime);

            string logResult = "Method result: " + result;

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

        private QueryResult CheckTime(State state, int actualTime)
        {
            var result = QueryResult.Undefined;

            if (-1 == _time)
            {
                result = CheckValuation(state);
                if (result != QueryResult.True)
                {
                    result = QueryResult.False == result ? QueryResult.Undefined : QueryResult.False;
                }
            }
            else if (_time <= actualTime)
            {
                result = CheckValuation(state);
            }
            else
            {
                result = QueryResult.Undefined;
            }

            return result;
        }

        private QueryResult CheckValuation(State state)
        {
            bool valuation = CalculateCondition(state);

            QueryResult result = valuation ? QueryResult.True : QueryResult.False;

            _logger.Info("Condition value:\ncondition: " + _condition + "result: " + result);
            return result;
        }

        private bool CalculateCondition(State state)
        {
            var values = new List<Tuple<string, bool>>();

            foreach (var name in _fluentNames)
            {
                var fluent = state.Fluents.FirstOrDefault(x => x.Name == name);

                if (fluent != null && fluent.Equals(default(Fluent)))
                {
                    _logger.Warn("Fluent '" + fluent.Name + "' does not exist!");
                    continue;
                }

                if (fluent != null)
                {
                    var pair = new Tuple<string, bool>(fluent.Name, fluent.Value);
                    values.Add(pair);
                }
            }

            return _logicExp.Evaluate(values);
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder("Condition at Time Query:\ncondition: ", 77);
            stringBuilder.Append(_condition);
            stringBuilder.Append("\ntime:");
            stringBuilder.Append(_time);

            return stringBuilder.ToString();
        }
    }
}