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
    public class Example1 : TestBase
    {
        private Reasoning _reasoning;
        private ScenarioDescription _scenarioDescription1;
        private ScenarioDescription _scenarioDescription2;

        #region | ACTIONS |
        private readonly WorldAction _drinkAction = new WorldAction
        {
            Id = "drink",
            Duration = 2
        };
        private readonly WorldAction _sleepAction = new WorldAction
        {
            Id = "sleep",
            Duration = 8
        };
        #endregion

        #region | FLUENTS |
        private Fluent _drunkFluent = new Fluent()
        {
            Name = "drunk"
        };
        private Fluent _hangoverFluent = new Fluent()
        {
            Name = "hangover"
        };

        List<Fluent> _listFluent = new List<Fluent>();
        #endregion

        #region | INITIALIZE |

        #endregion
        public Example1()
        {
            _listFluent = new List<Fluent>();
            _listFluent.Add(_drunkFluent);
            _listFluent.Add(_hangoverFluent);
        }

        [SetUp]
        public void SetUp()
        {
            _reasoning = new Reasoning();
            foreach(Fluent item in _listFluent)
            {
                _reasoning.AddWorldDescriptionRecord(new InitialRecord(item.ToString()));
            }
            _reasoning.AddWorldDescriptionRecord(new ActionCausesIfRecord(_drinkAction, _drunkFluent.Name.ToString(), String.Empty));
            _reasoning.AddWorldDescriptionRecord(new ActionCausesIfRecord(_sleepAction, "!" + _drunkFluent.Name.ToString(), String.Empty));
            _reasoning.AddWorldDescriptionRecord(new ActionCausesIfRecord(_sleepAction, _hangoverFluent.Name.ToString(), _drunkFluent.Name.ToString()));

            _scenarioDescription1 = new ScenarioDescription("ScenarioDescription1");

            _scenarioDescription1.addObservation(new SimpleLogicExpression("!" + _drunkFluent.Name.ToString()), 0);
            _scenarioDescription1.addObservation(new SimpleLogicExpression("!" + _hangoverFluent.Name.ToString()), 0);
            _scenarioDescription1.addObservation(new SimpleLogicExpression("!" + _drunkFluent.Name.ToString()), 10);
            _scenarioDescription1.addObservation(new SimpleLogicExpression("!" + _hangoverFluent.Name.ToString()), 10);
            _scenarioDescription1.addACS(_drinkAction, 2);
            _scenarioDescription1.addACS(_sleepAction, 2);

            _scenarioDescription2 = new ScenarioDescription("ScenarioDescription2");

            _scenarioDescription2.addObservation(new SimpleLogicExpression("!" + _drunkFluent.Name.ToString()), 0);
            _scenarioDescription2.addObservation(new SimpleLogicExpression("!" + _hangoverFluent.Name.ToString()), 0);
            _scenarioDescription2.addObservation(new SimpleLogicExpression("!" + _drunkFluent.Name.ToString()), 10);
            _scenarioDescription2.addObservation(new SimpleLogicExpression("!" + _hangoverFluent.Name.ToString()), 10);
            _scenarioDescription2.addACS(_sleepAction, 1);

        }

        #region | EXAMPLE 1 TEXT|

        //Michał jest pracujacym studentem. W srode o godzinie 8.00 powinien pojawic sie w pracy zupełnie
        //trzezwy. Mimo to we wtorek postanowił pójsc do baru. Jesli Michał sie napije, stanie sie pijany. Jesli
        //pójdzie spac przestanie byc pijany, ale stanie sie skacowany, co równiez bedzie niedopuszczalne w jego
        //pracy.

        //4.1.2 Opis akcji
        //(drink, 2) causes drunk
        //(sleep, 8) causes !drunk
        //(sleep, 8) causes hangover if drunk

        //4.1.3 Scenariusze
        //Sc =(OBS;ACS)
        //OBS = f(!drunk; 0); (!hangover; 0); (!drunk; 10); (!hangover; 10)g
        //ACS = f((drink; 2); 0); ((sleep; 8); 2)g
        //Sc2 =(OBS2; ACS2)
        //OBS2 = f(!drunk; 0); (!hangover; 0); (!drunk; 10); (!hangover; 10)g
        //ACS2 = f((sleep; 8); 1)g

        //4.1.4 Kwerendy
        //1. ever executable Sc
        //2. ever executable Sc2

        #endregion

        #region | TESTS |
        [Test]
        public void First_Query_ever_executable_SC1_Test()
        {
            //Arrange
            QueryResult expectedResult = QueryResult.False;
            //Act
            QueryResult result = _reasoning.ExecuteQuery(new ExecutableScenarioQuery(QuestionType.Ever,_scenarioDescription1),_scenarioDescription1);
            //Assert
            Assert.AreEqual(expectedResult, result);
        }
      
        [Test]
        public void Second_Query_ever_executable_SC2_Test()
        {
            //Arrange
            QueryResult expectedResult = QueryResult.True;
            //Act
            QueryResult result = _reasoning.ExecuteQuery(new ExecutableScenarioQuery(QuestionType.Ever,_scenarioDescription2),_scenarioDescription2);
            //Assert
            Assert.AreEqual(expectedResult, result);
        }
        #endregion
    }
}
