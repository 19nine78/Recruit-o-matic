using Recruit_o_matic.Models;
using Recruit_o_matic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recruit_o_matic.Controllers
{
    public class HomeController : BaseRavenController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var positions = RavenSession.Query<Vacancy>().Where(x => x.Published).ToList();
            var viewModel = new HomeViewModel()
            {
                currentPositions = positions
            };
            return View(viewModel);
        }

    }
}
