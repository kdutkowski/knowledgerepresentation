using System;
using System.Linq;

namespace KnowledgeRepresentationReasoning.World.Records
{
    using KnowledgeRepresentationReasoning.Expressions;

    using Microsoft.Practices.ServiceLocation;

    public class ActionInvokesAfterIf : WorldDescriptionRecord
    {
        private readonly ILogicExpression logicExpression;
        private readonly string ifExpression;

        public ActionInvokesAfterIf(Action action, Action result, int after, string ifExpression) 
            : base(WorldDescriptionRecordType.ActionCausesIf)
        {
            this.logicExpression = ServiceLocator.Current.GetInstance<ILogicExpression>();
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
