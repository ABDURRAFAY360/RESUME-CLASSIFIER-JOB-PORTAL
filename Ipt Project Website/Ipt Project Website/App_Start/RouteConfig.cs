﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ipt_Project_Website
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "Homepage",
            url: "",
            defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "Userlogin",
            url: "User/UserLogin",
            defaults: new { controller = "User", action = "UserLogin", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "Employerlogin",
            url: "User/UserLogin",
            defaults: new { controller = "Employer", action = "EmployerLogin", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "Createjob",
            url: "Employer/CreateJob",
            defaults: new { controller = "Employer", action = "CreateJob", id = UrlParameter.Optional }
            );
        }
    }
}
