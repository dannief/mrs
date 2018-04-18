using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MRS.Domain.Interfaces;
using MRS.Infrastructure.Common.Cache;
using MRS.Infrastructure.EFData;
using MRS.Infrastructure.CrossCutting;
using MRS.Application;
using MRS.Application.Interfaces;
using SimpleInjector;
using SimpleInjector.Extensions;
using MRS.Infrastructure.Patterns;
using MRS.Infrastructure.Common.Commands;

namespace MRS.Infrastructure.DI
{
    public class SimpleInjectorDIContainer : IContainer
    {
        private Container container;

        public SimpleInjectorDIContainer()
        {
            container = new Container();

            // Register context
            container.RegisterPerWebRequest<MrsContext>();
            
            // Register repositories
            container.Register<IRequestRepository, RequestRepository>();
            container.Register<ILookupRepository, LookupRepository>();            
            container.Register<IUserRepository, UserRepository>();
            
            // Application services
            container.Register<IRequestService, RequestService>();
            container.Register<ILookupService, LookupService>();
            container.RegisterManyForOpenGeneric(typeof(ICommandHandler<>), AppDomain.CurrentDomain.GetAssemblies());

            // -------- BEGIN INFRASTRUCTURE CLASSES --------

            // Register cache provider
            container.Register<ICacheProvider, MemoryCacheProvider>();

            // Register decorators
            //container.RegisterDecorator(typeof(ILookupRepository), typeof(LookupRepositoryCacheDecorator));
            //container.RegisterDecorator(typeof(IRequestRepository), typeof(RequestRepositoryAuditDecorator));

            // -------- END INFRASTRUCTURE CLASSES --------
        }

        public T GetInstance<T>() where T : class
        {
            return container.GetInstance<T>();
        }
    }
}
