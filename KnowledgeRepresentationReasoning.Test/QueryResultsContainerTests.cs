using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KnowledgeRepresentationReasoning.Queries;

namespace KnowledgeRepresentationReasoning.Test
{
    [TestClass]
    public class QueryResultsContainerTests:TestBase
    {
        private QueryResultsContainer _queryResultContainer;

        [TestMethod]
        public void AddManyAddOne()
        {
            _queryResultContainer = new QueryResultsContainer(QuestionType.Always);

            _queryResultContainer.AddMany(QueryResult.Undefined);

            int result = _queryResultContainer.Count();

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void AddManyAddMany()
        {
            _queryResultContainer = new QueryResultsContainer(QuestionType.Always);

            int many = 5;   //most random number
            for (int i = 0; i < many; ++i)
            {
                _queryResultContainer.AddMany(QueryResult.Undefined);
            }

            int result = _queryResultContainer.Count();

            Assert.AreEqual(many, result);
        }

        [TestMethod]
        public void CanAnswerEverOnTrue()
        {
            _queryResultContainer = new QueryResultsContainer(QuestionType.Ever);
            _queryResultContainer.AddMany(QueryResult.True);
            _queryResultContainer.AddMany(QueryResult.False);
            _queryResultContainer.AddMany(QueryResult.False);

            bool result = _queryResultContainer.CanQuickAnswer();

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void CanAnswerEverOnFalse()
        {
            _queryResultContainer = new QueryResultsContainer(QuestionType.Ever);
            _queryResultContainer.AddMany(QueryResult.False);
            _queryResultContainer.AddMany(QueryResult.False);

            bool result = _queryResultContainer.CanQuickAnswer();

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void CanAnswerAlwaysOnTrue()
        {
            _queryResultContainer = new QueryResultsContainer(QuestionType.Always);
            _queryResultContainer.AddMany(QueryResult.True);
            _queryResultContainer.AddMany(QueryResult.True);
            _queryResultContainer.AddMany(QueryResult.False);

            bool result = _queryResultContainer.CanQuickAnswer();

            Assert.AreEqual(true, result);
        }

        [TestMethod]
        public void CanAnswerAlwaysOnFalse()
        {
            _queryResultContainer = new QueryResultsContainer(QuestionType.Always);
            _queryResultContainer.AddMany(QueryResult.True);
            _queryResultContainer.AddMany(QueryResult.True);
            _queryResultContainer.AddMany(QueryResult.True);

            bool result = _queryResultContainer.CanQuickAnswer();

            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void CollectAnswerForEverQueryResultTrue()
        {
            _queryResultContainer = new QueryResultsContainer(QuestionType.Ever);
            _queryResultContainer.AddMany(QueryResult.True);
            _queryResultContainer.AddMany(QueryResult.False);

            QueryResult result = _queryResultContainer.CollectResults();

            Assert.AreEqual(QueryResult.True, result);
        }

        [TestMethod]
        public void CollectAnswerForEverQueryResultFalse()
        {
            _queryResultContainer = new QueryResultsContainer(QuestionType.Ever);
            _queryResultContainer.AddMany(QueryResult.False);
            _queryResultContainer.AddMany(QueryResult.False);

            QueryResult result = _queryResultContainer.CollectResults();

            Assert.AreEqual(QueryResult.False, result);
        }

        [TestMethod]
        public void CollectAnswerForAlwaysQueryResultTrue()
        {
            _queryResultContainer = new QueryResultsContainer(QuestionType.Always);
            _queryResultContainer.AddMany(QueryResult.True);
            _queryResultContainer.AddMany(QueryResult.True);

            QueryResult result = _queryResultContainer.CollectResults();

            Assert.AreEqual(QueryResult.True, result);
        }

        [TestMethod]
        public void CollectAnswerForAlwaysQueryResultFalse()
        {
            _queryResultContainer = new QueryResultsContainer(QuestionType.Always);
            _queryResultContainer.AddMany(QueryResult.True);
            _queryResultContainer.AddMany(QueryResult.False);

            QueryResult result = _queryResultContainer.CollectResults();

            Assert.AreEqual(QueryResult.False, result);
        }
    }
}
