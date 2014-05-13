using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace KnowledgeRepresentationReasoning.Test
{
    [TestClass]
    public class ReasoningTests: TestBase
    {
        private Reasoning _reasoning;


        public ReasoningTests() : base() { }

        [TestInitialize]
        public void SetUp()
        {
            _reasoning = new Reasoning();
        }

        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(true);
        }
    }
}
