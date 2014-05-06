namespace KnowledgeRepresentationReasoning.World.Records
{
    using log4net;

    using Microsoft.Practices.ServiceLocation;

    public abstract class WorldDescriptionRecord
    {
        public WorldDescriptionRecordType Type { get; set; }
        protected readonly ILog logger;

        protected WorldDescriptionRecord(WorldDescriptionRecordType type)
        {
            Type = type;
            logger = ServiceLocator.Current.GetInstance<ILog>();
        }
    }
}
