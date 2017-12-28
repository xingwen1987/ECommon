using System;
using AspectCore.Injector;
using ECommon.AspectCore;
using ECommon.Components;

namespace ECommon.Configurations
{
    /// <summary>ENode configuration class Autofac extensions.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>Use Autofac as the object container.
        /// </summary>
        /// <returns></returns>
        public static Configuration UseAspectCore(this Configuration configuration)
        {
            return UseAspectCore(configuration, new ServiceContainer());
        }
        /// <summary>Use Autofac as the object container.
        /// </summary>
        /// <returns></returns>
        public static Configuration UseAspectCore(this Configuration configuration,IServiceContainer serviceContainer)
        {
            ObjectContainer.SetContainer(new AspectCoreObjectContainer(serviceContainer));
            return configuration;
        }
    }
}