using KnowledgeRepresentationReasoning.Expressions;
using KnowledgeRepresentationReasoning.Logic;
using KnowledgeRepresentationReasoning.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeRepresentationReasoning.Queries
{
    class SatisfyConditionAtTimeQuery : Query
    {
        private string _condition; 
        private int _time;

        private SimpleLogicExpression _logicExp;
        private string[] _fluentNames;

        public SatisfyConditionAtTimeQuery(string condition, int time = -1)
        {
            queryType = QueryType.SatisfyConditionAtTime;
            _condition = condition;
            _time = time;
            _logicExp = new SimpleLogicExpression();
            _logicExp.SetExpression(_condition);
            _fluentNames = _logicExp.GetFluentNames();
        }

        public override QueryResult CheckCondition(World.State state, World.Action action, int time)
        {
            QueryResult result = QueryResult.None;

            if (time == _time || _time == -1)
                result = CheckValuation(state, _condition);
            else if (time < _time)
                result = QueryResult.None;
            else if (_time < time)
                result = QueryResult.False;

            return result;
        }

        private QueryResult CheckValuation(State state, string _condition)
        {
            QueryResult result = QueryResult.None;
            bool valuation = CalculateCondition(state, _condition);

            if (true == valuation)
                result = QueryResult.True;
            else result = QueryResult.False;

            return result;
        }

        private bool CalculateCondition(World.State state, string _condition)
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
