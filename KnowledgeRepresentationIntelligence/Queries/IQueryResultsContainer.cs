namespace KnowledgeRepresentationReasoning.Queries
{
    interface IQueryResultsContainer
    {
        QueryResult CollectResults();

        bool CanQuickAnswer();
    }
}
