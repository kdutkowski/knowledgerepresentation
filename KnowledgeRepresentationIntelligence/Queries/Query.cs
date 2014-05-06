using KnowledgeRepresentationReasoning.Logic;
using KnowledgeRepresentationReasoning.World;
using log4net;
using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;
namespace KnowledgeRepresentationReasoning.Queries
{
    public abstract class Query
    {
        internal ILog _logger { get; set; }

        public QueryType queryType { get; set; }
        public QuestionType questionType {get; set;}

        protected Query()
        {
            _logger = ServiceLocator.Current.GetInstance<ILog>();
        }

        public abstract QueryResult CheckCondition(State state, Action action, int time);
    }
}
