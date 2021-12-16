using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Ipt_Project_Website
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        protected void Session_Start(object sender, EventArgs e)
        {
            Session["login"] = "0";
            Session["User"] = 0;
            Session["User_ID"] = 0;
            Session["Employer_ID"] = 0;
            Session["Model"] = 0;
            Session["UserModel"] = 0;
            Session["EmployerModel"] = 0;
            Session["Employer"] = 0;
        }
    }
}
