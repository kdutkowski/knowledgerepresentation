namespace KnowledgeRepresentationReasoning.World.Records
{
    using System;
    using System.Linq;

    using KnowledgeRepresentationReasoning.Expressions;

    using Microsoft.Practices.ServiceLocation;

    public class ActionInvokesAfterIfRecord : WorldDescriptionRecord
    {
        private readonly ILogicExpression logicExpression;
        private readonly string ifExpression;
        private readonly WorldAction worldAction;
        private readonly WorldAction result;
        private readonly int after;

        public ActionInvokesAfterIfRecord(WorldAction worldAction, WorldAction result, int after, string ifExpression)
            : base(WorldDescriptionRecordType.ActionInvokesAfterIf)
        {
            this.logicExpression = ServiceLocator.Current.GetInstance<ILogicExpression>();
            this.ifExpression = ifExpression;
            this.worldAction = worldAction;
            this.result = result;
            this.after = after;
        }

        public bool IsFulfilled(State state, WorldAction endedWorldAction)
        {
            // Sprawdzamy czy to dana akcja się skończyła
            if (!endedWorldAction.Equals(this.worldAction))
            {
                return false;
            }

            // Sprawdzamy czy zachodzi warunek
            this.logicExpression.SetExpression(ifExpression);
            var fluents = this.logicExpression.GetFluentNames();
            var values = fluents.Select(t => new Tuple<string, bool>(t, state.Fluents.First(x => x.Name == t).Value));

            return this.logicExpression.Evaluate(values);
        }

        public WorldAction GetResult(int time)
        {
            this.result.StartAt = time + after + this.worldAction.Duration;
            return this.result;
        }

        public override string ToString()
        {
            return this.worldAction.ToString() + " invokes " + result.ToString() + " after " + after + ((ifExpression == "") ? "" : (" if " + ifExpression));
        }
    }
}
