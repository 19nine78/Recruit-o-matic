﻿using Recruit_o_matic.Models;
using Recruit_o_matic.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Recruit_o_matic.Controllers
{
    public class HomeController : BaseRavenController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var positions = RavenSession.Query<Vacancy>()
                                        .VacanciesVisibleToThePublic()
                                        .ToList();

            var viewModel = new HomeViewModel()
            {
                currentPositions = positions,
            };

            return View(viewModel);
        }

    }
}
