namespace KnowledgeRepresentationReasoning.World.Records
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using KnowledgeRepresentationReasoning.Expressions;
    using KnowledgeRepresentationReasoning.Helpers;

    using Microsoft.Practices.ServiceLocation;

    public class InitialRecord : WorldDescriptionRecord
    {
        private readonly ILogicExpression logicExpression;
        private string expression;

        public string Expression
        {
            get { return this.expression; }
            set
            {
                expression = value;
                this.logicExpression.SetExpression(expression);
                this.CalculatePossibleFluents();
            }
        }

        public List<Fluent[]> PossibleFluents { get; private set; }

        public InitialRecord(string expression) : base(WorldDescriptionRecordType.Initially)
        {
            this.PossibleFluents = new List<Fluent[]>();
            this.logicExpression = ServiceLocator.Current.GetInstance<ILogicExpression>();
            this.Expression = expression;
        }

        public InitialRecord ConcatAnd(InitialRecord record)
        {
            return new InitialRecord(Expression + " && " + record.Expression);
        }

        public InitialRecord ConcatOr(InitialRecord record)
        {
            return new InitialRecord(Expression + " || " + record.Expression);
        }

        private void CalculatePossibleFluents()
        {
            string[] fluentNames = this.logicExpression.GetFluentNames();
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
            return this.logicExpression.GetFluentNames();
        }
    }
}
