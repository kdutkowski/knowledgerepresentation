namespace KnowledgeRepresentationReasoning
{
    using System.Threading.Tasks;

    using Autofac;

    using KnowledgeRepresentationReasoning.Logging;
    using KnowledgeRepresentationReasoning.Logic;
    using KnowledgeRepresentationReasoning.Queries;
    using KnowledgeRepresentationReasoning.Scenario;
    using KnowledgeRepresentationReasoning.World;
    using KnowledgeRepresentationReasoning.World.Records;

    public class ReasoningFasade : IReasoning
    {
        private static IContainer Container { get; set; }

        public void AddWorldDescriptionRecord(WorldDescriptionRecord record)
        {
            throw new System.NotImplementedException();
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
            var builder = new ContainerBuilder();
            builder.RegisterModule(new LoggingModule());
            builder.RegisterType<Tree>().As<ITree>();
            Container = builder.Build();
        }
    }
}
