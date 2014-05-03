namespace KnowledgeRepresentationReasoning.Helpers
{
    using System;
    using System.Linq;

    using Autofac.Core;

    using log4net;

    public class LoggingModule : ComponentInjectionModule
    {
        protected override void OnComponentPreparing(object sender, PreparingEventArgs e)
        {
            Type t = e.Component.Activator.LimitType;
            e.Parameters = e.Parameters.Union(
                new[]
                {
                    new ResolvedParameter(
                        (p, i) => p.ParameterType == typeof(ILog), (p, i) => LogManager.GetLogger(t))
                });
        }
    }
}
