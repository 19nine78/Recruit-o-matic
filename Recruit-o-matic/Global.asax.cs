﻿using Raven.Client.Document;
using Raven.Client.Indexes;
using Recruit_o_matic.Infrastructure;
using Recruit_o_matic.Models.RavenDBIndexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Recruit_o_matic
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {

        public static DocumentStore Store;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutoMapperBootStrapper.Bootstrap();

            Store = new DocumentStore { ConnectionStringName = "RavenDB" };
            //Store.Conventions.ShouldCacheRequest = (url) => false;
            Store.Initialize();

            IndexCreation.CreateIndexes(typeof(Vacancies_WithApplicantCount).Assembly, Store);
        }
    }
}