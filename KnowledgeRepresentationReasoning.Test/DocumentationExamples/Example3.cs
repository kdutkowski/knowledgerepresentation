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
    public class Example3 : TestBase
    {
        private Reasoning _reasoning;
        private ScenarioDescription _scenarioDescription;

        #region | ACTIONS |
        private readonly WorldAction _goesBillAction = new WorldAction
        {
            Id = "goesBill",
            Duration = 2
        };
        private readonly WorldAction _runsMaxAction = new WorldAction
        {
            Id = "runsMax",
            Duration = 2
        };
        private readonly WorldAction _whistlesBillAction = new WorldAction
        {
            Id = "whistlesBill",
            Duration = 1
        };
        private readonly WorldAction _barksMaxAction = new WorldAction
        {
            Id = "barksMax",
            Duration = 1
        };
        #endregion

        #region | FLUENTS |
        private Fluent _runMaxFluent = new Fluent()
        {
            Name = "runMax"
        };
        private Fluent _barkMaxrFluent = new Fluent()
        {
            Name = "barkMax"
        };
        List<Fluent> _listFluent = new List<Fluent>();
        #endregion

        #region | INITIALIZE |

        public Example3()
        {
            _listFluent = new List<Fluent>();
            _listFluent.Add(_runMaxFluent);
            _listFluent.Add(_barkMaxrFluent);
        }

        [SetUp]
        public void SetUp()
        {
            _reasoning = new Reasoning();
            foreach(Fluent item in _listFluent)
            {
                _reasoning.AddWorldDescriptionRecord(new InitialRecord(item.Name.ToString()));
            }
            _reasoning.AddWorldDescriptionRecord(new ActionCausesIfRecord(_goesBillAction, _runMaxFluent.Name.ToString(), String.Empty));
            _reasoning.AddWorldDescriptionRecord(new ActionInvokesAfterIfRecord(_goesBillAction, _runsMaxAction, 0, String.Empty));
            _reasoning.AddWorldDescriptionRecord(new ActionCausesIfRecord(_runsMaxAction, "!" + _runMaxFluent.Name.ToString(), String.Empty));
            _reasoning.AddWorldDescriptionRecord(new ActionCausesIfRecord(_whistlesBillAction, _barkMaxrFluent.Name.ToString(), String.Empty));
            _reasoning.AddWorldDescriptionRecord(new ActionInvokesAfterIfRecord(_whistlesBillAction, _barksMaxAction, 0, String.Empty));
            _reasoning.AddWorldDescriptionRecord(new ActionCausesIfRecord(_barksMaxAction, "!" + _barkMaxrFluent.Name.ToString(), String.Empty));

            _scenarioDescription = new ScenarioDescription("ScenarioDescription");
            _scenarioDescription.addObservation(new SimpleLogicExpression("!" + _runMaxFluent.Name.ToString()), 0);
            _scenarioDescription.addObservation(new SimpleLogicExpression("!" + _barkMaxrFluent.Name.ToString()), 0);
            _scenarioDescription.addACS(_goesBillAction, 1);
            _scenarioDescription.addACS(_whistlesBillAction, 5);
            _scenarioDescription.addACS(_goesBillAction, 7);


        }
        #endregion

        #region | EXAMPLE 3 TEXT|
        //Mamy Billa i psa Maxa. Jesli Bill idzie, to Max biegnie przez jakis czas. Jesli Bill gwizdze, Max szczeka
        //przez jakis czas. Jesli Bill zatrzymuje sie, Max równiez. Jesli Bill przestaje gwizdac, to Max przestaje
        //szczekac.

        //Opis akcji
        //(goes Bill; 2) causes run Max
        //(goes Bill; 2) invokes (runs Max; 2) after 0
        //(runs Max; 2) causes !run Max
        //(whistles Bill; 1) causes bark Max
        //(whistles Bill; 1) invokes (barks Max; 1) after 0
        //(barks Max; 1) causes !bark Max

        //Scenariusz
        //Sc =(OBS; ACS)
        //OBS = (!run Max; 0); (!bark Max; 0)
        //ACS = ((goes Bill; 2); 1); ((whistles Bill; 1); 5); ((goes Bill; 2); 7)

        //Kwerendy
        //1. performing runs Max at 8 when Sc
        //2. performing runs Max when Sc
        //3. performing at 8 when Sc

        #endregion

        #region | TESTS |
        [Test]
        public void First_Query_performing_runs_at_8_when_Ever_Test()
        {
            //Arrange
            QueryResult expectedResult = QueryResult.False;
            //Act
            QueryResult result = _reasoning.ExecuteQuery(new PerformingActionAtTimeQuery(QuestionType.Ever, _runsMaxAction, 8), _scenarioDescription);
            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Second_Query_performing_runs_when_Ever_Test()
        {
            //Arrange
            QueryResult expectedResult = QueryResult.True;
            //Act
            QueryResult result = _reasoning.ExecuteQuery(new PerformingActionAtTimeQuery(QuestionType.Ever, _runsMaxAction), _scenarioDescription);
            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Third_Query_performing_at_8_when_Ever_Test()
        {
            //Arrange
            QueryResult expectedResult = QueryResult.True;
            //Act
            QueryResult result = _reasoning.ExecuteQuery(new PerformingActionAtTimeQuery(QuestionType.Ever, null, 8), _scenarioDescription);
            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void First_Query_performing_runs_at_8_when_Always_Test()
        {
            //Arrange
            QueryResult expectedResult = QueryResult.False;
            //Act
            QueryResult result = _reasoning.ExecuteQuery(new PerformingActionAtTimeQuery(QuestionType.Always, _runsMaxAction, 8), _scenarioDescription);
            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Second_Query_performing_runs_when_Always_Test()
        {
            //Arrange
            QueryResult expectedResult = QueryResult.True;
            //Act
            QueryResult result = _reasoning.ExecuteQuery(new PerformingActionAtTimeQuery(QuestionType.Always, _runsMaxAction), _scenarioDescription);
            //Assert
            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public void Third_Query_performing_at_8_when_Always_Test()
        {
            //Arrange
            QueryResult expectedResult = QueryResult.True;
            //Act
            QueryResult result = _reasoning.ExecuteQuery(new PerformingActionAtTimeQuery(QuestionType.Always, null, 8), _scenarioDescription);
            //Assert
            Assert.AreEqual(expectedResult, result);
        }
        #endregion
    }
}
