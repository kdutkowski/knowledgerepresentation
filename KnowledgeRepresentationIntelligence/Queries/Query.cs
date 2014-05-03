using KnowledgeRepresentationReasoning.Logic;
using KnowledgeRepresentationReasoning.World;
using log4net;
using System.Collections.Generic;
namespace KnowledgeRepresentationReasoning.Queries
{
    public abstract class Query
    {
        internal ILog logger { get; set; }

        public QueryType queryType { get; set; }
        public QuestionType questionType {get; set;}

        public abstract QueryResult CheckCondition(State state, Action action, int time);
    }
}
