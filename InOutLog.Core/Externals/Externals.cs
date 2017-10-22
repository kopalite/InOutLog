using System;
using System.Collections.Generic;

namespace InOutLog.Core
{
    public static class Externals
    {
        private static readonly Dictionary<Type, Func<object>> _resolvers = new Dictionary<Type, Func<object>>();

        public static void Register<TService>(Func<object> resolver) where TService : class
        {
            _resolvers.Add(typeof(TService), () => resolver());
        }

        public static TService Resolve<TService>() where TService : class
        {
            var resolver = _resolvers[typeof(TService)];
            var service = resolver();
            return service as TService;
        }
    }
}
