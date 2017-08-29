using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using BLL.Interfaces;
using BLL.IRepo;
using BLL.LogFolder;
using BLL.MapFolder;
using BLL.Services;
using DALwhithDapper.DapperRepository;
using Ninject;
using Ninject.Parameters;
using Ninject.Web.Common;
using Ninject.Web.Mvc.FilterBindingSyntax;
using WebApplication3.Filters;
using WebApplication3.Mapping;
using WebApplication3.NLog;

namespace WebApplication3.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {

        public IKernel Kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            Kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return Kernel.TryGet(serviceType, new IParameter[0]);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Kernel.GetAll(serviceType, new IParameter[0]);
        }

        private void AddBindings()
        {
            Kernel.BindFilter<PostActionFilter>(FilterScope.Action, 0).WhenControllerHas<PostActionFilter>();
            Kernel.Bind<IReaderService>().To<ReaderService>().InRequestScope();
            Kernel.Bind<IBookService>().To<BookService>().InRequestScope();
            Kernel.Bind<ILogService>().To<NLogService>().InRequestScope();
            Kernel.Bind<IUserService>().To<UserService>().InRequestScope();
            Kernel.Bind<IAccountService>().To<AccountService>().InRequestScope();
            Kernel.Bind<IOrderService>().To<OrderService>().InRequestScope();
            Kernel.Bind<IGenericMapper>().To<MyMapper>().InSingletonScope();
            Kernel.Bind<IMessage>().To<SendingMessage>().InSingletonScope();
            //Kernel.Bind<Library1Entities>().To<Library1Entities>().InSingletonScope(); 
            //Kernel.Bind<IBookRepository>().To<BookRepository>();
            //Kernel.Bind<IReaderRepository>().To<ReaderRepository>();
            //Kernel.Bind<IUserRepository>().To<UserRepository>();
            //Kernel.Bind<IRepository<OrderEntity>>().To<Repository<OrderEntity, Orders>>();
            Kernel.Bind<IDbConnection>().To<SqlConnection>().InRequestScope()
                .WithConstructorArgument("connectionString",
                    ConfigurationManager.ConnectionStrings["Library"].ConnectionString);
            Kernel.Bind<IBookRepository>().To<BookDapRepository>().InRequestScope();
            Kernel.Bind<IReaderRepository>().To<ReaderDapRepository>().InRequestScope();
            Kernel.Bind<IUserRepository>().To<UserDapRepository>().InRequestScope();
            Kernel.Bind<IOrderRepository>().To<OrderDapRepository>().InRequestScope();
         }
    }
}