using Recruit_o_matic.Models;
using Recruit_o_matic.Services.Interfaces;
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

        private ITestService _testService { get; set; }

        public HomeController(ITestService service)
        {
            _testService = service;
        }

        public ActionResult Index()
        {
            var positions = RavenSession.Query<Vacancy>()
                                        .VacanciesVisibleToThePublic()
                                        .ToList();

            var viewModel = new HomeViewModel()
            {
                currentPositions = positions,
            };

            ViewData["test"] = _testService.Do("hello");

            return View(viewModel);
        }

    }
}
