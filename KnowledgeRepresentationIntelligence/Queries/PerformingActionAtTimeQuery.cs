namespace KnowledgeRepresentationReasoning.Queries
{
    using System;

    using KnowledgeRepresentationReasoning.World;

    public class PerformingActionAtTimeQuery : Query
    {
        public PerformingActionAtTimeQuery(WorldAction action, int time = -1)
            : base()
        {
            queryType = QueryType.PerformingActionAtTime;
        }

        public override QueryResult CheckCondition(World.State state, World.WorldAction worldAction, int time)
        {
            throw new NotImplementedException();
        }
    }
}
