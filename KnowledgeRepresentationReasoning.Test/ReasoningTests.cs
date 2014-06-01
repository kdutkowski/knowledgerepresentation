namespace KnowledgeRepresentationReasoning.Test
{
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
            _reasoning = new Reasoning();
            _reasoning.AddWorldDescriptionRecord(new InitialRecord("a || b || c || d"));                                        // (0)
            _reasoning.AddWorldDescriptionRecord(new ActionCausesIfRecord(this._actionA2, "!c", "c"));                          // (1)
            _reasoning.AddWorldDescriptionRecord(new ActionCausesIfRecord(this._actionB3, "a || b", "d"));                      // (2)
            _reasoning.AddWorldDescriptionRecord(new ActionInvokesAfterIfRecord(this._actionB3, this._actionC3, 5, "a"));       // (3)
            _reasoning.AddWorldDescriptionRecord(new ActionInvokesAfterIfRecord(this._actionB3, this._actionD1, 1, "c"));       // (4)
            _reasoning.AddWorldDescriptionRecord(new ActionReleasesIfRecord(this._actionD1, new Fluent("d", true), ""));        // (5)
            _reasoning.AddWorldDescriptionRecord(new ExpressionTriggersActionRecord(this._actionC5, "a && !c && d"));           // (6)
            _reasoning.AddWorldDescriptionRecord(new ImpossibleActionAtRecord(this._actionC5, 50));                             // (7)
            _reasoning.AddWorldDescriptionRecord(new ImpossibleActionIfRecord(this._actionA2, "!a && !b"));                     // (8)
            _reasoning.AddWorldDescriptionRecord(new ActionInvokesAfterIfRecord(this._actionA2, this._actionC5, 12, "d"));      // (9)
            _reasoning.AddWorldDescriptionRecord(new ActionReleasesIfRecord(this._actionC5, new Fluent("c", true), "d || b"));  // (10)
        }

        public void SetComplexScenario_A()
        {
            _scenarioDescription = new ScenarioDescription("complexA");
            _scenarioDescription.addObservation(new SimpleLogicExpression("a && b && c && d"), 1);
            _scenarioDescription.addACS(this._actionB3, 3);
            _scenarioDescription.addACS(this._actionA2, 20);
            _scenarioDescription.addObservation(new SimpleLogicExpression("a && !b && !c && d"), 24);   // (1,0,0,1)

            /* Wynik dla ComplexWorldDescription_A
             * 
             * Obserwacja czas 1: a = 1, b = 1, c = 1, d = 1 czyli (1,1,1,1)
             * Akcja czas 3: (B,3) 
             *      Rezultaty:
             *              0) Czas 6: Możliwe stany: (0,1,1,1), (1,1,1,1), (1,0,1,1)
             *              1) Czas 7: Rozpoczyna się akcja (D,1)
             *              2) Czas 8: Zwalniany jest fluent d - 
             *                  Możliwe stany: 
             *                      (0,1,1,1), (0,1,1,0),
             *                      (1,1,1,1), (1,1,1,0),
             *                      (1,0,1,1), (1,0,1,0)
             *              3) Czas 11: Rozpoczyna się akcja (C,3)
             * Akcja czas 20: (A,2)
             *      Rezultaty:
             *              1) Czas 22: Możliwe stany:
             *                      (0,1,0,1), (0,1,0,0),
             *                      (1,1,0,1), (1,1,0,0),
             *                      (1,0,0,1), (1,0,0,0)
             *              2) Czas 22: Rozpoczyna się akcja (C,5)
             *              3) Czas 34: Rozpoczyna się akcja (C,5)
             *              
             */
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

        [Test]
        public void Reasoning_ExecutableScenarioQuery_Always_Complex_False_Test()
        {
            // Set scenario and description
            this.SetComplexWorldDescription_A();
            this.SetComplexScenario_A();

            // Test
            var result = _reasoning.ExecuteQuery(new ExecutableScenarioQuery(QuestionType.Always, _scenarioDescription), _scenarioDescription);
            Assert.AreEqual(QueryResult.True, result);
        }

        #endregion

        #region AccesibleConditionQuery

        [Test]
        public void Reasoning_AccesibleConditionQuery_Ever_Complex_True_Test()
        {
            // Set scenario and description
            this.SetComplexWorldDescription_A();
            this.SetComplexScenario_A();

            // Test
            // Condition is satisfied in time 1
            var result = _reasoning.ExecuteQuery(new AccesibleConditionQuery(QuestionType.Ever, "a && b && c && d", _scenarioDescription), _scenarioDescription);
            Assert.AreEqual(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_AccesibleConditionQuery_Always_Complex_False_Test()
        {
            // Set scenario and description
            this.SetComplexWorldDescription_A();
            this.SetComplexScenario_A();

            // Test
            var result = _reasoning.ExecuteQuery(new AccesibleConditionQuery(QuestionType.Always, "!a && b && c && d", _scenarioDescription), _scenarioDescription);
            Assert.AreEqual(QueryResult.False, result);
        }

        [Test]
        public void Reasoning_AccesibleConditionQuery_Ever_Complex_TrueAtTheEnd_Test()
        {
            // Set scenario and description
            this.SetComplexWorldDescription_A();
            this.SetComplexScenario_A();

            // Test
            var result = _reasoning.ExecuteQuery(new AccesibleConditionQuery(QuestionType.Ever, "a && !b && !c && d", _scenarioDescription), _scenarioDescription);
            Assert.AreEqual(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_AccesibleConditionQuery_Always_Complex_FalseAtTheEnd_Test()
        {
            // Set scenario and description
            this.SetComplexWorldDescription_A();
            this.SetComplexScenario_A();

            // Test
            var result = _reasoning.ExecuteQuery(new AccesibleConditionQuery(QuestionType.Always, "c", _scenarioDescription), _scenarioDescription);
            Assert.AreEqual(QueryResult.True, result);
        }

        #endregion

        #region PerformingActionAtTimeQuery

        [Test]
        public void Reasoning_PerformingActionAtTimeQuery_Ever_Complex_ActionInProgress_Test()
        {
            // Set scenario and description
            this.SetComplexWorldDescription_A();
            this.SetComplexScenario_A();

            // Test 1
            var result = _reasoning.ExecuteQuery(new PerformingActionAtTimeQuery(QuestionType.Always, this._actionC5, 24), _scenarioDescription);
            Assert.AreEqual(QueryResult.True, result);

            // Test 2
            result = _reasoning.ExecuteQuery(new PerformingActionAtTimeQuery(QuestionType.Always, this._actionC5, 35), _scenarioDescription);
            Assert.AreEqual(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_PerformingActionAtTimeQuery_Ever_Complex_ActionEnded_Test()
        {
            // Set scenario and description
            this.SetComplexWorldDescription_A();
            this.SetComplexScenario_A();

            // Test
            var result = _reasoning.ExecuteQuery(new PerformingActionAtTimeQuery(QuestionType.Always, this._actionC3, 14), _scenarioDescription);
            Assert.AreEqual(QueryResult.False, result);
        }

        #endregion

        #endregion
    }
}