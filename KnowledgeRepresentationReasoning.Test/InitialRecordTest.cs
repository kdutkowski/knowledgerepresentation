namespace KnowledgeRepresentationReasoning.Test
{
    using KnowledgeRepresentationReasoning.World.Records;

    using NUnit.Framework;

    [TestFixture]
    public class InitialRecordTest
    {
        [Test]
        public void GetFluentNamesTest()
        {
            InitialRecord record = new InitialRecord("a1 || (a2 && a1)");
            var result = record.GetFluentNames();
            Assert.AreEqual(2, result.Length);
        }
    }
}
