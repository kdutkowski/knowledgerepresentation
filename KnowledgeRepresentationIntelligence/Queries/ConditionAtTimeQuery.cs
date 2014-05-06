using KnowledgeRepresentationReasoning.Expressions;
using KnowledgeRepresentationReasoning.World;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KnowledgeRepresentationReasoning.Queries
{
    public class ConditionAtTimeQuery : Query
    {
        private string _condition; 
        private int _time;              //Variable time in query, -1 means no time.

        private SimpleLogicExpression _logicExp;
        private string[] _fluentNames;

        public ConditionAtTimeQuery(string condition, int time = -1)
            : base()
        {
            queryType = QueryType.SatisfyConditionAtTime;
            _condition = condition;
            _time = time;
            _logicExp = new SimpleLogicExpression(_condition);
            _fluentNames = _logicExp.GetFluentNames();

            _logger.Info("Creates "+ this.ToString());
        }

        public override QueryResult CheckCondition(World.State state, World.WorldAction worldAction, int time)
        {
            _logger.Info("Checking condition: " + _condition + "\nwith parameters:\nstate: " + state.ToString() + "\naction: " + worldAction.ToString() + "\ntime: " + time);
            QueryResult result = QueryResult.None;

            if (time == _time || _time == -1)
                result = CheckValuation(state);
            else if (time < _time)
                result = QueryResult.Undefined;
            else if (_time < time)
                result = QueryResult.False;

            string logResult = "Return result: " + result;
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
                Fluent fluent = state.Fluents.FirstOrDefault( x => x.Name == name);
                Tuple<string, bool> pair = new Tuple<string,bool>(fluent.Name, fluent.Value);
                values.Add(pair);
            }

            return _logicExp.Evaluate(values);
        }

        public override string ToString()
        {
            return "Condition at Time Query:\n" +
                    "condition: " + _condition + "\n"+
                    "time: " + _time;
        }
    }
}
