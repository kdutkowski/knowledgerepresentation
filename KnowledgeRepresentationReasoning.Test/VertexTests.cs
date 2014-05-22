namespace KnowledgeRepresentationReasoning.Test
{
    using KnowledgeRepresentationReasoning.Logic;
    using KnowledgeRepresentationReasoning.World;

    using NUnit.Framework;

    [TestFixture]
    public class VertexTests
    {
        private Vertex _vertex;

        [SetUp]
        public void SetUp()
        {
            _vertex = new Vertex();
        }

        [Test]
        public void GetNextActionTimeWithNoActions()
        {
            int result = _vertex.GetNextActionTime();

            Assert.AreEqual(-1, result);
        }

        [Test]
        public void GetNextActionTimeWithOneAction()
        {
            const int Time = 1;
            var worldAction = new WorldAction();
            worldAction.StartAt = Time;
            _vertex.NextActions.Add(worldAction);
            
            int result = _vertex.GetNextActionTime();

            Assert.AreEqual(Time, result);
        }

        [Test]
        public void GetNextActionTimeWithManyActions()
        {
            const int Time = 1;
            for (int i = Time; i < 5; ++i)
            {
                var worldAction = new WorldAction();
                worldAction.StartAt = i;
                _vertex.NextActions.Add(worldAction);
            }
            int result = _vertex.GetNextActionTime();

            Assert.AreEqual(Time, result);
        }

        [Test]
        public void ValidateActionsNoActualActionOneNextActionTrue()
        {
            _vertex.ActualWorldAction = null;
            _vertex.NextActions.Add(new WorldAction("a", 0, 1));

            bool result = _vertex.ValidateActions();

            Assert.IsTrue(result);
        }

        [Test]
        public void ValidateActionsNoActualActionOneNextActionFalse()
        {
            _vertex.ActualWorldAction = null;
            _vertex.NextActions.Add(new WorldAction("a", 0, 1));
            _vertex.NextActions[0].Duration = null;

            bool result = _vertex.ValidateActions();

            Assert.IsFalse(result);
        }

        [Test]
        public void ValidateActionsNoActualActionManyNextActionsTrue()
        {
            _vertex.ActualWorldAction = null;
            _vertex.NextActions.Add(new WorldAction("a", 0, 1));
            _vertex.NextActions.Add(new WorldAction("a", 1, 1));
            _vertex.NextActions.Add(new WorldAction("a", 4, 1));

            bool result = _vertex.ValidateActions();

            Assert.IsTrue(result);
        }

        [Test]
        public void ValidateActionsNoActualActionManyNextActionsFalse()
        {
            _vertex.ActualWorldAction = null;
            _vertex.NextActions.Add(new WorldAction("a", 0, 1));
            _vertex.NextActions.Add(new WorldAction("a", 1, 4));
            _vertex.NextActions.Add(new WorldAction("a", 4, 1));

            bool result = _vertex.ValidateActions();

            Assert.IsFalse(result);
        }

        [Test]
        public void ValidateActionsWithActualActionOneNextActionTrue()
        {
            _vertex.ActualWorldAction = new WorldAction("a", 0, 1);
            _vertex.NextActions.Add(new WorldAction("a", 1, 1));

            bool result = _vertex.ValidateActions();

            Assert.IsTrue(result);
        }

        [Test]
        public void ValidateActionsWithActualActionOneNextActionFalse()
        {
            _vertex.ActualWorldAction = new WorldAction("a", 0, 1);
            _vertex.NextActions.Add(new WorldAction("a", 0, 1));
            _vertex.NextActions[0].Duration = null;

            bool result = _vertex.ValidateActions();

            Assert.IsFalse(result);
        }

        [Test]
        public void ValidateActionsWithActualActionManyNextActionsTrue()
        {
            _vertex.ActualWorldAction = new WorldAction("a", 0, 1);
            _vertex.NextActions.Add(new WorldAction("a", 5, 1));
            _vertex.NextActions.Add(new WorldAction("a", 1, 1));
            _vertex.NextActions.Add(new WorldAction("a", 4, 1));

            bool result = _vertex.ValidateActions();

            Assert.IsTrue(result);
        }

        [Test]
        public void ValidateActionsWithActualActionManyNextActionsFalse()
        {
            _vertex.ActualWorldAction = new WorldAction("a", 0, 1);
            _vertex.NextActions.Add(new WorldAction("a", 0, 1));
            _vertex.NextActions.Add(new WorldAction("a", 1, 4));
            _vertex.NextActions.Add(new WorldAction("a", 4, 1));

            bool result = _vertex.ValidateActions();

            Assert.IsFalse(result);
        }

        // TODO: Czemu to jest zakomentowane?
        [Test]
        public void CreateChildsBasedOnImplicationsNoImplication()
        {
            //_vertex.CreateChildsBasedOnImplications(new System.Collections.Generic.List<Implication>(), 
        }
    }
}
