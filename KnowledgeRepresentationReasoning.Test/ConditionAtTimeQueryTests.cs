using KnowledgeRepresentationReasoning.Queries;
using KnowledgeRepresentationReasoning.World;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace KnowledgeRepresentationReasoning.Test
{
    [TestClass]
    public class ConditionAtTimeQueryTests : TestBase
    {
        private ConditionAtTimeQuery _query;

        public ConditionAtTimeQueryTests() :base(){ }

        [TestInitialize()]
        public void MyTestInitialize() { }

        [TestMethod]
        public void CheckConditionAtTimeWithTimeTrue()
        {
            int time = 0;
            _query = new ConditionAtTimeQuery(QuestionType.Ever, "aa || bb", time);
            State state = new State();
            List<Fluent> fluentList = new List<Fluent>() { new Fluent("aa", true), new Fluent("bb", false) };
            state.Fluents.AddRange(fluentList);

            QueryResult result = _query.CheckCondition(state, null, time);

            Assert.AreEqual(QueryResult.True, result);
        }
        
        [TestMethod]
        public void CheckConditionAtTimeWithTimeFalse()
        {
            int time = 0;
            _query = new ConditionAtTimeQuery(QuestionType.Ever, "aa && bb", time);
            State state = new State();
            List<Fluent> fluentList = new List<Fluent>() { new Fluent("aa", true), new Fluent("bb", false) };
            state.Fluents.AddRange(fluentList);

            QueryResult result = _query.CheckCondition(state, null, time);

            Assert.AreEqual(QueryResult.False, result);
        }

        [TestMethod]
        public void CheckConditionAtTimeNoTimeTrue()
        {
            int time = 0;
            _query = new ConditionAtTimeQuery(QuestionType.Ever, "aa || bb");
            State state = new State();
            List<Fluent> fluentList = new List<Fluent>() { new Fluent("aa", true), new Fluent("bb", false) };
            state.Fluents.AddRange(fluentList);

            QueryResult result = _query.CheckCondition(state, null, time);

            Assert.AreEqual(QueryResult.True, result);
        }

        [TestMethod]
        public void CheckConditionAtTimeNoTimeFalse()
        {
            int time = 0;
            _query = new ConditionAtTimeQuery(QuestionType.Ever, "aa && bb");
            State state = new State();
            List<Fluent> fluentList = new List<Fluent>() { new Fluent("aa", true), new Fluent("bb", false) };
            state.Fluents.AddRange(fluentList);

            QueryResult result = _query.CheckCondition(state, null, time);

            Assert.AreEqual(QueryResult.False, result);
        }

        [TestMethod]
        public void CheckConditionAtTimeBeforeTime()
        {
            int time = 10;
            _query = new ConditionAtTimeQuery(QuestionType.Ever, "aa && bb", time);
            State state = new State();
            List<Fluent> fluentList = new List<Fluent>() { new Fluent("aa", true), new Fluent("bb", false) };
            state.Fluents.AddRange(fluentList);

            QueryResult result = _query.CheckCondition(state, null, time-1);

            Assert.AreNotEqual(QueryResult.False, result);
            Assert.AreNotEqual(QueryResult.True, result);
        }

        [TestMethod]
        public void CheckConditionAtTimeAfterTime()
        {
            int time = 10;
            _query = new ConditionAtTimeQuery(QuestionType.Ever, "aa && bb", time-1);
            State state = new State();
            List<Fluent> fluentList = new List<Fluent>() { new Fluent("aa", true), new Fluent("bb", false) };
            state.Fluents.AddRange(fluentList);

            QueryResult result = _query.CheckCondition(state, null, time);

            Assert.AreEqual(QueryResult.False, result);
        }
    }
}
