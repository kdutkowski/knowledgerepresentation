namespace KnowledgeRepresentationReasoning.World.Records
{
    using log4net;

    using Microsoft.Practices.ServiceLocation;
    using System;

    public abstract class WorldDescriptionRecord
    {
        public Guid Id { get; private set; }
        public WorldDescriptionRecordType Type { get; set; }
        protected readonly ILog logger;

        protected WorldDescriptionRecord(WorldDescriptionRecordType type)
        {
            Id = Guid.NewGuid();
            Type = type;
            logger = ServiceLocator.Current.GetInstance<ILog>();
        }

        public override abstract string ToString();
    }
}
