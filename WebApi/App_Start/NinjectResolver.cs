using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace WebApi.App_Start
{
    public class NinjectResolver : IDependencyResolver
    {
        private IKernel _container;
        public NinjectResolver(IKernel container)
        {
            _container = container;
        }
        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        public object GetService(Type serviceType)
        {
            return _container.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAll(serviceType);
        }
    }
}