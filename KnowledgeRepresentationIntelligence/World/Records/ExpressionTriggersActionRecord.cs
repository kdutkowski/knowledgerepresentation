namespace KnowledgeRepresentationReasoning.World.Records
{
    using System;
    using System.Linq;

    using KnowledgeRepresentationReasoning.Expressions;

    using Microsoft.Practices.ServiceLocation;

    public class ExpressionTriggersActionRecord : WorldDescriptionRecord
    {
        private readonly ILogicExpression logicExpression;
        private readonly WorldAction worldAction;

        public ExpressionTriggersActionRecord(WorldAction worldAction, string ifExpression) 
            : base(WorldDescriptionRecordType.ExpressionTriggersAction)
        {
            this.logicExpression = new SimpleLogicExpression(ifExpression);
            this.worldAction = worldAction;
        }

        public bool IsFulfilled(State state)
        {
            return this.logicExpression.Evaluate(state);
        }

        public WorldAction GetResult(int time)
        {
            this.worldAction.StartAt = time;
            return this.worldAction;
        }

        public override string ToString()
        {
            return logicExpression + " triggers " + worldAction.ToString();
        }
    }
}
