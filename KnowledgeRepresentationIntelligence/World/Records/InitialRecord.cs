namespace KnowledgeRepresentationReasoning.World.Records
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using KnowledgeRepresentationReasoning.Expressions;
    using KnowledgeRepresentationReasoning.Helpers;

    using Microsoft.Practices.ServiceLocation;

    public class InitialRecord : WorldDescriptionRecord
    {
        private readonly char[] specialCharacters = new[] { '|', '&', '(', ')', '!' };

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
            this.PossibleFluents = new List<Fluent[]>();
            this.logicExpression = ServiceLocator.Current.GetInstance<ILogicExpression>();
            this.SetExpression(expression);
        }

        public InitialRecord ConcatAnd(InitialRecord record)
        {
            return new InitialRecord(Expression + " && " + record.Expression);
        }

        public InitialRecord ConcatOr(InitialRecord record)
        {
            return new InitialRecord(Expression + " || " + record.Expression);
        }

        private void SetExpression(string expression)
        {
            this.expression = expression;
            this.logicExpression.SetExpression(expression);
            string[] fluentNames = this.GetFluentNames();
            int numberOfFluents = fluentNames.Length;
            foreach (var code in Gray.GetGreyCodesWithLengthN(numberOfFluents))
            {
                var possibleFluents = new Fluent[numberOfFluents];
                for (int i = 0; i < numberOfFluents; i++)
                    possibleFluents[i] = new Fluent { Id = i.ToString(), Name = fluentNames[i], Value = code[i] };
                if(this.logicExpression.Evaluate(possibleFluents.Select(t => new Tuple<string, bool>(t.Name, t.Value))))
                    PossibleFluents.Add(possibleFluents);
            }
        }

        public string[] GetFluentNames()
        {
            string filteredString = this.Expression;
            filteredString = this.specialCharacters.Aggregate(filteredString, (current, specialCharacter) => current.Replace(specialCharacter, ' '));
            filteredString = Regex.Replace(filteredString, " {2,}", " ");
            return filteredString.Split(new []{ ' ' }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();
        }
    }
}
