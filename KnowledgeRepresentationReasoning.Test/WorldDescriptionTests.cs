namespace KnowledgeRepresentationReasoning.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Autofac;
    using Autofac.Extras.CommonServiceLocator;

    using KnowledgeRepresentationReasoning.Expressions;
    using KnowledgeRepresentationReasoning.Helpers;
    using KnowledgeRepresentationReasoning.Logic;
    using KnowledgeRepresentationReasoning.World;
    using KnowledgeRepresentationReasoning.World.Records;

    using log4net;
    using log4net.Config;

    using Microsoft.Practices.ServiceLocation;

    using NUnit.Framework;

    [TestFixture]
    public class WorldDescriptionTests
    {
        private static IContainer Container { get; set; }

        private WorldDescription _worldDescription;
        private WorldAction action_A_2 = new WorldAction { Duration = 2, Id = "A" };
        private WorldAction action_B_3 = new WorldAction { Duration = 3, Id = "B" };
        private WorldAction action_C_5 = new WorldAction { Duration = 5, Id = "C" };

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            XmlConfigurator.Configure();
            this.Initialize();
        }

        [SetUp]
        public void SetUp()
        {
            var fluent_a = new Fluent { Id = "a", Name = "a", Value = true };

            _worldDescription = new WorldDescription();

            // INITIALLY
            _worldDescription.Descriptions.Add(new Tuple<WorldDescriptionRecordType, WorldDescriptionRecord>(WorldDescriptionRecordType.Initially, new InitialRecord("a || b || c || e")));

            // ACTION CAUSES IF
            _worldDescription.Descriptions.Add(new Tuple<WorldDescriptionRecordType, WorldDescriptionRecord>(WorldDescriptionRecordType.ActionCausesIf, new ActionCausesIfRecord(action_A_2, "a && !b", "c")));
            _worldDescription.Descriptions.Add(new Tuple<WorldDescriptionRecordType, WorldDescriptionRecord>(WorldDescriptionRecordType.ActionCausesIf, new ActionCausesIfRecord(action_B_3, "a || b", "e")));
            _worldDescription.Descriptions.Add(new Tuple<WorldDescriptionRecordType, WorldDescriptionRecord>(WorldDescriptionRecordType.ActionCausesIf, new ActionCausesIfRecord(action_B_3, "!b && c", "e")));

            // RELEASE FLUENT
            _worldDescription.Descriptions.Add(new Tuple<WorldDescriptionRecordType, WorldDescriptionRecord>(WorldDescriptionRecordType.ActionReleasesIf, new ActionReleasesIfRecord(action_B_3, fluent_a, "e || c")));

        }

        [Test]
        public void GetImplications_GetPossibleFutureStates_Simple_Test()
        {
            var state = new State
                        {
                            Fluents = new List<Fluent>
                                      {
                                          new Fluent { Id = "a", Name = "a", Value = true },
                                          new Fluent { Id = "b", Name = "b", Value = true },
                                          new Fluent { Id = "c", Name = "c", Value = true },
                                          new Fluent { Id = "e", Name = "e", Value = true },
                                      }
                        };
            var leaf = new Vertex(state, action_A_2, 10, null);

            var implication = _worldDescription.GetImplications(leaf, 0);
            Assert.AreEqual(1, implication.Count);
            Assert.NotNull(implication[0].FutureState);
            var futureState = implication[0].FutureState;
            Assert.IsTrue(futureState.Fluents.First(t => t.Name == "a").Value);
            Assert.IsFalse(futureState.Fluents.First(t => t.Name == "b").Value);
            Assert.IsTrue(futureState.Fluents.First(t => t.Name == "c").Value);
            Assert.IsTrue(futureState.Fluents.First(t => t.Name == "e").Value);
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
