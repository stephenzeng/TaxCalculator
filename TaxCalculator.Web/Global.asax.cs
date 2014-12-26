using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TaxCalculator.Dal;

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

            var context = new TaxCalculatorContext();
            Database.SetInitializer(new TaxCalculatorInitializer());
            context.Database.Initialize(true);
        }
    }
}
