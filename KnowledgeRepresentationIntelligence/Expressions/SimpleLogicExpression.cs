using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("KnowledgeRepresentationReasoning.Test")]
namespace KnowledgeRepresentationReasoning.Expressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using ExpressionEvaluator;

    internal class SimpleLogicExpression : ILogicExpression
    {
        private readonly char[] specialCharacters = new[] { '|', '&', '(', ')', '!' };

        private string _expression { get; set; }

        public bool Evaluate()
        {
            if (this._expression.Equals(string.Empty)) return false;            
            var expression = new CompiledExpression(this._expression);
            return (bool)expression.Eval();
        }

        public bool Evaluate(IEnumerable<Tuple<string, bool>> values)
        {
            if (this._expression.Equals(string.Empty)) return false;
            var expression = new CompiledExpression(this._expression);
            expression.RegisterType("h", typeof(ExpressionHelper));
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
            this._expression = expression??string.Empty;
        }

        public string[] GetFluentNames()
        {
            string filteredString = this._expression;
            filteredString = this.specialCharacters.Aggregate(filteredString, (current, specialCharacter) => current.Replace(specialCharacter, ' '));
            filteredString = Regex.Replace(filteredString, " {2,}", " ");
            return filteredString.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();
        }

        class ExpressionHelper
        {
            public static bool impl(bool a, bool b)
            {
                if (a == false) return true;
                return b;
            }

            public static bool rown(bool a, bool b)
            {
                if (a == b) return true;
                else return false;
            }
        }
    }
}
