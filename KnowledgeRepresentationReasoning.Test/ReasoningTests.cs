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

        [Test]
        public void Reasoning_ExecutableScenarioQuery_Ever_Basic_Test()
        {
            var result = _reasoning.ExecuteQuery(new ExecutableScenarioQuery(QuestionType.Ever, _scenarioDescription), _scenarioDescription);
            Assert.Equals(QueryResult.True, result);
        }

        [Test]
        public void Reasoning_ExecutableScenarioQuery_Ever_Allways_Test()
        {
            var result = _reasoning.ExecuteQuery(new ExecutableScenarioQuery(QuestionType.Always, _scenarioDescription), _scenarioDescription);
            Assert.Equals(QueryResult.True, result);
        }
    }
}