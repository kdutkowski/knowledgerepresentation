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
    }
}
