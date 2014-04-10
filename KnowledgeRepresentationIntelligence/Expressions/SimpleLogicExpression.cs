using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("KnowledgeRepresentationReasoning.Test")]
namespace KnowledgeRepresentationReasoning.Expressions
{
    using System;
    using System.Collections.Generic;

    using ExpressionEvaluator;

    internal class SimpleLogicExpression : ILogicExpression
    {
        public string _expression;

        public SimpleLogicExpression()
        {
            _expression = string.Empty;
        }

        public SimpleLogicExpression(string expression)
        {
            _expression = expression??string.Empty;
        }

        public bool Evaluate()
        {
            if (_expression.Equals(string.Empty)) return false;            
            var expression = new CompiledExpression(_expression);
            return (bool)expression.Eval();
        }

        public bool Evaluate(IEnumerable<Tuple<string, bool>> values)
        {
            if (_expression.Equals(string.Empty)) return false;
            var expression = new CompiledExpression(_expression);
            if (values != null)
            {
                foreach (var value in values)
                {
                    expression.RegisterType(value.Item1, value.Item2);
                }
            }
            return (bool)expression.Eval();
        }

        public void SetExpression(string expression)
        {
            _expression = expression??string.Empty;
        }
    }
}
