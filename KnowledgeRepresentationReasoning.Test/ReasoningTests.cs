namespace KnowledgeRepresentationReasoning.Test
{
    using System;
    using System.Linq;

    using KnowledgeRepresentationReasoning.Expressions;
    using KnowledgeRepresentationReasoning.Queries;
    using KnowledgeRepresentationReasoning.Scenario;
    using KnowledgeRepresentationReasoning.World;
    using KnowledgeRepresentationReasoning.World.Records;

    using NUnit.Framework;

    [TestFixture]
    public class ReasoningTests : TestBase
    {
        private Reasoning _reasoning;
        private WorldDescription _worldDescription;
        private readonly WorldAction _actionA2 = new WorldAction { Duration = 2, Id = "A" };
        private readonly WorldAction _actionB3 = new WorldAction { Duration = 3, Id = "B" };
        private ScenarioDescription _scenarioDescription;

        [SetUp]
        public void SetUp()
        {
            // BASIC WORLD DESCRIPTION
            _worldDescription = new WorldDescription();
            _worldDescription.Descriptions.Add(new Tuple<WorldDescriptionRecordType, WorldDescriptionRecord>(WorldDescriptionRecordType.Initially, new InitialRecord("a || b || c || d")));
            _worldDescription.Descriptions.Add(new Tuple<WorldDescriptionRecordType, WorldDescriptionRecord>(WorldDescriptionRecordType.ActionCausesIf, new ActionCausesIfRecord(this._actionA2, "a && !b", "c")));
            _worldDescription.Descriptions.Add(new Tuple<WorldDescriptionRecordType, WorldDescriptionRecord>(WorldDescriptionRecordType.ActionCausesIf, new ActionCausesIfRecord(this._actionB3, "a || b", "d")));

            // BASIC SCENARIO
            _scenarioDescription = new ScenarioDescription("basic");
            _scenarioDescription.addObservation(new SimpleLogicExpression("a && b && c && d"), 0);
            _scenarioDescription.addACS(this._actionA2, 1);
            _scenarioDescription.addObservation(new SimpleLogicExpression("a && !b && c && d"), 4);

            _reasoning = new Reasoning();

            foreach (var description in _worldDescription.Descriptions.Select(t => t.Item2))
                _reasoning.AddWorldDescriptionRecord(description);
        }

        #region | BASIC TESTS - SIMPLE |

        #region ExecutableScenarioQuery

        [Test]
        public void Reasoning_ExecutableScenarioQuery_Ever_Basic_True_Test()
        {
            var result = _reasoning.ExecuteQuery(new ExecutableScenarioQuery(QuestionType.Ever, _scenarioDescription), _scenarioDescription);
            Assert.Equals(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_ExecutableScenarioQuery_Always_Basic_True_Test()
        {
            var result = _reasoning.ExecuteQuery(new ExecutableScenarioQuery(QuestionType.Always, _scenarioDescription), _scenarioDescription);
            Assert.Equals(QueryResult.True, result);
        }

        #endregion

        #region AccesibleConditionQuery

        [Test]
        public void Reasoning_AccesibleConditionQuery_Ever_Basic_TrueInTimeZero_Test()
        {
            // Condition is satisfied in time 0
            var result = _reasoning.ExecuteQuery(new AccesibleConditionQuery(QuestionType.Ever, "a && b && c && d", _scenarioDescription), _scenarioDescription);
            Assert.Equals(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_AccesibleConditionQuery_Always_Basic_TrueInTimeZero_Test()
        {
            // Condition is satisfied in time 0
            var result = _reasoning.ExecuteQuery(new AccesibleConditionQuery(QuestionType.Always, "a && b && c && d", _scenarioDescription), _scenarioDescription);
            Assert.Equals(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_AccesibleConditionQuery_Ever_Basic_True_Test()
        {
            // Condition should be satisfied in time 3 and 4 and probably later
            var result = _reasoning.ExecuteQuery(new AccesibleConditionQuery(QuestionType.Ever, "a && !b && c && d", _scenarioDescription), _scenarioDescription);
            Assert.Equals(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_AccesibleConditionQuery_Always_Basic_True_Test()
        {
            // Condition should be satisfied in time 3 and 4 and probably later
            var result = _reasoning.ExecuteQuery(new AccesibleConditionQuery(QuestionType.Always, "a && !b && c && d", _scenarioDescription), _scenarioDescription);
            Assert.Equals(QueryResult.True, result);
        }

        #endregion

        #region PerformingActionAtTimeQuery

        [Test]
        public void Reasoning_PerformingActionAtTimeQuery_Always_Basic_True_ActionInProgress_Test()
        {
            var result = _reasoning.ExecuteQuery(new PerformingActionAtTimeQuery(QuestionType.Always, _actionA2, 2), _scenarioDescription);
            Assert.Equals(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_PerformingActionAtTimeQuery_Always_Basic_True_ActionStart_Test()
        {
            var result = _reasoning.ExecuteQuery(new PerformingActionAtTimeQuery(QuestionType.Always, _actionA2, 1), _scenarioDescription);
            Assert.Equals(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_PerformingActionAtTimeQuery_Always_Basic_True_ActionEnd_Test()
        {
            var result = _reasoning.ExecuteQuery(new PerformingActionAtTimeQuery(QuestionType.Always, _actionA2, 3), _scenarioDescription);
            Assert.Equals(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_PerformingActionAtTimeQuery_Ever_Basic_True_ActionInProgress_Test()
        {
            var result = _reasoning.ExecuteQuery(new PerformingActionAtTimeQuery(QuestionType.Ever, _actionA2, 2), _scenarioDescription);
            Assert.Equals(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_PerformingActionAtTimeQuery_Ever_Basic_True_ActionStart_Test()
        {
            var result = _reasoning.ExecuteQuery(new PerformingActionAtTimeQuery(QuestionType.Ever, _actionA2, 1), _scenarioDescription);
            Assert.Equals(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_PerformingActionAtTimeQuery_Ever_Basic_True_ActionEnd_Test()
        {
            var result = _reasoning.ExecuteQuery(new PerformingActionAtTimeQuery(QuestionType.Ever, _actionA2, 3), _scenarioDescription);
            Assert.Equals(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_PerformingActionAtTimeQuery_Always_Basic_False_Test()
        {
            var result = _reasoning.ExecuteQuery(new PerformingActionAtTimeQuery(QuestionType.Always, _actionA2, 0), _scenarioDescription);
            Assert.Equals(QueryResult.False, result);
        }

        [Test]
        public void Reasoning_PerformingActionAtTimeQuery_Ever_Basic_False_Test()
        {
            var result = _reasoning.ExecuteQuery(new PerformingActionAtTimeQuery(QuestionType.Ever, _actionA2, 0), _scenarioDescription);
            Assert.Equals(QueryResult.False, result);
        }

        #endregion

        #region ConditionAtTimeQuery

        // EVER

        [Test]
        public void Reasoning_ConditionAtTimeQuery_Ever_Basic_TrueInTimeZero_Test()
        {
            var result = _reasoning.ExecuteQuery(new ConditionAtTimeQuery(QuestionType.Ever, "a && b && c && d", 0), _scenarioDescription);
            Assert.Equals(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_ConditionAtTimeQuery_Ever_Basic_FalseInTimeZero_Test()
        {
            var result = _reasoning.ExecuteQuery(new ConditionAtTimeQuery(QuestionType.Ever, "a && !b && c && d", 0), _scenarioDescription);
            Assert.Equals(QueryResult.False, result);
        }

        [Test]
        public void Reasoning_ConditionAtTimeQuery_Ever_Basic_True_Test()
        {
            var result = _reasoning.ExecuteQuery(new ConditionAtTimeQuery(QuestionType.Ever, "a && !b && c && d", 3), _scenarioDescription);
            Assert.Equals(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_ConditionAtTimeQuery_Ever_Basic_False_Test()
        {
            var result = _reasoning.ExecuteQuery(new ConditionAtTimeQuery(QuestionType.Ever, "a && b && c && d", 3), _scenarioDescription);
            Assert.Equals(QueryResult.True, result);
        }

        // ALWAYS

        [Test]
        public void Reasoning_ConditionAtTimeQuery_Always_Basic_TrueInTimeZero_Test()
        {
            var result = _reasoning.ExecuteQuery(new ConditionAtTimeQuery(QuestionType.Always, "a && b && c && d", 0), _scenarioDescription);
            Assert.Equals(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_ConditionAtTimeQuery_Always_Basic_FalseInTimeZero_Test()
        {
            var result = _reasoning.ExecuteQuery(new ConditionAtTimeQuery(QuestionType.Always, "a && !b && c && d", 0), _scenarioDescription);
            Assert.Equals(QueryResult.False, result);
        }

        [Test]
        public void Reasoning_ConditionAtTimeQuery_Always_Basic_True_Test()
        {
            var result = _reasoning.ExecuteQuery(new ConditionAtTimeQuery(QuestionType.Always, "a && !b && c && d", 3), _scenarioDescription);
            Assert.Equals(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_ConditionAtTimeQuery_Always_Basic_False_Test()
        {
            var result = _reasoning.ExecuteQuery(new ConditionAtTimeQuery(QuestionType.Always, "a && b && c && d", 3), _scenarioDescription);
            Assert.Equals(QueryResult.True, result);
        }

        #endregion

        #endregion
    }
}