namespace KnowledgeRepresentationReasoning.Test
{
    using NUnit.Framework;

    using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

    [TestFixture]
    public class ReasoningTests : TestBase
    {
        private Reasoning _reasoning;

        [SetUp]
        public void SetUp()
        {
            _reasoning = new Reasoning();
        }

        [Test]
        public void NotImplementedTest()
        {
            Assert.IsTrue(true);
        }
    }
}