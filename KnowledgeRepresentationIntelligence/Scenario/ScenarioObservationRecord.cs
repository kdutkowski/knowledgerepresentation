namespace KnowledgeRepresentationReasoning.Scenario
{
    using KnowledgeRepresentationReasoning.Expressions;
    using KnowledgeRepresentationReasoning.World;
    using System.Collections.Generic;

    public class ScenarioObservationRecord : ScenarioDescriptionRecord
    {
        public ILogicExpression Expr { get; set; }
        public int Time { get; set; }

        public ScenarioObservationRecord(ILogicExpression expr, int time)
            : base()
        {
            Expr = expr;
            Time = time;
        }

        /// <summary>
        /// We check if all fluents of parameter state are equal to matching fluents stored in this object.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool CheckState(State state, int time)
        {
            if (!time.Equals(Time))
            {
                return true;
            }

            return Expr.Evaluate(state);
        }

        public override string ToString()
        {
            return "Expression " + Expr.ToString() + " in time " + Time.ToString();
        }
    }
}
