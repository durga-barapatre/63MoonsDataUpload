using DataUpload.Application.Interfaces;
using DataUpload.Application.Services;
using DataUpload.DAL.Dependency;
using DataUpload.DAL.Repositories;
using DataUpload.Domain.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Lifetime;

namespace DataUpload.Application.ServiceDependency
{
    public class ServiceDependencyRegister
    {
        public static IUnityContainer RegisterTypes(IUnityContainer container)
        {
            container = DomainDependencyRegister.RegisterTypes(container);
            container.RegisterType<IDataUpload, DataUploadService>(new PerThreadLifetimeManager());
            
            return container;
        }
    }
}
