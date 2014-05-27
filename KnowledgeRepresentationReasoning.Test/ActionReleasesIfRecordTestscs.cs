using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KnowledgeRepresentationReasoning.World;
using KnowledgeRepresentationReasoning.World.Records;
using NUnit.Framework;

namespace KnowledgeRepresentationReasoning.Test
{
    class ActionReleasesIfRecordTestscs : TestBase
    {
        private ActionReleasesIfRecord _actionReleasesIfRecord;
        private WorldAction _worldAction;
        private string _ifExpression;
        private Fluent _fluent;
        private string _idWorldAction;
        private int _startTime;
        private int _durationTime;
        private State _state;
        WorldAction _endedWorldAction;


        [SetUp]
        public void SetUp()
        {
            _idWorldAction = "idWorldAction";
            _startTime = 8;
            _durationTime = 5;

            _worldAction = new WorldAction(_idWorldAction, _startTime, _durationTime);

            _fluent = new Fluent()
            {
                Name = "a",
                Value = true
            };

            _state = new State
            {
                Fluents = new List<Fluent>{   
                    new Fluent { Name = "a", Value = true },
                    new Fluent { Name = "b", Value = true },
                    new Fluent { Name = "c", Value = true },
                    new Fluent { Name = "d", Value = true }
                }
            };
        }

        [Test]
        public void IsFulfilledWrongActionNameTest()
        {
            _ifExpression = "a && b && c && d";
            _actionReleasesIfRecord = new ActionReleasesIfRecord(_worldAction, _fluent, _ifExpression);
            _endedWorldAction = new WorldAction("_endedWorldAction", _startTime, _durationTime);

            bool result = _actionReleasesIfRecord.IsFulfilled(_state, _endedWorldAction);
            Assert.IsFalse(result);
        }
        [Test]
        public void IsFulfilledWrongActionDurationTest()
        {
            _ifExpression = "a && b && c && d";
            _actionReleasesIfRecord = new ActionReleasesIfRecord(_worldAction, _fluent, _ifExpression);
            _endedWorldAction = new WorldAction(_idWorldAction, _startTime, 7);

            bool result = _actionReleasesIfRecord.IsFulfilled(_state, _endedWorldAction);
            Assert.IsFalse(result);
        }
        [Test]
        public void IsFulfilledCorrectTest()
        {
            _ifExpression = "a && b && c && d";
            _actionReleasesIfRecord = new ActionReleasesIfRecord(_worldAction, _fluent, _ifExpression);

            bool result = _actionReleasesIfRecord.IsFulfilled(_state, _worldAction);
            Assert.IsTrue(result);
        }
        [Test]
        public void IsFulfilledFalseExpressionTest()
        {
            _ifExpression = "a && b && c && !d";
            _actionReleasesIfRecord = new ActionReleasesIfRecord(_worldAction, _fluent, _ifExpression);

            bool result = _actionReleasesIfRecord.IsFulfilled(_state, _worldAction);
            Assert.IsFalse(result);
        }


        [Test]
        public void GetResultCorrectTest()
        {
            _actionReleasesIfRecord = new ActionReleasesIfRecord(_worldAction, _fluent, _ifExpression);

            Fluent result = _actionReleasesIfRecord.GetResult(_startTime);
            Assert.IsTrue(result.Value);
        }

        [Test]
        public void GetResultCorrectFalseFluentTest()
        {
            _fluent.Value = false;
            _actionReleasesIfRecord = new ActionReleasesIfRecord(_worldAction, _fluent, _ifExpression);

            Fluent result = _actionReleasesIfRecord.GetResult(_startTime);
            Assert.IsFalse(result.Value);
        }
    }
}
