namespace KnowledgeRepresentationReasoning.World.Records
{
    using System;
    using System.Linq;

    using KnowledgeRepresentationReasoning.Expressions;

    using Microsoft.Practices.ServiceLocation;

    public class ActionReleasesIfRecord : WorldDescriptionRecord
    {
        private readonly ILogicExpression logicExpression;
        private readonly string ifExpression;
        private readonly WorldAction worldAction;
        private readonly Fluent fluent;

        public ActionReleasesIfRecord(WorldAction worldAction, Fluent fluent, string ifExpression) 
            : base(WorldDescriptionRecordType.ActionReleasesIf)
        {
            this.logicExpression = ServiceLocator.Current.GetInstance<ILogicExpression>();
            this.ifExpression = ifExpression;
            this.worldAction = worldAction;
            this.fluent = fluent;
        }

        public bool IsFulfilled(State state, WorldAction endedWorldAction)
        {
            // Sprawdzamy czy to dana akcja się skończyła
            if (!endedWorldAction.Equals(this.worldAction)) 
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

        public override string ToString()
        {
            return worldAction.ToString() + " releases " + fluent.ToString() + " if " + ifExpression;
        }
    }
}
