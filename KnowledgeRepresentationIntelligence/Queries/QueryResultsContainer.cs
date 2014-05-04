using KnowledgeRepresentationReasoning.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnowledgeRepresentationReasoning.Queries
{
    class QueryResultsContainer : IQueryResultsContainer
    {
        private QuestionType _questionType;
        private List<QueryResult> _results;

        public QueryResultsContainer(QuestionType questionType)
        {
            this._questionType = questionType;
        }

        public QueryResult CollectResults()
        {
            QueryResult finalResult = QueryResult.None;

            if (QuestionType.Always == _questionType)
            {
                foreach (var result in _results)
                {
                    finalResult = QueryResult.True;
                    if (QueryResult.False == result)
                    {
                        finalResult = QueryResult.False;
                        break;
                    }
                }
            }
            else if (QuestionType.Ever == _questionType)
            {
                foreach (var result in _results)
                {
                    finalResult = QueryResult.False;
                    if (QueryResult.True == result)
                    {
                        finalResult = QueryResult.True;
                        break;
                    }
                }
            }

            return finalResult;
        }

        public bool CanAnswer()
        {
            bool answer = true;

            foreach (var result in _results)
            {
                if (result != QueryResult.True && result != QueryResult.False)
                {
                    answer = false;
                    break;
                }
            }
            return answer;
        }
    }
}
