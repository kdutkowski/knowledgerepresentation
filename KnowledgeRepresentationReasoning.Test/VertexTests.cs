using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KnowledgeRepresentationReasoning.Logic;
using KnowledgeRepresentationReasoning.World;

namespace KnowledgeRepresentationReasoning.Test
{
    [TestClass]
    public class VertexTests
    {
        private Vertex _vertex;

        [TestInitialize]
        public void SetUp()
        {
            _vertex = new Vertex();
        }

        [TestMethod]
        public void GetNextActionTimeWithNoActions()
        {
            int result = _vertex.GetNextActionTime();

            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void GetNextActionTimeWithOneAction()
        {
            int time = 1;
            WorldAction worldAction = new WorldAction();
            worldAction.StartAt = time;
            _vertex.NextActions.Add(worldAction);
            
            int result = _vertex.GetNextActionTime();

            Assert.AreEqual(time, result);
        }

        [TestMethod]
        public void GetNextActionTimeWithManyActions()
        {
            int time = 1;
            for (int i = time; i < 5; ++i)
            {
                WorldAction worldAction = new WorldAction();
                worldAction.StartAt = i;
                _vertex.NextActions.Add(worldAction);
            }
            int result = _vertex.GetNextActionTime();

            Assert.AreEqual(time, result);
        }
    }
}
