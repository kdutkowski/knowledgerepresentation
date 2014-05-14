namespace KnowledgeRepresentationReasoning.Queries
{
    using System;

    public class AccesibleConditionQuery : Query
    {
        public AccesibleConditionQuery(string condition)
            : base()
        {
            queryType = QueryType.AccesibleCondition;
        }

        public override QueryResult CheckCondition(World.State state, World.WorldAction worldAction, int time)
        {
            throw new NotImplementedException();
        }
    }
}
