using System;
using System.Collections.Generic;
using KnowledgeRepresentationReasoning.Expressions;
using KnowledgeRepresentationReasoning.Queries;
using KnowledgeRepresentationReasoning.Scenario;
using KnowledgeRepresentationReasoning.World;
using KnowledgeRepresentationReasoning.World.Records;
using NUnit.Framework;

namespace KnowledgeRepresentationReasoning.Test.DocumentationExamples
{
    [TestFixture]
    public class Example4 : TestBase
    {
        private Reasoning _reasoning;
        private ScenarioDescription _scenarioDescription;

        #region | ACTIONS |
        private readonly WorldAction _clicksButtonAction = new WorldAction
        {
            Id = "clicksButton",
            Duration = 1
        };
        private readonly WorldAction _switchesOnComputerAction = new WorldAction
        {
            Id = "switchesOnComputer",
            Duration = 2
        };
        private readonly WorldAction _disconnectsPowerAction = new WorldAction
        {
            Id = "disconnectsPower",
            Duration = 1
        };
        #endregion

        #region | FLUENTS |
        private Fluent _connectPowerComputerFluent = new Fluent()
        {
            Name = "connectPowerComputer"
        };
        private Fluent _onComputerFluent = new Fluent()
        {
            Name = "onComputer"
        };
        List<Fluent> _listFluent = new List<Fluent>();
        #endregion

        #region | INITIALIZE |

        public Example4()
        {
            _listFluent = new List<Fluent>();
            _listFluent.Add(_connectPowerComputerFluent);
            _listFluent.Add(_onComputerFluent);
        }

        [SetUp]
        public void SetUp()
        {
            _reasoning = new Reasoning();
            foreach(Fluent item in _listFluent)
            {
                _reasoning.AddWorldDescriptionRecord(new InitialRecord(item.Name.ToString()));
            }
            _reasoning.AddWorldDescriptionRecord(new ActionCausesIfRecord(_switchesOnComputerAction, _onComputerFluent.Name.ToString(), String.Empty));
            _reasoning.AddWorldDescriptionRecord(new ActionCausesIfRecord(_disconnectsPowerAction, "!" + _connectPowerComputerFluent.Name.ToString(), String.Empty));
            _reasoning.AddWorldDescriptionRecord(new ActionCausesIfRecord(_disconnectsPowerAction, "!" + _onComputerFluent.Name.ToString(), String.Empty));
            _reasoning.AddWorldDescriptionRecord(new ActionInvokesAfterIfRecord(_clicksButtonAction, _switchesOnComputerAction, 0, _connectPowerComputerFluent.Name.ToString()));

            _scenarioDescription = new ScenarioDescription("ScenarioDescription");

            _scenarioDescription.addObservation(new SimpleLogicExpression("!" + _onComputerFluent.Name.ToString()), 0);
            _scenarioDescription.addObservation(new SimpleLogicExpression(_connectPowerComputerFluent.Name.ToString()), 0);
            _scenarioDescription.addACS(_clicksButtonAction, 1);
            _scenarioDescription.addACS(_disconnectsPowerAction, 4);
            _scenarioDescription.addACS(_clicksButtonAction, 5);
        }
        #endregion

        #region | EXAMPLE 4 TEXT|
        //Mamy Billa oraz komputer. Bill moze nacisnac przycisk Włacz lub odłaczyc komputer od zasilania.
        //Poczatkowo komputer jest wyłaczony i podłaczony do zasilania. Jezeli zostanie nacisniety jego przycisk
        //Włacz oraz komputer jest podłaczony do zasilania, to komputer właczy sie. Odłaczenie komputera od
        //pradu powoduje, ze komputer bedzie odłaczony od zasilania oraz wyłaczony.

        //4.4.2 Opis akcji
        //(clicks button on; 1) invokes (switches on computer; 2) after 0 if connect power computer
        //(switches on computer; 2) causes on computer
        //(disconnects power; 1) causes !connect power computer
        //(disconnects power; 1) causes !on computer

        //4.4.3 Scenariusz
        //Sc =(OBS; ACS)
        //OBS = f(!on computer; 0); (connect power computer; 0)g
        //ACS = f(clicks button on; 1); 1); ((disconnects power; 1); 4); ((clicks button on; 1); 5)g

        //4.4.4 Kwerendy
        //1. on computer at 7 when Sc
        //2. accesible on computer when Sc
        #endregion

        #region | TESTS |
        [Test]
        public void First_Query_at_7_when_Ever_Test()
        {
            //Arrange
            QueryResult expectedResult = QueryResult.False;
            //Act
            QueryResult result = _reasoning.ExecuteQuery(new ConditionAtTimeQuery(QuestionType.Ever, _onComputerFluent.Name.ToString(), 7), _scenarioDescription);
            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Second_Query_accesible_when_Ever_Test()
        {
            //Arrange
            QueryResult expectedResult = QueryResult.True;
            //Act
            QueryResult result = _reasoning.ExecuteQuery(new ConditionAtTimeQuery(QuestionType.Ever, _onComputerFluent.Name.ToString()), _scenarioDescription);
            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void First_Query_at_7_when_Always_Test()
        {
            //Arrange
            QueryResult expectedResult = QueryResult.False;
            //Act
            QueryResult result = _reasoning.ExecuteQuery(new ConditionAtTimeQuery(QuestionType.Always, _onComputerFluent.Name.ToString(), 7), _scenarioDescription);
            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Second_Query_accesible_when_Always_Test()
        {
            //Arrange
            QueryResult expectedResult = QueryResult.True;
            //Act
            QueryResult result = _reasoning.ExecuteQuery(new ConditionAtTimeQuery(QuestionType.Always, _onComputerFluent.Name.ToString()), _scenarioDescription);
            //Assert
            Assert.AreEqual(expectedResult, result);
        }
        #endregion
    }
}
