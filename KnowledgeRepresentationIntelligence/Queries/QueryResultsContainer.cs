using log4net;
using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;
using System.Linq;

namespace KnowledgeRepresentationReasoning.Queries
{
    class QueryResultsContainer : IQueryResultsContainer
    {
        private ILog _logger;

        private QuestionType _questionType;
        private List<QueryResult> _results;

        public QueryResultsContainer(QuestionType questionType)
        {
            _questionType = questionType;
            _results = new List<QueryResult>();

            _logger = ServiceLocator.Current.GetInstance<ILog>();
            _logger.Info("Create query result container with question type '" + _questionType);
        }

        public QueryResult CollectResults()
        {
            QueryResult finalResult = QueryResult.Undefined;

            if (QuestionType.Always == _questionType)
            {
                finalResult = CollectResultsForAlways(finalResult);
            }
            else if (QuestionType.Ever == _questionType)
            {
                finalResult = CollectResultsForEver(finalResult);
            }

            _logger.Info("Collect result: " + finalResult);
            return finalResult;
        }

        private QueryResult CollectResultsForEver(QueryResult finalResult)
        {
            bool answer = _results.Any(x => x == QueryResult.True);
            return answer ? QueryResult.True : QueryResult.False;
        }

        private QueryResult CollectResultsForAlways(QueryResult finalResult)
        {
            bool answer = _results.Any(x => x == QueryResult.False);
            return answer ? QueryResult.False : QueryResult.True;
        }

        public bool CanQuickAnswer()
        {
            bool answer = false;

            if (QuestionType.Always == _questionType)
            {
                answer = CanQuickAnswerForAlways();
            }
            else if (QuestionType.Ever == _questionType)
            {
                answer = CanQuickAnswerForEver();
            }

            _logger.Info("Query can answer now: " + answer);
            return answer;
        }

        private bool CanQuickAnswerForEver()
        {
            return _results.Any(x => x == QueryResult.True);
        }

        private bool CanQuickAnswerForAlways()
        {
            return _results.Any(x => x == QueryResult.False);
        }

        public void AddMany(QueryResult queryResult, int count = 1)
        {
            _results.AddRange(Enumerable.Repeat(queryResult, count));
            _logger.Info("Add query result '" + queryResult + "' " + count + " times");
        }

        public int Count()
        {
            return _results.Count;
        }

        internal QueryResult CollectResultsForExecutableScenario()
        {
            bool answer = _results.All(x => x == QueryResult.False);
            return answer ? QueryResult.False : QueryResult.True;
        }
    }
}
