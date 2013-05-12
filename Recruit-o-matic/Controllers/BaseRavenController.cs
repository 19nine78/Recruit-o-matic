using Raven.Client;
using ServiceStack.Mvc;
using System.Web.Mvc;

namespace Recruit_o_matic.Controllers
{
    public class BaseRavenController : ServiceStackController
    {

        public IDocumentSession RavenSession { get; protected set; }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //RavenSession.
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.IsChildAction)
                return;

            using (RavenSession)
            {
                if (filterContext.Exception != null)
                    return;

                if (RavenSession != null)
                    RavenSession.SaveChanges();
            }
        }

    }
}
