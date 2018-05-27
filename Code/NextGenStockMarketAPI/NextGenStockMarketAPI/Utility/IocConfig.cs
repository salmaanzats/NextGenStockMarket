using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using NextGenStockMarket.Service;
using NextGenStockMarket.Service.Interface;

namespace NextGenStockMarketAPI.Utility
{
    public class IocConfig
    {
        public static IContainer Register()
        {
            var config = GlobalConfiguration.Configuration;
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);
   
            builder.RegisterType<BankService>().As<IBankService>().InstancePerRequest();
            builder.RegisterType<StockMarketService>().As<IStockMarketService>().InstancePerRequest();
            builder.RegisterType<BrokerService>().As<IBrokerService>().InstancePerRequest();
            
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            return container;
        }
    }
}