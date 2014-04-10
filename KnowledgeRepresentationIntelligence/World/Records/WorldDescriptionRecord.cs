namespace KnowledgeRepresentationReasoning.World.Records
{
    public abstract class WorldDescriptionRecord
    {
        public WorldDescriptionRecordType Type { get; set; }

        protected WorldDescriptionRecord(WorldDescriptionRecordType type)
        {
            Type = type;
        }
    }
}
