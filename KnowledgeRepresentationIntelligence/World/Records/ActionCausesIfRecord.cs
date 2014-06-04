namespace KnowledgeRepresentationReasoning.World.Records
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using KnowledgeRepresentationReasoning.Expressions;
    using Microsoft.Practices.ServiceLocation;

    public class ActionCausesIfRecord : WorldDescriptionRecord
    {
        private readonly ILogicExpression _logicExpression;
        private readonly string _resultExpression;
        private readonly string _ifExpression;
        private readonly WorldAction _worldAction;


        public ActionCausesIfRecord(WorldAction worldAction, string resultExpression, string ifExpression)
            : base(WorldDescriptionRecordType.ActionCausesIf)
        {
            _logicExpression = ServiceLocator.Current.GetInstance<ILogicExpression>();
            _resultExpression = resultExpression;
            _ifExpression = ifExpression;
            _worldAction = worldAction;
        }

        public bool IsFulfilled(State state, WorldAction startedWorldAction)
        {
            // Sprawdzamy czy to dana akcja się skończyła
            if (startedWorldAction == null || !startedWorldAction.Equals(this._worldAction))
            {
                return false;
            }

            // Sprawdzamy czy zachodzi warunek
            _logicExpression.SetExpression(_ifExpression);
            var fluents = _logicExpression.GetFluentNames();
            var values = fluents.Select(t => new Tuple<string, bool>(t, state.Fluents.First(x => x.Name == t).Value));

            return _logicExpression.Evaluate(values);
        }

        public ActionCausesIfRecord Concat(ActionCausesIfRecord record)
        {
            if (_worldAction.Equals(record._worldAction))
            {
                return new ActionCausesIfRecord(_worldAction,
                    "(" + _resultExpression + ") && (" + record._resultExpression + ")",
                    "(" + _ifExpression + ") && (" + record._ifExpression + ")");
            }
            return null;
        }

        public List<Fluent[]> GetResult()
        {
            _logicExpression.SetExpression(_resultExpression);
            return _logicExpression.CalculatePossibleFluents();
        }

        public override string ToString()
        {
            return _worldAction + " causes " + _resultExpression +
                   ((_ifExpression == "") ? "" : (" if " + _ifExpression));
        }
    }
}