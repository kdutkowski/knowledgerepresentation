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

        [TestMethod]
        public void ValidateActionsNoActualActionOneNextActionTrue()
        {
            _vertex.ActualWorldAction = null;
            _vertex.NextActions.Add(new WorldAction("a", 0, 1));

            bool result = _vertex.ValidateActions();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateActionsNoActualActionOneNextActionFalse()
        {
            _vertex.ActualWorldAction = null;
            _vertex.NextActions.Add(new WorldAction("a", 0, 1));
            _vertex.NextActions[0].Duration = null;

            bool result = _vertex.ValidateActions();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateActionsNoActualActionManyNextActionsTrue()
        {
            _vertex.ActualWorldAction = null;
            _vertex.NextActions.Add(new WorldAction("a", 0, 1));
            _vertex.NextActions.Add(new WorldAction("a", 1, 1));
            _vertex.NextActions.Add(new WorldAction("a", 4, 1));

            bool result = _vertex.ValidateActions();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateActionsNoActualActionManyNextActionsFalse()
        {
            _vertex.ActualWorldAction = null;
            _vertex.NextActions.Add(new WorldAction("a", 0, 1));
            _vertex.NextActions.Add(new WorldAction("a", 1, 4));
            _vertex.NextActions.Add(new WorldAction("a", 4, 1));

            bool result = _vertex.ValidateActions();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateActionsWithActualActionOneNextActionTrue()
        {
            _vertex.ActualWorldAction = new WorldAction("a", 0, 1);
            _vertex.NextActions.Add(new WorldAction("a", 1, 1));

            bool result = _vertex.ValidateActions();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateActionsWithActualActionOneNextActionFalse()
        {
            _vertex.ActualWorldAction = new WorldAction("a", 0, 1);
            _vertex.NextActions.Add(new WorldAction("a", 0, 1));
            _vertex.NextActions[0].Duration = null;

            bool result = _vertex.ValidateActions();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateActionsWithActualActionManyNextActionsTrue()
        {
            _vertex.ActualWorldAction = new WorldAction("a", 0, 1);
            _vertex.NextActions.Add(new WorldAction("a", 5, 1));
            _vertex.NextActions.Add(new WorldAction("a", 1, 1));
            _vertex.NextActions.Add(new WorldAction("a", 4, 1));

            bool result = _vertex.ValidateActions();

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateActionsWithActualActionManyNextActionsFalse()
        {
            _vertex.ActualWorldAction = new WorldAction("a", 0, 1);
            _vertex.NextActions.Add(new WorldAction("a", 0, 1));
            _vertex.NextActions.Add(new WorldAction("a", 1, 4));
            _vertex.NextActions.Add(new WorldAction("a", 4, 1));

            bool result = _vertex.ValidateActions();

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CreateChildsBasedOnImplicationsNoImplication()
        {
            //_vertex.CreateChildsBasedOnImplications(new System.Collections.Generic.List<Implication>(), 
        }
    }
}
