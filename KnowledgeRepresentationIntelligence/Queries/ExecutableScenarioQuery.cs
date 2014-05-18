namespace KnowledgeRepresentationReasoning.Queries
{
    using System;

    class ExecutableScenarioQuery : Query
    {
        public ExecutableScenarioQuery(QuestionType questionType)
            : base(QueryType.ExecutableScenario, questionType) { }

        public override QueryResult CheckCondition(World.State state, World.WorldAction worldAction, int time)
        {
            throw new NotImplementedException();
        }
    }
}
