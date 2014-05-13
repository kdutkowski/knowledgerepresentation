using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KnowledgeRepresentationReasoning;
using Microsoft.Practices.ServiceLocation;
using log4net;
using log4net.Config;
using Autofac;
using Autofac.Extras.CommonServiceLocator;
using KnowledgeRepresentationReasoning.Logic;
using KnowledgeRepresentationReasoning.Expressions;
using KnowledgeRepresentationReasoning.Helpers;

namespace KnowledgeRepresentationReasoning.Test
{
    public abstract class TestBase
    {
        protected static IContainer Container { get; set; }

        public TestBase()
        {
            Initialize();
        }

        private void Initialize()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new LoggingModule());
            builder.RegisterInstance(LogManager.GetLogger(typeof(Reasoning))).As<ILog>();
            builder.RegisterType<Tree>().As<ITree>();
            builder.RegisterType<SimpleLogicExpression>().As<ILogicExpression>();
            Container = builder.Build();
            ServiceLocator.SetLocatorProvider(() => new AutofacServiceLocator(Container));
        }
    }
}
