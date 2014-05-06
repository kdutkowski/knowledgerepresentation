namespace KnowledgeRepresentationReasoning.World.Records
{
    using System;
    using System.Linq;

    using KnowledgeRepresentationReasoning.Expressions;

    using Microsoft.Practices.ServiceLocation;

    using Action = KnowledgeRepresentationReasoning.World.Action;

    public class ActionReleasesIfRecord : WorldDescriptionRecord
    {
        private readonly ILogicExpression logicExpression;
        private readonly string ifExpression;
        private readonly Action action;
        private readonly Fluent fluent;

        public ActionReleasesIfRecord(Action action, Fluent fluent, string ifExpression) 
            : base(WorldDescriptionRecordType.ActionReleasesIf)
        {
            this.logicExpression = ServiceLocator.Current.GetInstance<ILogicExpression>();
            this.ifExpression = ifExpression;
            this.action = action;
            this.fluent = fluent;
        }

        public bool IsFulfilled(State state, Action endedAction)
        {
            // Sprawdzamy czy to dana akcja się skończyła
            if (!endedAction.Equals(action)) 
                return false;
            // Sprawdzamy czy zachodzi warunek
            this.logicExpression.SetExpression(ifExpression);
            var fluents = this.logicExpression.GetFluentNames();
            var values = fluents.Select(t => new Tuple<string, bool>(t, state.Fluents.First(x => x.Name == t).Value));
            return this.logicExpression.Evaluate(values);
        }

        public Fluent GetResult(int time)
        {
            return this.fluent;
        }
    }
}
