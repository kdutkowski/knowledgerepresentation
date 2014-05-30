using KnowledgeRepresentationReasoning.Logic;
using KnowledgeRepresentationReasoning.World;
using log4net;
using Microsoft.Practices.ServiceLocation;

namespace KnowledgeRepresentationReasoning.Queries
{
    public abstract class Query
    {
        protected ILog _logger { get; set; }

        protected QueryType _queryType { get; set; }

        public QuestionType questionType { get; private set; }

        public int Time { get; set; }

        protected Query(QueryType queryType, QuestionType questionType)
        {
            _queryType = queryType;
            this.questionType = questionType;
            Time = int.MaxValue;

            _logger = ServiceLocator.Current.GetInstance<ILog>();
        }

        public abstract QueryResult CheckCondition(State state, WorldAction worldAction, int time);

        public virtual QueryResult CheckCondition(Vertex v)
        {
            return CheckCondition(v, v.Time);
        }

        public virtual QueryResult CheckCondition(Vertex vertex, int time)
        {
            return CheckCondition(vertex.ActualState, vertex.ActualWorldAction, time);
        }
    }
}