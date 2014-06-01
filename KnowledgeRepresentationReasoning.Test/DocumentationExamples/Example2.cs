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
    public class Example2 : TestBase
    {
        private Reasoning _reasoning;
        private ScenarioDescription _scenarioDescription;

        #region | ACTIONS |
        private readonly WorldAction _makingPancAction = new WorldAction
        {
            Id = "makingPanc",
            Duration = 1
        };
        private readonly WorldAction _makingCakeAction = new WorldAction
        {
            Id = "makingCake",
            Duration = 1
        };
        private readonly WorldAction _buyEggsAction = new WorldAction
        {
            Id = "buyEggs",
            Duration = 2
        };
        #endregion

        #region | FLUENTS |
        private Fluent _eggsFluent = new Fluent()
        {
            Name = "eggs"
        };
        List<Fluent> _listFluent = new List<Fluent>();
        #endregion

        #region | INITIALIZE |

        public Example2()
        {
            _listFluent = new List<Fluent>();
            _listFluent.Add(_eggsFluent);
        }
        [SetUp]
        public void SetUp()
        {
            _reasoning = new Reasoning();
            foreach(Fluent item in _listFluent)
            {
                _reasoning.AddWorldDescriptionRecord(new InitialRecord(item.ToString()));
            }
            _reasoning.AddWorldDescriptionRecord(new ActionCausesIfRecord(_makingPancAction, "!" + _eggsFluent.Name.ToString(), _eggsFluent.Name.ToString()));
            _reasoning.AddWorldDescriptionRecord(new ActionCausesIfRecord(_makingCakeAction, "!" + _eggsFluent.Name.ToString(), _eggsFluent.Name.ToString()));
            _reasoning.AddWorldDescriptionRecord(new ActionCausesIfRecord(_buyEggsAction, _eggsFluent.Name.ToString(), String.Empty));

            _scenarioDescription = new ScenarioDescription("ScenarioDescription");

            _scenarioDescription.addObservation(new SimpleLogicExpression(_eggsFluent.Name.ToString()), 0);
            _scenarioDescription.addACS(_makingPancAction, 0);
            _scenarioDescription.addACS(_makingCakeAction, 2);
        }
        #endregion

        #region | EXAMPLE 2 TEXT |

        //Mick i Sarah sa para, wiec maja wspólne produkty spozywcze, ale posiłki zwykle jadaja oddzielnie.
        //Pewnego dnia Sarah chce zrobic ciasto, a Mick nalesniki. Nie moga byc one robione w tym samym czasie
        //ze wzgledu koniecznosc uzycia miksera do przygotowania obu. Ponadto, zrobienie jednego lub drugiego
        //dania zuzywa cały zapas jajek dostepnych w mieszkaniu, wiec trzeba je potem dokupic.

        //4.2.2 Opis akcji
        //(making panc; 1) causes !eggs if eggs
        //(making cake; 1) causes !eggs if eggs
        //(buy eggs; 2) causes eggs

        //4.2.3 Scenariusz
        //Sc =(OBS; ACS)
        //OBS = f(eggs; 0)g
        //ACS = f((making panc; 1); 0); ((making cake; 1); 2)g

        //4.2.4 Kwerendy
        //1. eggs at 0 when Sc
        //2. eggs at 2 when Sc


        #endregion

        #region | TESTS |
        [Test]
        public void First_Query_at_0_when_Ever_TTest()
        {
            //Arrange
            QueryResult expectedResult = QueryResult.True;
            //Act
            QueryResult result = _reasoning.ExecuteQuery(new ConditionAtTimeQuery(QuestionType.Ever, _eggsFluent.Name.ToString(), 0), _scenarioDescription);
            //Assert
            Assert.AreEqual(expectedResult, result);
        }
        [Test]
        public void First_Query_at_0_when_Always_Test()
        {
            //Arrange
            QueryResult expectedResult = QueryResult.True;
            //Act
            QueryResult result = _reasoning.ExecuteQuery(new ConditionAtTimeQuery(QuestionType.Always, _eggsFluent.Name.ToString(), 0), _scenarioDescription);
            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Second_Query_at_2_when_Ever_Test()
        {
            //Arrange
            QueryResult expectedResult = QueryResult.False;
            //Act
            QueryResult result = _reasoning.ExecuteQuery(new ConditionAtTimeQuery(QuestionType.Ever, _eggsFluent.Name.ToString(), 2), _scenarioDescription);
            //Assert
            Assert.AreEqual(expectedResult, result);
        }
        [Test]
        public void Second_Query_at_2_when_Always_Test()
        {
            //Arrange
            QueryResult expectedResult = QueryResult.False;
            //Act
            QueryResult result = _reasoning.ExecuteQuery(new ConditionAtTimeQuery(QuestionType.Always, _eggsFluent.Name.ToString(), 2), _scenarioDescription);
            //Assert
            Assert.AreEqual(expectedResult, result);
        }
        #endregion
    }
}
