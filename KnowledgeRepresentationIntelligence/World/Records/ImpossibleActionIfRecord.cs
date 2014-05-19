namespace KnowledgeRepresentationReasoning.World.Records
{
    using System;
    using System.Linq;

    using KnowledgeRepresentationReasoning.Expressions;

    using Microsoft.Practices.ServiceLocation;

    public class ImpossibleActionIfRecord : WorldDescriptionRecord
    {
        private readonly ILogicExpression logicExpression;
        private readonly string ifExpression;
        private readonly WorldAction worldAction;

        public ImpossibleActionIfRecord(WorldAction worldAction, string ifExpression) 
            : base(WorldDescriptionRecordType.ImpossibleActionIf)
        {
            this.logicExpression = ServiceLocator.Current.GetInstance<ILogicExpression>();
            this.ifExpression = ifExpression;
            this.worldAction = worldAction;
        }

        public bool IsFulfilled(State state)
        {
            this.logicExpression.SetExpression(ifExpression);
            var fluents = this.logicExpression.GetFluentNames();
            var values = fluents.Select(t => new Tuple<string, bool>(t, state.Fluents.First(x => x.Name == t).Value));
            return this.logicExpression.Evaluate(values);
        }

        public WorldAction GetResult()
        {
            return this.worldAction;
        }

        public override string ToString()
        {
            return "impossible " + worldAction.ToString() + " if " + ifExpression;
        }
    }
}
