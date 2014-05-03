namespace KnowledgeRepresentationReasoning.Test
{
    using Autofac;
    using Autofac.Extras.CommonServiceLocator;

    using KnowledgeRepresentationReasoning.Expressions;
    using KnowledgeRepresentationReasoning.Helpers;
    using KnowledgeRepresentationReasoning.Logic;
    using KnowledgeRepresentationReasoning.World.Records;

    using log4net;
    using log4net.Config;

    using Microsoft.Practices.ServiceLocation;

    using NUnit.Framework;

    [TestFixture]
    public class InitialRecordTest
    {
        private static IContainer Container { get; set; }

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            XmlConfigurator.Configure();
            this.Initialize();
        }

        [Test]
        public void GetFluentNamesTest()
        {
            var record = new InitialRecord("a1 || (a2 && a1)");
            var result = record.GetResult();
            Assert.AreEqual(2, result.Length);
        }

        public void Initialize()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new LoggingModule());
            builder.RegisterInstance(LogManager.GetLogger(typeof(InitialRecordTest))).As<ILog>();
            builder.RegisterType<Tree>().As<ITree>();
            builder.RegisterType<SimpleLogicExpression>().As<ILogicExpression>();
            Container = builder.Build();
            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(Container));
        }
    }
}
