namespace KnowledgeRepresentationReasoning.World.Records
{
    using System.Collections.Generic;
    using KnowledgeRepresentationReasoning.Expressions;

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
                PossibleFluents = this.logicExpression.CalculatePossibleFluents();
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

        public string[] GetResult()
        {
            return this.logicExpression.GetFluentNames();
        }

        public override string ToString()
        {
            return "Initially " + expression;
        }
    }
}
