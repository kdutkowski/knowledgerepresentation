using KnowledgeRepresentationReasoning.Expressions;
using KnowledgeRepresentationReasoning.World;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KnowledgeRepresentationReasoning.Queries
{
    class ConditionAtTimeQuery : Query
    {
        private string _condition; 
        private int _time;

        private SimpleLogicExpression _logicExp;
        private string[] _fluentNames;

        public ConditionAtTimeQuery(string condition, int time = -1)
        {
            queryType = QueryType.SatisfyConditionAtTime;
            _condition = condition;
            _time = time;
            _logicExp = new SimpleLogicExpression();
            _logicExp.SetExpression(_condition);
            _fluentNames = _logicExp.GetFluentNames();

            logger.Info("Creates SatisfyConditionAtTimeQuery query with parameters:\ncondition: " + condition + "\ntime: " + time);
        }

        public override QueryResult CheckCondition(World.State state, World.Action action, int time)
        {
            logger.Info("Checking condition: " + _condition + "\nwith parameters:\nstate: " + state.ToString() + "\naction: " + action.ToString() + "\ntime: " + time);
            QueryResult result = QueryResult.None;

            if (time == _time || _time == -1)
                result = CheckValuation(state);
            else if (time < _time)
                result = QueryResult.Undefined;
            else if (_time < time)
                result = QueryResult.False;

            string logResult = "Return result: " + result;
            if (QueryResult.None == result)
                logger.Warn(logResult);
            else
                logger.Info(logResult);

            return result;
        }

        private QueryResult CheckValuation(State state)
        {
            QueryResult result = QueryResult.None;
            bool valuation = CalculateCondition(state);

            if (true == valuation)
                result = QueryResult.True;
            else result = QueryResult.False;

            logger.Info("Condition value:\ncondition: " + _condition + "result: " + result);
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
    }
}
