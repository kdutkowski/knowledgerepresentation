namespace KnowledgeRepresentationReasoning.Helpers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;

    using Autofac.Core;

    using log4net;

    /// <summary>
    ///     Base module for injecting into registrations when they are prepared/activated.
    /// </summary>
    public class ComponentInjectionModule : IModule
    {
        private static void InjectLoggerProperties(object instance)
        {
            var instanceType = instance.GetType();

            // Get all the injectable properties to set.
            // If you wanted to ensure the properties were only UNSET properties,
            // here's where you'd do it.
            var properties = instanceType
              .GetProperties(BindingFlags.Public | BindingFlags.Instance)
              .Where(p => p.PropertyType == typeof(ILog) && p.CanWrite && p.GetIndexParameters().Length == 0);

            // Set the properties located.
            foreach (var propToSet in properties)
            {
                propToSet.SetValue(instance, LogManager.GetLogger(instanceType), null);
            }
        }

        /// <summary>
        ///     Apply the module to the component registry.
        /// </summary>
        /// <param name = "componentRegistry">
        ///     Component registry to apply configuration to.
        /// </param>
        public void Configure(IComponentRegistry componentRegistry)
        {
            foreach (var registration in componentRegistry.Registrations)
            {
                this.AttachToComponentRegistration(registration);
            }

            componentRegistry.Registered +=
                (sender, e) => this.AttachToComponentRegistration(e.ComponentRegistration);
        }

        /// <summary>
        ///     Attaches to the <see cref = "IComponentRegistration.Preparing" /> event of a component registration.
        /// </summary>
        /// <param name = "registration">
        ///     The registration whose Preparing event will be attached.
        /// </param>
        protected virtual void AttachToComponentRegistration(IComponentRegistration registration)
        {
            registration.Preparing += this.OnComponentPreparing;
            registration.Activating += this.OnComponentActivating;
            registration.Activated += this.OnComponentActivated;
        }

        /// <summary>
        /// Called when Autofac has activated a component.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ActivatedEventArgs{T}"/> instance containing the event data.</param>
        [SuppressMessage("Microsoft.Security", "CA2109:ReviewVisibleEventHandlers", Justification = "Not an event handler")]
        protected virtual void OnComponentActivated(object sender, ActivatedEventArgs<object> e)
        {
            InjectLoggerProperties(e.Instance);
        }

        /// <summary>
        /// Called when Autofac is activating a component.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ActivatingEventArgs{T}"/> instance containing the event data.</param>
        [SuppressMessage("Microsoft.Security", "CA2109:ReviewVisibleEventHandlers", Justification = "Not an event handler")]
        protected virtual void OnComponentActivating(object sender, ActivatingEventArgs<object> e)
        {
        }

        /// <summary>
        /// Called when Autofac is preparing a component for activation.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="ActivatingEventArgs{T}"/> instance containing the event data.</param>
        [SuppressMessage("Microsoft.Security", "CA2109:ReviewVisibleEventHandlers", Justification = "Not an event handler")]
        protected virtual void OnComponentPreparing(object sender, PreparingEventArgs e)
        {
        }
    }
}
