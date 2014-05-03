namespace KnowledgeRepresentationReasoning.World.Records
{
    using System;
    using System.Linq;

    using KnowledgeRepresentationReasoning.Expressions;

    using Microsoft.Practices.ServiceLocation;

    using Action = KnowledgeRepresentationReasoning.World.Action;

    public class ActionInvokesAfterIfRecord : WorldDescriptionRecord
    {
        private readonly ILogicExpression logicExpression;
        private readonly string ifExpression;
        private readonly Action action;
        private readonly Action result;
        private readonly int after;

        public ActionInvokesAfterIfRecord(Action action, Action result, int after, string ifExpression) 
            : base(WorldDescriptionRecordType.ActionInvokesAfterIf)
        {
            this.logicExpression = ServiceLocator.Current.GetInstance<ILogicExpression>();
            this.ifExpression = ifExpression;
            this.action = action;
            this.result = result;
            this.after = after;
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

        public Action GetResult(int time)
        {
            this.result.StartAt = time + after + action.Duration;
            return this.result;
        }
    }
}
