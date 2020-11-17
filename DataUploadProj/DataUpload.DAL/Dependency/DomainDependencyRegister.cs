using DataUpload.DAL.Contexts;
using DataUpload.DAL.Repositories;
using DataUpload.Domain.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace DataUpload.DAL.Dependency
{
    public static class DomainDependencyRegister
    {
        [System.Obsolete]
        public static IUnityContainer RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<DbContext>(new PerThreadLifetimeManager(), new InjectionFactory(x => new ApplicationContext()));
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>), new PerThreadLifetimeManager());
            container.RegisterType<IDataUploadRepository, DataUploadRepository>(new PerThreadLifetimeManager());
            return container;
        }
    }
}
