namespace KnowledgeRepresentationReasoning.Scenario
{
    using KnowledgeRepresentationReasoning.World;

    public class ScenarioActionRecord : ScenarioDescriptionRecord
    {
        public WorldAction WorldAction { get; set; }
        public int Time { get; set; }

        public ScenarioActionRecord(WorldAction worldAction, int time)
            : base()
        {
            this.WorldAction = worldAction;
            Time = time;
        }


        /// <summary>
        /// Checks if Action in this object takes place in given time
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public bool CheckIfActiveAt(int time)
        {

            if (time>=this.WorldAction.StartAt&&time<this.WorldAction.GetEndTime()) return true;
            else return false;
        }
    }
}
