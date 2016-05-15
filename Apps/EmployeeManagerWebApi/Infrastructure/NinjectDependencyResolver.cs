using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

using Ninject;

using Flagstone.Data.Employees;

namespace EmployeeManagerWebApi.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel m_kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            m_kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return m_kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return m_kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
#if DEBUG
            m_kernel.Bind<IUnitOfWorkFactory>().To<FakeUnitOfWorkFactory>().InSingletonScope();
#else
            m_kernel.Bind<IUnitOfWorkFactory>().To<EFUnitOfWorkFactory>();
#endif
        }
    }
}