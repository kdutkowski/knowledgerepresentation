namespace KnowledgeRepresentationReasoning.Test
{
    using KnowledgeRepresentationReasoning.Logic;
    using KnowledgeRepresentationReasoning.World;

    using NUnit.Framework;

    [TestFixture]
    public class VertexTests : TestBase
    {
        private Vertex _vertex;

        [SetUp]
        public void SetUp()
        {
            _vertex = new Vertex();
        }
    }
}
