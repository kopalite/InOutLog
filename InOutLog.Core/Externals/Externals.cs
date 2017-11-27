using System;
using System.Collections.Generic;

namespace InOutLog.Core
{
    public static class Externals
    {
        private class Registration
        {
            private Func<object> _resolver;

            private object _service;

            public Func<object> Resolver { get; private set; }

            public Registration(Func<object> resolver, bool isSingleton)
            {
                _resolver = resolver;

                if (isSingleton)
                {
                    Resolver = () =>
                    {
                        if (_service == null)
                        {
                            _service = _resolver();
                        }
                        return _service;
                    };
                }
                else
                {
                    Resolver = () => _resolver();
                }
            }
        }

        private static readonly Dictionary<Type, Registration> _resolvers = new Dictionary<Type, Registration>();

        private static bool _isLocked;

        public static void Lock()
        {
            _isLocked = true;
        }

        public static void Register<TService>(Func<object> resolver, bool isSingleton = false) where TService : class
        {
            if (!_isLocked)
            {
                _resolvers.Add(typeof(TService), new Registration(resolver, isSingleton));
            }
        }

        public static TService Resolve<TService>() where TService : class
        {
            var registration = _resolvers[typeof(TService)];
            var service = registration.Resolver();
            return service as TService;
        }
    }
}
