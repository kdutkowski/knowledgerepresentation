namespace KnowledgeRepresentationReasoning.Test
{
    using KnowledgeRepresentationReasoning.Queries;

    using NUnit.Framework;

    using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

    [TestFixture]
    public class QueryResultsContainerTests : TestBase
    {
        private QueryResultsContainer _queryResultContainer;

        [Test]
        public void AddOne()
        {
            _queryResultContainer = new QueryResultsContainer(QuestionType.Always);

            _queryResultContainer.AddOne(QueryResult.Undefined);

            int result = _queryResultContainer.Count();

            Assert.AreEqual(1, result);
        }

        [Test]
        public void AddMany()
        {
            _queryResultContainer = new QueryResultsContainer(QuestionType.Always);

            int many = 5; //most random number
            for (int i = 0; i < many; ++i)
            {
                _queryResultContainer.AddOne(QueryResult.Undefined);
            }

            int result = _queryResultContainer.Count();

            Assert.AreEqual(many, result);
        }

        [Test]
        public void CanAnswerEverOnTrue()
        {
            _queryResultContainer = new QueryResultsContainer(QuestionType.Ever);
            _queryResultContainer.AddOne(QueryResult.True);
            _queryResultContainer.AddOne(QueryResult.False);
            _queryResultContainer.AddOne(QueryResult.False);

            bool result = _queryResultContainer.CanQuickAnswer();

            Assert.AreEqual(true, result);
        }

        [Test]
        public void CanAnswerEverOnFalse()
        {
            _queryResultContainer = new QueryResultsContainer(QuestionType.Ever);
            _queryResultContainer.AddOne(QueryResult.False);
            _queryResultContainer.AddOne(QueryResult.False);

            bool result = _queryResultContainer.CanQuickAnswer();

            Assert.AreEqual(false, result);
        }

        [Test]
        public void CanAnswerAlwaysOnTrue()
        {
            _queryResultContainer = new QueryResultsContainer(QuestionType.Always);
            _queryResultContainer.AddOne(QueryResult.True);
            _queryResultContainer.AddOne(QueryResult.True);
            _queryResultContainer.AddOne(QueryResult.False);

            bool result = _queryResultContainer.CanQuickAnswer();

            Assert.AreEqual(true, result);
        }

        [Test]
        public void CanAnswerAlwaysOnFalse()
        {
            _queryResultContainer = new QueryResultsContainer(QuestionType.Always);
            _queryResultContainer.AddOne(QueryResult.True);
            _queryResultContainer.AddOne(QueryResult.True);
            _queryResultContainer.AddOne(QueryResult.True);

            bool result = _queryResultContainer.CanQuickAnswer();

            Assert.AreEqual(false, result);
        }

        [Test]
        public void CollectAnswerForEverQueryResultTrue()
        {
            _queryResultContainer = new QueryResultsContainer(QuestionType.Ever);
            _queryResultContainer.AddOne(QueryResult.True);
            _queryResultContainer.AddOne(QueryResult.False);

            QueryResult result = _queryResultContainer.CollectResults();

            Assert.AreEqual(QueryResult.True, result);
        }

        [Test]
        public void CollectAnswerForEverQueryResultFalse()
        {
            _queryResultContainer = new QueryResultsContainer(QuestionType.Ever);
            _queryResultContainer.AddOne(QueryResult.False);
            _queryResultContainer.AddOne(QueryResult.False);

            QueryResult result = _queryResultContainer.CollectResults();

            Assert.AreEqual(QueryResult.False, result);
        }

        [Test]
        public void CollectAnswerForAlwaysQueryResultTrue()
        {
            _queryResultContainer = new QueryResultsContainer(QuestionType.Always);
            _queryResultContainer.AddOne(QueryResult.True);
            _queryResultContainer.AddOne(QueryResult.True);

            QueryResult result = _queryResultContainer.CollectResults();

            Assert.AreEqual(QueryResult.True, result);
        }

        [Test]
        public void CollectAnswerForAlwaysQueryResultFalse()
        {
            _queryResultContainer = new QueryResultsContainer(QuestionType.Always);
            _queryResultContainer.AddOne(QueryResult.True);
            _queryResultContainer.AddOne(QueryResult.False);

            QueryResult result = _queryResultContainer.CollectResults();

            Assert.AreEqual(QueryResult.False, result);
        }
    }
}