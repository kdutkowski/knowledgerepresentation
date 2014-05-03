using log4net.Config;

[assembly: XmlConfigurator(Watch = true)]
namespace KnowledgeRepresentationReasoning
{
    using System.Threading.Tasks;

    using Autofac;
    using Autofac.Extras.CommonServiceLocator;

    using KnowledgeRepresentationReasoning.Expressions;
    using KnowledgeRepresentationReasoning.Helpers;
    using KnowledgeRepresentationReasoning.Logic;
    using KnowledgeRepresentationReasoning.Queries;
    using KnowledgeRepresentationReasoning.Scenario;
    using KnowledgeRepresentationReasoning.World;
    using KnowledgeRepresentationReasoning.World.Records;

    using log4net;

    using Microsoft.Practices.ServiceLocation;

   
    public class ReasoningFasade : IReasoning
    {
        private IContainer Container { get; set; }
        private ILog logger { get; set; }
        private WorldDescription WorldDescription { get; set; }
        private ScenarioDescription ScenarioDescription { get; set; }

        public ReasoningFasade()
        {
            this.Initialize();
            logger = ServiceLocator.Current.GetInstance<ILog>();
        }

        public void AddWorldDescriptionRecord(WorldDescriptionRecord record)
        {
        }

        public void RemoveWorldDescriptionRecord(WorldDescriptionRecord record)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateWorldDescriptionRecord(WorldDescriptionRecord record)
        {
            throw new System.NotImplementedException();
        }

        public WorldDescription GetWorldDescription()
        {
            throw new System.NotImplementedException();
        }

        public void AddScenarioDescriptionRecord(ScenarioDescriptionRecord record)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveScenarioDescriptionRecord(ScenarioDescriptionRecord record)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateScenarioDescriptionRecord(ScenarioDescriptionRecord record)
        {
            throw new System.NotImplementedException();
        }

        public ScenarioDescription GetScenarioDescription()
        {
            throw new System.NotImplementedException();
        }

        public QueryResult ExecuteQuery(Query query)
        {
            throw new System.NotImplementedException();
        }

        public Task<QueryResult> ExecuteQueryAsync(QueryResult query)
        {
            throw new System.NotImplementedException();
        }

        public void Initialize()
        {
            // Autofac
            var builder = new ContainerBuilder();
            builder.RegisterModule(new LoggingModule());
            builder.RegisterInstance(LogManager.GetLogger(typeof(ReasoningFasade))).As<ILog>();
            builder.RegisterType<Tree>().As<ITree>();
            builder.RegisterType<SimpleLogicExpression>().As<ILogicExpression>();
            Container = builder.Build();
            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(Container));

            // WorldDescription
            
            // ScenarioDescription
        }
    }
}
