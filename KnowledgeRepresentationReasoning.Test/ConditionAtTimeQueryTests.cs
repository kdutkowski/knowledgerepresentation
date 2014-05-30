namespace KnowledgeRepresentationReasoning.Test
{
    using System.Collections.Generic;

    using KnowledgeRepresentationReasoning.Queries;
    using KnowledgeRepresentationReasoning.World;

    using NUnit.Framework;

    [TestFixture]
    public class ConditionAtTimeQueryTests : TestBase
    {
        private ConditionAtTimeQuery _query;

        [Test]
        public void CheckConditionAtTimeWithTimeTrue()
        {
            const int Time = 0;
            _query = new ConditionAtTimeQuery(QuestionType.Ever, "aa || bb", Time);
            var state = new State();
            var fluentList = new List<Fluent>
                             {
                                 new Fluent("aa", true),
                                 new Fluent("bb", false)
                             };
            state.Fluents.AddRange(fluentList);

            QueryResult result = _query.CheckCondition(state, null, Time);

            Assert.AreEqual(QueryResult.True, result);
        }

        [Test]
        public void CheckConditionAtTimeWithTimeFalse()
        {
            const int Time = 0;
            _query = new ConditionAtTimeQuery(QuestionType.Ever, "aa && bb", Time);
            var state = new State();
            var fluentList = new List<Fluent>
                             {
                                 new Fluent("aa", true),
                                 new Fluent("bb", false)
                             };
            state.Fluents.AddRange(fluentList);

            QueryResult result = _query.CheckCondition(state, null, Time);

            Assert.AreEqual(QueryResult.False, result);
        }

        [Test]
        public void CheckConditionAtTimeNoTimeTrue()
        {
            const int Time = 0;
            _query = new ConditionAtTimeQuery(QuestionType.Ever, "aa || bb");
            var state = new State();
            var fluentList = new List<Fluent>
                             {
                                 new Fluent("aa", true),
                                 new Fluent("bb", false)
                             };
            state.Fluents.AddRange(fluentList);

            QueryResult result = _query.CheckCondition(state, null, Time);

            Assert.AreEqual(QueryResult.True, result);
        }

        [Test]
        public void CheckConditionAtTimeNoTimeQueryResultUndefined()
        {
            const int Time = 0;
            _query = new ConditionAtTimeQuery(QuestionType.Ever, "aa && bb");
            var state = new State();
            var fluentList = new List<Fluent>
                             {
                                 new Fluent("aa", true), 
                                 new Fluent("bb", false)
                             };
            state.Fluents.AddRange(fluentList);

            QueryResult result = _query.CheckCondition(state, null, Time);

            Assert.AreEqual(QueryResult.Undefined, result);
        }

        [Test]
        public void CheckConditionAtTimeBeforeTime()
        {
            const int Time = 10;
            _query = new ConditionAtTimeQuery(QuestionType.Ever, "aa && bb", Time);
            var state = new State();
            var fluentList = new List<Fluent>
                             {
                                 new Fluent("aa", true),
                                 new Fluent("bb", false)
                             };
            state.Fluents.AddRange(fluentList);

            QueryResult result = _query.CheckCondition(state, null, Time-1);

            Assert.AreEqual(QueryResult.Undefined, result);
        }

        [Test]
        public void CheckConditionAtTimeAfterTimeFalse()
        {
            const int Time = 10;
            _query = new ConditionAtTimeQuery(QuestionType.Ever, "aa && bb", Time-1);
            var state = new State();
            var fluentList = new List<Fluent>
                             {
                                 new Fluent("aa", true), 
                                 new Fluent("bb", false)
                             };
            state.Fluents.AddRange(fluentList);

            QueryResult result = _query.CheckCondition(state, null, Time);

            Assert.AreEqual(QueryResult.False, result);
        }
    }
}
