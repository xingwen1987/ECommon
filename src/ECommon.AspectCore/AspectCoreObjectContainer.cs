using System;
using AspectCore.Injector;
using ECommon.Components;

namespace ECommon.AspectCore
{
    /// <summary>Autofac implementation of IObjectContainer.
    /// </summary>
    public class AspectCoreObjectContainer : IObjectContainer
    {
        private readonly IServiceContainer _serviceContainer;
        private IServiceResolver _serviceResolver;
        /// <summary>Default constructor.
        /// </summary>
        public AspectCoreObjectContainer() : this(new ServiceContainer())
        {
        }
        /// <summary>Parameterized constructor.
        /// </summary>
        public AspectCoreObjectContainer(IServiceContainer serviceContainer)
        {
            _serviceContainer = serviceContainer;
        }

        /// <summary>Represents the iner autofac container builder.
        /// </summary>
        public IServiceResolver ServiceResolver
        {
            get
            {
                return _serviceResolver;
            }
        }

        /// <summary>Represents the inner autofac container.
        /// </summary>
        public IServiceContainer ServiceContainer
        {
            get
            {
                return _serviceContainer;
            }
        }



        /// <summary>Build the container.
        /// </summary>
        public void Build()
        {
            _serviceResolver = _serviceContainer.Build();
        }
        /// <summary>Register a implementation type.
        /// </summary>
        /// <param name="implementationType">The implementation type.</param>
        /// <param name="serviceName">The service name.</param>
        /// <param name="life">The life cycle of the implementer type.</param>
        public void RegisterType(Type implementationType, string serviceName = null, LifeStyle life = LifeStyle.Singleton)
        {
            var lifetime = PasreLifeStyle(life);
            var registrationBuilder = _serviceContainer.AddType(implementationType,lifetime);
            //if (implementationType.IsGenericType)
            //{
            //    var registrationBuilder = _serviceContainer.AddType(implementationType,);
            //    if (serviceName != null)
            //    {
            //        registrationBuilder.Named(serviceName, implementationType);
            //    }
            //    if (life == LifeStyle.Singleton)
            //    {
            //        registrationBuilder.SingleInstance();
            //    }
            //}
            //else
            //{
            //    var registrationBuilder = _serviceContainer.RegisterType(implementationType);
            //    if (serviceName != null)
            //    {
            //        registrationBuilder.Named(serviceName, implementationType);
            //    }
            //    if (life == LifeStyle.Singleton)
            //    {
            //        registrationBuilder.SingleInstance();
            //    }
            //}
        }
        /// <summary>Register a implementer type as a service implementation.
        /// </summary>
        /// <param name="serviceType">The service type.</param>
        /// <param name="implementationType">The implementation type.</param>
        /// <param name="serviceName">The service name.</param>
        /// <param name="life">The life cycle of the implementer type.</param>
        public void RegisterType(Type serviceType, Type implementationType, string serviceName = null, LifeStyle life = LifeStyle.Singleton)
        {
            var lifetime = PasreLifeStyle(life);
            var registrationBuilder = _serviceContainer.AddType(serviceType, implementationType, lifetime);
            //if (implementationType.IsGenericType)
            //{
               
            //    //if (serviceName != null)
            //    //{
            //    //    registrationBuilder.Named(serviceName, implementationType);
            //    //}
            //    //if (life == LifeStyle.Singleton)
            //    //{
            //    //    registrationBuilder.SingleInstance();
            //    //}
            //}
            //else
            //{
            //    var registrationBuilder = _serviceContainer.RegisterType(implementationType).As(serviceType);
            //    if (serviceName != null)
            //    {
            //        registrationBuilder.Named(serviceName, serviceType);
            //    }
            //    if (life == LifeStyle.Singleton)
            //    {
            //        registrationBuilder.SingleInstance();
            //    }
            //}
        }
        /// <summary>Register a implementer type as a service implementation.
        /// </summary>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <typeparam name="TImplementer">The implementer type.</typeparam>
        /// <param name="serviceName">The service name.</param>
        /// <param name="life">The life cycle of the implementer type.</param>
        public void Register<TService, TImplementer>(string serviceName = null, LifeStyle life = LifeStyle.Singleton)
            where TService : class
            where TImplementer : class, TService
        {
            var lifetime = PasreLifeStyle(life);
            var registrationBuilder = _serviceContainer.AddType<TService,TImplementer>(lifetime);
            //var registrationBuilder = _serviceContainer.AddType<TImplementer>().As<TService>();
            //if (serviceName != null)
            //{
            //    registrationBuilder.Named<TService>(serviceName);
            //}
            //if (life == LifeStyle.Singleton)
            //{
            //    registrationBuilder.SingleInstance();
            //}
        }
        /// <summary>Register a implementer type instance as a service implementation.
        /// </summary>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <typeparam name="TImplementer">The implementer type.</typeparam>
        /// <param name="instance">The implementer type instance.</param>
        /// <param name="serviceName">The service name.</param>
        public void RegisterInstance<TService, TImplementer>(TImplementer instance, string serviceName = null)
            where TService : class
            where TImplementer : class, TService
        {
            var registrationBuilder = _serviceContainer.AddInstance(typeof(TService), typeof(TImplementer));
            //var registrationBuilder = _serviceContainer.re(instance).As<TService>().SingleInstance();
            //if (serviceName != null)
            //{
            //    registrationBuilder.Named<TService>(serviceName);
            //}
        }

        /// <summary>Resolve a service.
        /// </summary>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <returns>The component instance that provides the service.</returns>
        public TService Resolve<TService>() where TService : class
        {
            return _serviceResolver.Resolve<TService>();
        }

        /// <summary>Resolve a service.
        /// </summary>
        /// <param name="serviceType">The service type.</param>
        /// <returns>The component instance that provides the service.</returns>
        public object Resolve(Type serviceType)
        {
            return _serviceResolver.Resolve(serviceType);
        }
        /// <summary>Try to retrieve a service from the container.
        /// </summary>
        /// <typeparam name="TService">The service type to resolve.</typeparam>
        /// <param name="instance">The resulting component instance providing the service, or default(TService).</param>
        /// <returns>True if a component providing the service is available.</returns>
        public bool TryResolve<TService>(out TService instance) where TService : class
        {
            return _serviceResolver.ResolveRequired();
        }
        /// <summary>Try to retrieve a service from the container.
        /// </summary>
        /// <param name="serviceType">The service type to resolve.</param>
        /// <param name="instance">The resulting component instance providing the service, or null.</param>
        /// <returns>True if a component providing the service is available.</returns>
        public bool TryResolve(Type serviceType, out object instance)
        {
            return _serviceResolver.TryResolve(serviceType, out instance);
        }
        /// <summary>Resolve a service.
        /// </summary>
        /// <typeparam name="TService">The service type.</typeparam>
        /// <param name="serviceName">The service name.</param>
        /// <returns>The component instance that provides the service.</returns>
        [Obsolete("没有实现")]
        public TService ResolveNamed<TService>(string serviceName) where TService : class
        {
            throw new NotImplementedException();
        }
        /// <summary>Resolve a service.
        /// </summary>
        /// <param name="serviceName">The service name.</param>
        /// <param name="serviceType">The service type.</param>
        /// <returns>The component instance that provides the service.</returns>
        [Obsolete("没有实现")]
        public object ResolveNamed(string serviceName, Type serviceType)
        {
            throw new NotImplementedException();
        }
        /// <summary>Try to retrieve a service from the container.
        /// </summary>
        /// <param name="serviceName">The name of the service to resolve.</param>
        /// <param name="serviceType">The type of the service to resolve.</param>
        /// <param name="instance">The resulting component instance providing the service, or null.</param>
        /// <returns>True if a component providing the service is available.</returns>
        [Obsolete("没有实现")]
        public bool TryResolveNamed(string serviceName, Type serviceType, out object instance)
        {
            throw new NotImplementedException();
        }

        public Lifetime PasreLifeStyle(LifeStyle lifeStyle)
        {
            if (lifeStyle == LifeStyle.Singleton)
            {
                return Lifetime.Singleton;
            }
            else
            {
                return Lifetime.Transient;
            }
        }
    }
}

