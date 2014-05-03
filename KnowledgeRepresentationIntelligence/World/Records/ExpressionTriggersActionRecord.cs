namespace KnowledgeRepresentationReasoning.World.Records
{
    using System;
    using System.Linq;

    using KnowledgeRepresentationReasoning.Expressions;

    using Microsoft.Practices.ServiceLocation;

    using Action = KnowledgeRepresentationReasoning.World.Action;

    public class ExpressionTriggersActionRecord : WorldDescriptionRecord
    {
        private readonly ILogicExpression logicExpression;
        private readonly string ifExpression;
        private readonly Action action;

        public ExpressionTriggersActionRecord(Action action, string ifExpression) 
            : base(WorldDescriptionRecordType.ExpressionTriggersAction)
        {
            this.logicExpression = ServiceLocator.Current.GetInstance<ILogicExpression>();
            this.ifExpression = ifExpression;
            this.action = action;
        }

        public bool IsFulfilled(State state)
        {
            // Sprawdzamy czy zachodzi warunek
            this.logicExpression.SetExpression(ifExpression);
            var fluents = this.logicExpression.GetFluentNames();
            var values = fluents.Select(t => new Tuple<string, bool>(t, state.Fluents.First(x => x.Name == t).Value));
            return this.logicExpression.Evaluate(values);
        }

        public Action GetResult()
        {
            return this.action;
        }
    }
}
