﻿namespace KnowledgeRepresentationReasoning.World.Records
{
    using System;
    using System.Linq;

    using KnowledgeRepresentationReasoning.Expressions;

    using Microsoft.Practices.ServiceLocation;

    public class ExpressionTriggersActionRecord : WorldDescriptionRecord
    {
        private readonly ILogicExpression logicExpression;
        private readonly string ifExpression;
        private readonly WorldAction worldAction;

        public ExpressionTriggersActionRecord(WorldAction worldAction, string ifExpression) 
            : base(WorldDescriptionRecordType.ExpressionTriggersAction)
        {
            this.logicExpression = ServiceLocator.Current.GetInstance<ILogicExpression>();
            this.ifExpression = ifExpression;
            this.worldAction = worldAction;
        }

        public bool IsFulfilled(State state)
        {
            // Sprawdzamy czy zachodzi warunek
            this.logicExpression.SetExpression(ifExpression);
            var fluents = this.logicExpression.GetFluentNames();
            var values = fluents.Select(t => new Tuple<string, bool>(t, state.Fluents.First(x => x.Name == t).Value));
            return this.logicExpression.Evaluate(values);
        }

        public WorldAction GetResult(int time)
        {
            this.worldAction.StartAt = time;
            return this.worldAction;
        }

        public override string ToString()
        {
            return ifExpression + " triggers " + worldAction.ToString();
        }
    }
}
