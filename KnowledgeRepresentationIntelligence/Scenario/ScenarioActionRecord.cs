namespace KnowledgeRepresentationReasoning.Scenario
{
    using KnowledgeRepresentationReasoning.World;

    class ScenarioActionRecord : ScenarioDescriptionRecord
    {
        public Action Action { get; set; }
        public int Time { get; set; }

        public ScenarioActionRecord(Action action, int time)
            : base()
        {
            Action = action;
            Time = time;
        }


        /// <summary>
        /// Checks if Action in this object takes place in given time
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool CheckIfActiveAt(int time)
        {

            if (time>=Action.StartAt&&time<Action.GetEndTime()) return true;
            else return false;
        }
    }
}
