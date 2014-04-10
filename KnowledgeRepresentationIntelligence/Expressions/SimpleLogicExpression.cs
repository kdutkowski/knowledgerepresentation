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

        public SimpleLogicExpression(string expression)
        {
            _expression = expression;
        }

        public bool Evaluate(IEnumerable<Tuple<string, bool>> values)
        {
            var expression = new CompiledExpression(_expression);
            foreach (var value in values)
            {
                expression.RegisterType(value.Item1, value.Item2);                
            }
            return (bool)expression.Eval();
        }

        public void SetExpression(string expression)
        {
            _expression = expression;
        }
    }
}
