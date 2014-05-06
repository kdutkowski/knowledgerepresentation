namespace KnowledgeRepresentationReasoning.World.Records
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using KnowledgeRepresentationReasoning.Expressions;

    using Microsoft.Practices.ServiceLocation;

    using Action = KnowledgeRepresentationReasoning.World.Action;

    public class ActionCausesIfRecord : WorldDescriptionRecord
    {
        private readonly ILogicExpression logicExpression;
        private readonly string resultExpression;
        private readonly string ifExpression;
        private readonly Action action;


        public ActionCausesIfRecord(Action action, string resultExpression, string ifExpression) 
            : base(WorldDescriptionRecordType.ActionCausesIf)
        {
            this.logicExpression = ServiceLocator.Current.GetInstance<ILogicExpression>();
            this.resultExpression = resultExpression;
            this.ifExpression = ifExpression;
            this.action = action;
        }

        public bool IsFulfilled(State state, Action startedAction)
        {
            // Sprawdzamy czy to dana akcja się rozpoczęła
            if (!startedAction.Equals(action))
                return false;
            // Sprawdzamy czy zachodzi warunek
            this.logicExpression.SetExpression(ifExpression);
            var fluents = this.logicExpression.GetFluentNames();
            var values = fluents.Select(t => new Tuple<string, bool>(t, state.Fluents.First(x => x.Name == t).Value));
            return this.logicExpression.Evaluate(values);
        }

        public List<Fluent[]> GetResult()
        {
            this.logicExpression.SetExpression(resultExpression);
            return this.logicExpression.CalculatePossibleFluents();
        }

        public override string ToString()
        {
            return action.ToString() + " causes " + resultExpression + " if " + ifExpression;
        }
    }
}
