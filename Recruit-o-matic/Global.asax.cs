using Funq;
using Raven.Client;
using Raven.Client.Document;
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
using Recruit_o_matic.Services;
using Recruit_o_matic.Services.Interfaces;
using ServiceStack.Mvc;
using ServiceStack.WebHost.Endpoints;

namespace Recruit_o_matic
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        /// Web Service Singleton AppHost
        public class InfoAppHost : AppHostBase
        {
            //Tell Service Stack the name of your application and where to find your web services
            public InfoAppHost()
                : base("Services", typeof(MvcApplication).Assembly) { }

            public override void Configure(Funq.Container container)
            {
                 Store = new DocumentStore { ConnectionStringName = "RavenDB" };
            //Store.Conventions.ShouldCacheRequest = (url) => false;
            Store.Initialize();
               // IndexCreation.CreateIndexes(typeof(Vacancies_WithApplicantCount).Assembly, Store);
               // container.Register(Store);
                container.Register<IDocumentStore>(c => Store);

                container.Register(c => c.Resolve<IDocumentStore>().OpenSession())
                         .ReusedWithin(ReuseScope.Request);

                //container.RegisterAutoWiredAs<VacancyService,IVacancyService>();

                container.Register<ITestService>(c => new TestService());

                ControllerBuilder.Current.SetControllerFactory(
                    new FunqControllerFactory(container));
            }
        }

        public static DocumentStore Store; 

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutoMapperBootStrapper.Bootstrap();

           

            

            

            //Initialize your application
            var appHost = new InfoAppHost();
            appHost.Init();
        }
    }
}