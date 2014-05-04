using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnowledgeRepresentationReasoning.Queries
{
    interface IQueryResultsContainer
    {
        QueryResult CollectResults();

        bool CanAnswer();
    }
}
