namespace KnowledgeRepresentationReasoning.Queries
{
    using System;

    class ExecutableScenarioQuery : Query
    {
        public ExecutableScenarioQuery()
            : base()
        {
            queryType = QueryType.ExecutableScenario;
        }

        public override QueryResult CheckCondition(World.State state, World.WorldAction worldAction, int time)
        {
            throw new NotImplementedException();
        }
    }
}
