namespace KnowledgeRepresentationReasoning
{
    using System.Threading.Tasks;

    using KnowledgeRepresentationReasoning.Queries;
    using KnowledgeRepresentationReasoning.World;
    using KnowledgeRepresentationReasoning.Scenario;
    using KnowledgeRepresentationReasoning.World.Records;

    public interface IReasoning
    {
        void AddWorldDescriptionRecord(WorldDescriptionRecord record);
        void RemoveWorldDescriptionRecord(WorldDescriptionRecord record);
        void UpdateWorldDescriptionRecord(WorldDescriptionRecord record);
        WorldDescription GetWorldDescription();

        void AddScenarioDescriptionRecord(ScenarioDescriptionRecord record);
        void RemoveScenarioDescriptionRecord(ScenarioDescriptionRecord record);
        void UpdateScenarioDescriptionRecord(ScenarioDescriptionRecord record);
        ScenarioDescription GetScenarioDescription();

        QueryResult ExecuteQuery(Query query);
        Task<QueryResult> ExecuteQueryAsync(Query query);

        void Initialize();
    }
}
