namespace KnowledgeRepresentationReasoning.World.Records
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using KnowledgeRepresentationReasoning.Expressions;

    using Microsoft.Practices.ServiceLocation;

    public class ActionCausesIfRecord : WorldDescriptionRecord
    {
        private readonly ILogicExpression logicExpression;
        private readonly string resultExpression;
        private readonly string ifExpression;
        private readonly WorldAction worldAction;


        public ActionCausesIfRecord(WorldAction worldAction, string resultExpression, string ifExpression)
            : base(WorldDescriptionRecordType.ActionCausesIf)
        {
            this.logicExpression = ServiceLocator.Current.GetInstance<ILogicExpression>();
            this.resultExpression = resultExpression;
            this.ifExpression = ifExpression;
            this.worldAction = worldAction;
        }

        public bool IsFulfilled(State state, WorldAction startedWorldAction)
        {
            // Sprawdzamy czy to dana akcja się skończyła
            if (startedWorldAction == null || !startedWorldAction.Equals(this.worldAction))
            {
                return false;
            }

            // Sprawdzamy czy zachodzi warunek
            this.logicExpression.SetExpression(ifExpression);
            var fluents = this.logicExpression.GetFluentNames();
            var values = fluents.Select(t => new Tuple<string, bool>(t, state.Fluents.First(x => x.Name == t).Value));

            return this.logicExpression.Evaluate(values);
        }

        public ActionCausesIfRecord Concat(ActionCausesIfRecord record)
        {
            if (worldAction.Equals(record.worldAction))
            {
                return new ActionCausesIfRecord(worldAction,
                    "(" + resultExpression + ") && (" + record.resultExpression + ")",
                    "(" + ifExpression + ") && (" + record.ifExpression + ")");
            }
            return null;
        }

        public List<Fluent[]> GetResult()
        {
            this.logicExpression.SetExpression(resultExpression);
            return this.logicExpression.CalculatePossibleFluents();
        }

        public override string ToString()
        {
            return this.worldAction.ToString() + " causes " + resultExpression + ((ifExpression == "")?"":(" if " + ifExpression));
        }
    }
}
