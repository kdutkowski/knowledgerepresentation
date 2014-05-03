namespace KnowledgeRepresentationReasoning.World.Records
{
    public class ImpossibleActionAtRecord : WorldDescriptionRecord
    {
        private readonly Action action;
        private readonly int time;

        public ImpossibleActionAtRecord(Action action, int time)
            : base(WorldDescriptionRecordType.ImpossibleActionAt)
        {
            this.action = action;
            this.time = time;
        }

        public bool IsFulfilled(int currentTime)
        {
            return this.time == currentTime;
        }
    }
}
