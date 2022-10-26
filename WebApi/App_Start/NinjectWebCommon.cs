[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebApi.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(WebApi.App_Start.NinjectWebCommon), "Stop")]

namespace WebApi.App_Start
{
    using System;
    using System.Web;
    using AutoMapper;
    using BusinessLayer.Model.Interfaces;
    using BusinessLayer.Services;
    using DataAccessLayer.Database;
    using DataAccessLayer.Model.Interfaces;
    using DataAccessLayer.Model.Models;
    using DataAccessLayer.Repositories;
    using Microsoft.Extensions.Logging;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            //company DI
            kernel.Bind<ICompanyService>().To<CompanyService>().InRequestScope();
            kernel.Bind<ICompanyRepository>().To<CompanyRepository>().InRequestScope();
            kernel.Bind<IDbWrapper<Company>>().To<InMemoryDatabase<Company>>().InRequestScope();
            //employee DI
            kernel.Bind<IEmployeeService>().To<EmployeeService>().InRequestScope();
            kernel.Bind<IEmployeeRepository>().To<EmployeeRepository>().InRequestScope();
            kernel.Bind<IDbWrapper<Employee>>().To<InMemoryDatabase<Employee>>().InRequestScope();

            //adjustment to keep cache, in order to make easy tests:
            kernel.Bind<IMemoryInCache>().To<MemoryInCache>().InSingletonScope();
            //mapper
            kernel.Bind<IMapper>()
            .ToMethod(context =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<AppServicesProfile>();
                    // tell automapper to use ninject when creating value converters and resolvers
                    cfg.ConstructServicesUsing(t => kernel.Get(t));
                });
                return config.CreateMapper();
            }).InSingletonScope();
            //logger
            kernel.Bind<ILogger<CompanyService>>().To<LoggerManager<CompanyService>>().InSingletonScope();
            kernel.Bind<ILogger<EmployeeService>>().To<LoggerManager<EmployeeService>>().InSingletonScope();

        }


    }
}