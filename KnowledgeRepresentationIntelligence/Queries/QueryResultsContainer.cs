using KnowledgeRepresentationReasoning.Queries;
using log4net;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnowledgeRepresentationReasoning.Queries
{
    class QueryResultsContainer : IQueryResultsContainer
    {
        private ILog _logger;
        private QuestionType _questionType;
        private List<QueryResult> _results;

        public QueryResultsContainer(QuestionType questionType)
        {
            _logger = ServiceLocator.Current.GetInstance<ILog>();
            _questionType = questionType;
            _results = new List<QueryResult>();
        }

        public QueryResult CollectResults()
        {
            QueryResult finalResult = QueryResult.None;

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
            finalResult = QueryResult.False;
            foreach (var result in _results)
            {
                if (QueryResult.True == result)
                {
                    finalResult = QueryResult.True;
                    break;
                }
            }
            return finalResult;
        }

        private QueryResult CollectResultsForAlways(QueryResult finalResult)
        {
            finalResult = QueryResult.True;
            foreach (var result in _results)
            {
                if (QueryResult.False == result)
                {
                    finalResult = QueryResult.False;
                    break;
                }
            }
            return finalResult;
        }

        public bool CanAnswer()
        {
            bool answer = true;

            if (QuestionType.Always == _questionType)
            {
                answer = CanAnswerForAlways(answer);
            }
            else if (QuestionType.Ever == _questionType)
            {
                answer = CanAnswerForEver(answer);
            }

            _logger.Info("Query can answer: " + answer);
            return answer;
        }

        private bool CanAnswerForEver(bool answer)
        {
            foreach (var result in _results)
            {
                if (QueryResult.True == result)
                {
                    answer = true;
                    break;
                }
                if (result != QueryResult.True && result != QueryResult.False)
                {
                    answer = false;
                    //break;
                }
            }
            return answer;
        }

        private bool CanAnswerForAlways(bool answer)
        {
            foreach (var result in _results)
            {
                if (QueryResult.False == result)
                {
                    answer = true;
                    break;
                }
                if (result != QueryResult.True && result != QueryResult.False)
                {
                    answer = false;
                    //break;
                }
            }
            return answer;
        }

        internal void Add(QueryResult queryResult, int count=0)
        {
            while (count-- > 0)
            {
                _results.Add(queryResult);
                _logger.Info("Adding query result: " + queryResult);
            }
        }
    }
}
