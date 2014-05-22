namespace KnowledgeRepresentationReasoning.Test
{
    using KnowledgeRepresentationReasoning.World.Records;

    using NUnit.Framework;

    [TestFixture]
    public class InitialRecordTest : TestBase
    {
        [Test]
        public void GetFluentNamesTest()
        {
            var record = new InitialRecord("a1 || (a2 && a1)");
            var result = record.GetResult();
            Assert.AreEqual(2, result.Length);
        }
    }
}
