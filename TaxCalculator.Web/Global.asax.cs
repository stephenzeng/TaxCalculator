using System.Data.Entity;
using System.Data.SqlClient;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using TaxCalculator.Dal;
using TaxCalculator.Service.Iterfaces;

namespace TaxCalculator.Web
{
    public class MvcApplication : HttpApplication
    {
        public MvcApplication()
        {
            BeginRequest += (sender, args) =>
            {
                HttpContext.Current.Items["CurrentRequestDataContext"] = new TaxCalculatorContext();
            };

            EndRequest += (sender, args) =>
            {
                using (var context = (TaxCalculatorContext) HttpContext.Current.Items["CurrentRequestDataContext"])
                {
                    if (context == null)
                        return;

                    if (Server.GetLastError() != null)
                        return;

                    context.SaveChanges();
                }
            };
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            SetupDependencyInjection();
            SetupDataContext();
        }

        private static void SetupDataContext()
        {
            SqlConnection.ClearAllPools();
            var context = new TaxCalculatorContext();
            Database.SetInitializer(new TaxCalculatorInitializer());
            context.Database.Initialize(true);
        }

        private static void SetupDependencyInjection()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterAssemblyTypes(typeof(ICalculateService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerRequest();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
