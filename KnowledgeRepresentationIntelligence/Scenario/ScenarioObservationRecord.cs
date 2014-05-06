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


        /// <summary>
        /// We check if all fluents of parameter state are equal to matching fluents stored in this object.
        /// </summary>
        /// <param name="state"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool CheckState(State state, int time)
        {

            if (!time.Equals(Time)) return true;

            for (int i = 0; i < state.Fluents.Capacity;i++ )
            {
                foreach(Fluent[] fluents in Expr.CalculatePossibleFluents()){
                    foreach(Fluent exprF in fluents){
                        Fluent stateF=state.Fluents[i];
                        if (stateF.Name.Equals(exprF.Name) && !(stateF.Value.Equals(exprF.Value))) return false;
                        //We return false as soon as we find non-matching fluents (names are the same, values not)
                    }
                }
            }

            return true;
        }
    }
}
