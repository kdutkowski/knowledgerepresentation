namespace KnowledgeRepresentationReasoning.World.Records
{
    using System;
    using System.Linq;

    using KnowledgeRepresentationReasoning.Expressions;

    using Microsoft.Practices.ServiceLocation;

    using Action = KnowledgeRepresentationReasoning.World.Action;

    public class ActionCausesIfRecord : WorldDescriptionRecord
    {
        private readonly ILogicExpression logicExpression;
        private readonly string resultExpression;
        private readonly string ifExpression;

        public ActionCausesIfRecord(Action action, string resultExpression, string ifExpression) 
            : base(WorldDescriptionRecordType.ActionCausesIf)
        {
            this.logicExpression = ServiceLocator.Current.GetInstance<ILogicExpression>();
            this.resultExpression = resultExpression;
            this.ifExpression = ifExpression;
        }

        public bool IsFulfilled(State state)
        {
            this.logicExpression.SetExpression(ifExpression);
            var fluents = this.logicExpression.GetFluentNames();
            var values = fluents.Select(t => new Tuple<string, bool>(t, state.Fluents.First(x => x.Name == t).Value));
            return this.logicExpression.Evaluate(values);
        }
    }
}
