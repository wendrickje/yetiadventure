using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetiAdventure.LevelBuilder.Common
{

    public class ServiceContainer : IServiceProvider
    {
        private static readonly ServiceContainer _default = new ServiceContainer();

        private readonly object _lock;
        private readonly Dictionary<Type, object> _registry;

        public ServiceContainer()
        {
            _lock = new object();
            _registry = new Dictionary<Type, object>();
        }

        public static ServiceContainer Default
        {
            get { return _default; }
        }

        public void AddService(Type serviceType, object service)
        {
            lock (_lock)
            {
                _registry[serviceType] = service;
            }
            OnServiceSet(new ServiceEventArgs(serviceType));
        }

        public void AddService<TService>(TService service)
        {
            lock (_lock)
            {
                _registry[typeof(TService)] = service;
            }
            OnServiceSet(new ServiceEventArgs(typeof(TService)));
        }

        public object GetService(Type serviceType)
        {
            object service;
            lock (_lock)
            {
                _registry.TryGetValue(serviceType, out service);
            }
            return service;
        }

        public TService GetService<TService>()
            where TService : class
        {
            object service;
            lock (_lock)
            {
                _registry.TryGetValue(typeof(TService), out service);
            }
            return service as TService;
        }

        public void RemoveService(Type serviceType)
        {
            lock (_lock)
            {
                _registry.Remove(serviceType);
            }
            OnServiceSet(new ServiceEventArgs(serviceType));
        }

        public void RemoveService<TService>()
        {
            lock (_lock)
            {
                _registry.Remove(typeof(TService));
            }
            OnServiceSet(new ServiceEventArgs(typeof(TService)));
        }

        public event EventHandler<ServiceEventArgs> ServiceSet;

        protected virtual void OnServiceSet(ServiceEventArgs e)
        {
            if (ServiceSet != null)
                ServiceSet(this, e);
        }
    }
}
