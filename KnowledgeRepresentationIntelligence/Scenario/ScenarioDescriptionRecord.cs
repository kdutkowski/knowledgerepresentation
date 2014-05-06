namespace KnowledgeRepresentationReasoning.Scenario
{
    using log4net;
    using Microsoft.Practices.ServiceLocation;

    public abstract class ScenarioDescriptionRecord
    {
        protected readonly ILog logger;

        protected ScenarioDescriptionRecord()
        {
            logger = ServiceLocator.Current.GetInstance<ILog>();
        }
    }
}
