namespace KnowledgeRepresentationReasoning.Scenario
{
    using KnowledgeRepresentationReasoning.Expressions;
    using KnowledgeRepresentationReasoning.World;
    using System.Collections.Generic;

    class ScenarioObservationRecord : ScenarioDescriptionRecord
    {
        public ILogicExpression Expr { get; set; }
        public int Time { get; set; }

        public ScenarioObservationRecord(ILogicExpression expr, int time)
            : base()
        {
            Expr = expr;
            Time = time;
        }

        public bool checkState(State state, int time)
        {
            //TODO type something here
            return true;
        }
    }
}
