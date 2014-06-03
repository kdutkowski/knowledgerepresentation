namespace KnowledgeRepresentationReasoning.Test
{
    using System.Collections.Generic;

    using KnowledgeRepresentationReasoning.Queries;
    using KnowledgeRepresentationReasoning.World;

    using NUnit.Framework;
    using KnowledgeRepresentationReasoning.Logic;

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

            Vertex vertex = new Vertex(state, null, Time, null);

            QueryResult result = _query.CheckCondition(vertex);

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

            Vertex vertex = new Vertex(state, null, Time, null);

            QueryResult result = _query.CheckCondition(vertex);

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

            Vertex vertex = new Vertex(state, null, Time, null);

            QueryResult result = _query.CheckCondition(vertex);

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
            Vertex vertex = new Vertex(state, null, Time, null);

            QueryResult result = _query.CheckCondition(vertex);

            Assert.AreEqual(QueryResult.Undefined, result);
        }

        [Test]
        public void CheckConditionAtTimeBeforeTime()
        {
            const int Time = 10;
            _query = new ConditionAtTimeQuery(QuestionType.Ever, "aa && bb", Time+1);
            var state = new State();
            var fluentList = new List<Fluent>
                             {
                                 new Fluent("aa", true),
                                 new Fluent("bb", false)
                             };
            state.Fluents.AddRange(fluentList);
            Vertex vertex = new Vertex(state, null, Time, null);

            QueryResult result = _query.CheckCondition(vertex);

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
            Vertex vertex = new Vertex(state, null, Time, null);

            QueryResult result = _query.CheckCondition(vertex);

            Assert.AreEqual(QueryResult.False, result);
        }
    }
}
