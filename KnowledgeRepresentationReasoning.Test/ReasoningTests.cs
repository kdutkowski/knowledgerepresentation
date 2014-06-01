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
    using NUnit.Framework.Constraints;

    [TestFixture]
    public class ReasoningTests : TestBase
    {
        private Reasoning _reasoning;
        private ScenarioDescription _scenarioDescription;

        #region | ACTIONS |
        private readonly WorldAction _actionA2 = new WorldAction { Duration = 2, Id = "A" };
        private readonly WorldAction _actionB3 = new WorldAction { Duration = 3, Id = "B" };
        private readonly WorldAction _actionC3 = new WorldAction { Duration = 3, Id = "C" };
        private readonly WorldAction _actionC5 = new WorldAction { Duration = 5, Id = "C" };
        private readonly WorldAction _actionD1 = new WorldAction { Duration = 1, Id = "D" };
        #endregion

        [SetUp]
        public void SetUp()
        {
            // BASIC WORLD DESCRIPTION
            _reasoning = new Reasoning();
            _reasoning.AddWorldDescriptionRecord(new InitialRecord("a || b || c || d"));
            _reasoning.AddWorldDescriptionRecord(new ActionCausesIfRecord(this._actionA2, "a && !b", "c"));
            _reasoning.AddWorldDescriptionRecord(new ActionCausesIfRecord(this._actionB3, "a || b", "d"));

            // BASIC SCENARIO
            _scenarioDescription = new ScenarioDescription("basic");
            _scenarioDescription.addObservation(new SimpleLogicExpression("a && b && c && d"), 0);
            _scenarioDescription.addACS(this._actionA2, 1);
            _scenarioDescription.addObservation(new SimpleLogicExpression("a && !b && c && d"), 4);
        }

        #region | BASIC TESTS - SIMPLE |

        #region ExecutableScenarioQuery

        [Test]
        public void Reasoning_ExecutableScenarioQuery_Ever_Basic_True_Test()
        {
            var result = _reasoning.ExecuteQuery(new ExecutableScenarioQuery(QuestionType.Ever, _scenarioDescription), _scenarioDescription);
            Assert.AreEqual(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_ExecutableScenarioQuery_Always_Basic_True_Test()
        {
            var result = _reasoning.ExecuteQuery(new ExecutableScenarioQuery(QuestionType.Always, _scenarioDescription), _scenarioDescription);
            Assert.AreEqual(QueryResult.True, result);
        }

        #endregion

        #region AccesibleConditionQuery

        [Test]
        public void Reasoning_AccesibleConditionQuery_Ever_Basic_TrueInTimeZero_Test()
        {
            // Condition is satisfied in time 0
            var result = _reasoning.ExecuteQuery(new AccesibleConditionQuery(QuestionType.Ever, "a && b && c && d", _scenarioDescription), _scenarioDescription);
            Assert.AreEqual(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_AccesibleConditionQuery_Always_Basic_TrueInTimeZero_Test()
        {
            // Condition is satisfied in time 0
            var result = _reasoning.ExecuteQuery(new AccesibleConditionQuery(QuestionType.Always, "a && b && c && d", _scenarioDescription), _scenarioDescription);
            Assert.AreEqual(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_AccesibleConditionQuery_Ever_Basic_True_Test()
        {
            // Condition should be satisfied in time 3 and 4
            var result = _reasoning.ExecuteQuery(new AccesibleConditionQuery(QuestionType.Ever, "a && !b && c && d", _scenarioDescription), _scenarioDescription);
            Assert.AreEqual(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_AccesibleConditionQuery_Always_Basic_True_Test()
        {
            // Condition should be satisfied in time 3 and 4
            var result = _reasoning.ExecuteQuery(new AccesibleConditionQuery(QuestionType.Always, "a && !b && c && d", _scenarioDescription), _scenarioDescription);
            Assert.AreEqual(QueryResult.True, result);
        }

        #endregion

        #region PerformingActionAtTimeQuery

        [Test]
        public void Reasoning_PerformingActionAtTimeQuery_Always_Basic_True_ActionInProgress_Test()
        {
            var result = _reasoning.ExecuteQuery(new PerformingActionAtTimeQuery(QuestionType.Always, _actionA2, 2), _scenarioDescription);
            Assert.AreEqual(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_PerformingActionAtTimeQuery_Always_Basic_True_ActionStart_Test()
        {
            var result = _reasoning.ExecuteQuery(new PerformingActionAtTimeQuery(QuestionType.Always, _actionA2, 1), _scenarioDescription);
            Assert.AreEqual(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_PerformingActionAtTimeQuery_Always_Basic_False_ActionEnd_Test()
        {
            var result = _reasoning.ExecuteQuery(new PerformingActionAtTimeQuery(QuestionType.Always, _actionA2, 3), _scenarioDescription);
            Assert.AreEqual(QueryResult.False, result);
        }

        [Test]
        public void Reasoning_PerformingActionAtTimeQuery_Ever_Basic_True_ActionInProgress_Test()
        {
            var result = _reasoning.ExecuteQuery(new PerformingActionAtTimeQuery(QuestionType.Ever, _actionA2, 2), _scenarioDescription);
            Assert.AreEqual(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_PerformingActionAtTimeQuery_Ever_Basic_True_ActionStart_Test()
        {
            var result = _reasoning.ExecuteQuery(new PerformingActionAtTimeQuery(QuestionType.Ever, _actionA2, 1), _scenarioDescription);
            Assert.AreEqual(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_PerformingActionAtTimeQuery_Ever_Basic_False_ActionEnd_Test()
        {
            var result = _reasoning.ExecuteQuery(new PerformingActionAtTimeQuery(QuestionType.Ever, _actionA2, 3), _scenarioDescription);
            Assert.AreEqual(QueryResult.False, result);
        }

        [Test]
        public void Reasoning_PerformingActionAtTimeQuery_Always_Basic_False_Test()
        {
            var result = _reasoning.ExecuteQuery(new PerformingActionAtTimeQuery(QuestionType.Always, _actionA2, 0), _scenarioDescription);
            Assert.AreEqual(QueryResult.False, result);
        }

        [Test]
        public void Reasoning_PerformingActionAtTimeQuery_Ever_Basic_False_Test()
        {
            var result = _reasoning.ExecuteQuery(new PerformingActionAtTimeQuery(QuestionType.Ever, _actionA2, 0), _scenarioDescription);
            Assert.AreEqual(QueryResult.False, result);
        }

        #endregion

        #region ConditionAtTimeQuery

        // EVER

        [Test]
        public void Reasoning_ConditionAtTimeQuery_Ever_Basic_TrueInTimeZero_Test()
        {
            var result = _reasoning.ExecuteQuery(new ConditionAtTimeQuery(QuestionType.Ever, "a && b && c && d", 0), _scenarioDescription);
            Assert.AreEqual(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_ConditionAtTimeQuery_Ever_Basic_FalseInTimeZero_Test()
        {
            var result = _reasoning.ExecuteQuery(new ConditionAtTimeQuery(QuestionType.Ever, "a && !b && c && d", 0), _scenarioDescription);
            Assert.AreEqual(QueryResult.False, result);
        }

        [Test]
        public void Reasoning_ConditionAtTimeQuery_Ever_Basic_True_Test()
        {
            var result = _reasoning.ExecuteQuery(new ConditionAtTimeQuery(QuestionType.Ever, "a && !b && c && d", 3), _scenarioDescription);
            Assert.AreEqual(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_ConditionAtTimeQuery_Ever_Basic_False_Test()
        {
            var result = _reasoning.ExecuteQuery(new ConditionAtTimeQuery(QuestionType.Ever, "a && b && c && d", 3), _scenarioDescription);
            Assert.AreEqual(QueryResult.False, result);
        }

        // ALWAYS

        [Test]
        public void Reasoning_ConditionAtTimeQuery_Always_Basic_TrueInTimeZero_Test()
        {
            var result = _reasoning.ExecuteQuery(new ConditionAtTimeQuery(QuestionType.Always, "a && b && c && d", 0), _scenarioDescription);
            Assert.AreEqual(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_ConditionAtTimeQuery_Always_Basic_FalseInTimeZero_Test()
        {
            var result = _reasoning.ExecuteQuery(new ConditionAtTimeQuery(QuestionType.Always, "a && !b && c && d", 0), _scenarioDescription);
            Assert.AreEqual(QueryResult.False, result);
        }

        [Test]
        public void Reasoning_ConditionAtTimeQuery_Always_Basic_True_Test()
        {
            var result = _reasoning.ExecuteQuery(new ConditionAtTimeQuery(QuestionType.Always, "a && !b && c && d", 3), _scenarioDescription);
            Assert.AreEqual(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_ConditionAtTimeQuery_Always_Basic_False_Test()
        {
            var result = _reasoning.ExecuteQuery(new ConditionAtTimeQuery(QuestionType.Always, "a && b && c && d", 3), _scenarioDescription);
            Assert.AreEqual(QueryResult.False, result);
        }

        #endregion

        #endregion

        #region | COMPLEX TESTS |

        public void SetComplexWorldDescription_A()
        {
            _reasoning.AddWorldDescriptionRecord(new ActionCausesIfRecord(this._actionA2, "!c", "c"));
            _reasoning.AddWorldDescriptionRecord(new ActionCausesIfRecord(this._actionB3, "a || b", "d"));
            _reasoning.AddWorldDescriptionRecord(new ActionInvokesAfterIfRecord(this._actionB3, this._actionC3, 5, "a"));
            _reasoning.AddWorldDescriptionRecord(new ActionInvokesAfterIfRecord(this._actionB3, this._actionD1, 1, "c"));
            _reasoning.AddWorldDescriptionRecord(new ActionReleasesIfRecord(this._actionD1, new Fluent("d", true), ""));
            _reasoning.AddWorldDescriptionRecord(new ExpressionTriggersActionRecord(this._actionC5, "a && !c && d"));
            _reasoning.AddWorldDescriptionRecord(new ImpossibleActionAtRecord(this._actionC5, 50));
            _reasoning.AddWorldDescriptionRecord(new ImpossibleActionIfRecord(this._actionA2, "!a && !b"));
            _reasoning.AddWorldDescriptionRecord(new ActionInvokesAfterIfRecord(this._actionA2, this._actionC5, 12, "d"));
            _reasoning.AddWorldDescriptionRecord(new ActionReleasesIfRecord(this._actionC5, new Fluent("c", true), "d || b"));
        }

        public void SetComplexScenario_A()
        {
            _scenarioDescription = new ScenarioDescription("complexA");
            _scenarioDescription.addObservation(new SimpleLogicExpression("a && b && c && d"), 1);
            _scenarioDescription.addACS(this._actionB3, 3);
            _scenarioDescription.addACS(this._actionA2, 20);
            _scenarioDescription.addObservation(new SimpleLogicExpression("a && !b && !c && d"), 24);
        }

        #region ExecutableScenarioQuery

        [Test]
        public void Reasoning_ExecutableScenarioQuery_Ever_Complex_True_Test()
        {
            // Set scenario and description
            this.SetComplexWorldDescription_A();
            this.SetComplexScenario_A();

            // Test
            var result = _reasoning.ExecuteQuery(new ExecutableScenarioQuery(QuestionType.Ever, _scenarioDescription), _scenarioDescription);
            Assert.AreEqual(QueryResult.True, result);
        }

        /*
        [Test]
        public void Reasoning_ExecutableScenarioQuery_Always_Basic_True_Test()
        {
            var result = _reasoning.ExecuteQuery(new ExecutableScenarioQuery(QuestionType.Always, _scenarioDescription), _scenarioDescription);
            Assert.AreEqual(QueryResult.True, result);
        }
        */
        #endregion

        #endregion
    }
}