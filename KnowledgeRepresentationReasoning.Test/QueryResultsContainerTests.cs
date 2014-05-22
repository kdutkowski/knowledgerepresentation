namespace KnowledgeRepresentationReasoning.Test
{
    using KnowledgeRepresentationReasoning.Queries;

    using NUnit.Framework;

    using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

    [TestFixture]
    public class QueryResultsContainerTests:TestBase
    {
        private QueryResultsContainer _queryResultContainer;

        [Test]
        public void AddManyAddOne()
        {
            _queryResultContainer = new QueryResultsContainer(QuestionType.Always);

            _queryResultContainer.AddMany(QueryResult.Undefined);

            int result = _queryResultContainer.Count();

            Assert.AreEqual(1, result);
        }

        [Test]
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

        [Test]
        public void CanAnswerEverOnTrue()
        {
            _queryResultContainer = new QueryResultsContainer(QuestionType.Ever);
            _queryResultContainer.AddMany(QueryResult.True);
            _queryResultContainer.AddMany(QueryResult.False);
            _queryResultContainer.AddMany(QueryResult.False);

            bool result = _queryResultContainer.CanQuickAnswer();

            Assert.AreEqual(true, result);
        }

        [Test]
        public void CanAnswerEverOnFalse()
        {
            _queryResultContainer = new QueryResultsContainer(QuestionType.Ever);
            _queryResultContainer.AddMany(QueryResult.False);
            _queryResultContainer.AddMany(QueryResult.False);

            bool result = _queryResultContainer.CanQuickAnswer();

            Assert.AreEqual(false, result);
        }

        [Test]
        public void CanAnswerAlwaysOnTrue()
        {
            _queryResultContainer = new QueryResultsContainer(QuestionType.Always);
            _queryResultContainer.AddMany(QueryResult.True);
            _queryResultContainer.AddMany(QueryResult.True);
            _queryResultContainer.AddMany(QueryResult.False);

            bool result = _queryResultContainer.CanQuickAnswer();

            Assert.AreEqual(true, result);
        }

        [Test]
        public void CanAnswerAlwaysOnFalse()
        {
            _queryResultContainer = new QueryResultsContainer(QuestionType.Always);
            _queryResultContainer.AddMany(QueryResult.True);
            _queryResultContainer.AddMany(QueryResult.True);
            _queryResultContainer.AddMany(QueryResult.True);

            bool result = _queryResultContainer.CanQuickAnswer();

            Assert.AreEqual(false, result);
        }

        [Test]
        public void CollectAnswerForEverQueryResultTrue()
        {
            _queryResultContainer = new QueryResultsContainer(QuestionType.Ever);
            _queryResultContainer.AddMany(QueryResult.True);
            _queryResultContainer.AddMany(QueryResult.False);

            QueryResult result = _queryResultContainer.CollectResults();

            Assert.AreEqual(QueryResult.True, result);
        }

        [Test]
        public void CollectAnswerForEverQueryResultFalse()
        {
            _queryResultContainer = new QueryResultsContainer(QuestionType.Ever);
            _queryResultContainer.AddMany(QueryResult.False);
            _queryResultContainer.AddMany(QueryResult.False);

            QueryResult result = _queryResultContainer.CollectResults();

            Assert.AreEqual(QueryResult.False, result);
        }

        [Test]
        public void CollectAnswerForAlwaysQueryResultTrue()
        {
            _queryResultContainer = new QueryResultsContainer(QuestionType.Always);
            _queryResultContainer.AddMany(QueryResult.True);
            _queryResultContainer.AddMany(QueryResult.True);

            QueryResult result = _queryResultContainer.CollectResults();

            Assert.AreEqual(QueryResult.True, result);
        }

        [Test]
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
