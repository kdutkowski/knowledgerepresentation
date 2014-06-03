namespace KnowledgeRepresentationReasoning
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using KnowledgeRepresentationReasoning.Queries;
    using KnowledgeRepresentationReasoning.Scenario;
    using KnowledgeRepresentationReasoning.World.Records;

    public interface IReasoning
    {
        void AddWorldDescriptionRecord(WorldDescriptionRecord record);

        void RemoveWorldDescriptionRecord(WorldDescriptionRecord record);

        QueryResult ExecuteQuery(Query query, ScenarioDescription scenarioDescription);

        Task<QueryResult> ExecuteQueryAsync(Query query);
    }
}