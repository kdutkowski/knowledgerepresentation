using KnowledgeRepresentationReasoning.World;
using KnowledgeRepresentationReasoning.World.Records;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeRepresentationReasoning.Test
{
    class TriggerTests:TestBase
    {
        private ExpressionTriggersActionRecord expressionTriggersActionRecord;
        private WorldAction worldAction;
        private string ifExpression;
        private string idWorldAction;
        private int startTime;
        private int durationTime;
        private State state;


        [SetUp]
        public void SetUp()
        {
            idWorldAction="idWorldAction";
            startTime = 3;
            durationTime = 5;

            state = new State
            {
                Fluents = new List<Fluent>{   
                    new Fluent { Name = "a", Value = true },
                    new Fluent { Name = "b", Value = true },
                    new Fluent { Name = "c", Value = true },
                    new Fluent { Name = "d", Value = true }
                }
            };
            worldAction = new WorldAction(idWorldAction,startTime,durationTime);
          
        }

        [Test]
        public void IsFulfilledTrueTest()
        {
            ifExpression = "a && b && c && d";
            expressionTriggersActionRecord = new ExpressionTriggersActionRecord(worldAction, ifExpression);
            bool result = expressionTriggersActionRecord.IsFulfilled(state);
            Assert.IsTrue(result);
        }

        [Test]
        public void IsFulfilledFalseTest()
        {
            ifExpression = "a && b && !c && d";
            expressionTriggersActionRecord = new ExpressionTriggersActionRecord(worldAction, ifExpression);
            bool result = expressionTriggersActionRecord.IsFulfilled(state);
            Assert.IsFalse(result);
        }
        
    }
}
