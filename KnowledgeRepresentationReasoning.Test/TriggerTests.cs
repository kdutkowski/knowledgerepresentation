using KnowledgeRepresentationReasoning.World;
using KnowledgeRepresentationReasoning.World.Records;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assert = NUnit.Framework.Assert;

namespace KnowledgeRepresentationReasoning.Test
{
    class TriggerTests:TestBase
    {
        private ExpressionTriggersActionRecord _expressionTriggersActionRecord;
        private WorldAction _worldAction;
        private string _ifExpression;
        private string _idWorldAction;
        private int _startTime;
        private int _durationTime;
        private State _state;


        [SetUp]
        public void SetUp()
        {
            _idWorldAction = "idWorldAction";
            _startTime = 3;
            _durationTime = 5;

            _state = new State
            {
                Fluents = new List<Fluent>{   
                    new Fluent { Name = "a", Value = true },
                    new Fluent { Name = "b", Value = true },
                    new Fluent { Name = "c", Value = true },
                    new Fluent { Name = "d", Value = true }
                }
            };
            _worldAction = new WorldAction(_idWorldAction, _startTime, _durationTime);

        }

        [Test]
        public void IsFulfilledTrueTest()
        {
            _ifExpression = "a && b && c && d";
            _expressionTriggersActionRecord = new ExpressionTriggersActionRecord(_worldAction, _ifExpression);
            bool result = _expressionTriggersActionRecord.IsFulfilled(_state);
            Assert.IsTrue(result);
        }

        [Test]
        public void IsFulfilledFalseTest()
        {
            _ifExpression = "a && b && c && !d";
            _expressionTriggersActionRecord = new ExpressionTriggersActionRecord(_worldAction, _ifExpression);
            bool result = _expressionTriggersActionRecord.IsFulfilled(_state);
            Assert.IsFalse(result);
        }

        [Test]
        [NUnit.Framework.ExpectedException]
        public void IsFulfilledWrongExpressionTest()
        {
            _ifExpression = "a && b && c && e";
            _expressionTriggersActionRecord = new ExpressionTriggersActionRecord(_worldAction, _ifExpression);
            bool result = _expressionTriggersActionRecord.IsFulfilled(_state);
            Assert.IsFalse(result);
        }

        [Test, Sequential]
        public void GetResultTest(
            [Values(0, 1)]int time,
            [Values(0, 1)]int expected)
        {
            _ifExpression = "a && b && c && d";
            _expressionTriggersActionRecord = new ExpressionTriggersActionRecord(_worldAction, _ifExpression);
            WorldAction worldAction = _expressionTriggersActionRecord.GetResult(time);
            Assert.AreEqual( expected,_worldAction.StartAt);
        }
        
    }
}
