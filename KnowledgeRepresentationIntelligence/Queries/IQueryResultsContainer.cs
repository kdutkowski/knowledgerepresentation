namespace KnowledgeRepresentationReasoning.Queries
{
    internal interface IQueryResultsContainer
    {
        QueryResult CollectResults();

        bool CanQuickAnswer();
    }
}