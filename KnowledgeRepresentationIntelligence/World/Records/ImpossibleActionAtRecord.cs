namespace KnowledgeRepresentationReasoning.World.Records
{
    public class ImpossibleActionAtRecord : WorldDescriptionRecord
    {
        private readonly WorldAction worldAction;
        private readonly int time;

        public ImpossibleActionAtRecord(WorldAction worldAction, int time)
            : base(WorldDescriptionRecordType.ImpossibleActionAt)
        {
            this.worldAction = worldAction;
            this.time = time;
        }

        public bool IsFulfilled(int currentTime)
        {
            return this.time == currentTime;
        }

        public WorldAction GetResult()
        {
            return this.worldAction;
        }
    }
}
