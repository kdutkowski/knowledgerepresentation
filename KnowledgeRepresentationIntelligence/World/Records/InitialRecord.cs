namespace KnowledgeRepresentationReasoning.World.Records
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using KnowledgeRepresentationReasoning.Expressions;
    using KnowledgeRepresentationReasoning.Helpers;

    public class InitialRecord : WorldDescriptionRecord
    {
        private const char FluentPrefix = '$';

        private readonly ILogicExpression logicExpression;
        private string expression;

        public string Expression
        {
            get { return this.expression; }
            set { this.SetExpression(value); }
        }

        public List<Fluent[]> PossibleFluents { get; private set; }

        public InitialRecord(string expression) : base(WorldDescriptionRecordType.Initially)
        {
            this.SetExpression(expression);
            this.logicExpression = new SimpleLogicExpression(expression);
        }

        private void SetExpression(string expression)
        {
            this.expression = expression;
            int numberOfFluents = this.expression.Count(t => t == FluentPrefix);
            string[] fluentNames = this.GetFluentNames();
            foreach (var code in Gray.GetGreyCodesWithLengthN(numberOfFluents))
            {
                var possibleFluents = new Fluent[numberOfFluents];
                for (int i = 0; i < numberOfFluents; i++)
                    possibleFluents[i] = new Fluent { Id = i.ToString(), Name = fluentNames[i], Value = code[i] };
                if(this.logicExpression.Evaluate(possibleFluents.Select(t => new Tuple<string, bool>(t.Name, t.Value))))
                    PossibleFluents.Add(possibleFluents);
            }
        }

        private string[] GetFluentNames()
        {
            return (
                    from Match fluent 
                    in Regex.Matches(this.expression, @"(?<!\w)" + FluentPrefix + @"\w+") 
                    select fluent.Value
                    ).ToArray();
        }
    }
}
