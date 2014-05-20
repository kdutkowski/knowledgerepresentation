namespace KnowledgeRepresentationReasoning.Scenario
{
    using log4net;
    using Microsoft.Practices.ServiceLocation;
    using System;

    public abstract class ScenarioDescriptionRecord
    {
        public Guid Id { get; private set; }
        protected readonly ILog logger;

        protected ScenarioDescriptionRecord()
        {
            Id = Guid.NewGuid();
            logger = ServiceLocator.Current.GetInstance<ILog>();
        }

        public new abstract string ToString();
    }
}
