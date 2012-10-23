using Recruit_o_matic.Models;
using Recruit_o_matic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recruit_o_matic.Controllers
{
    public class VacancyController : BaseRavenController
    {
        //
        // GET: /Vacancy/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(string id)
        {
            var vacancy = RavenSession.Load<Vacancy>(id);
            var viewModel = new DetailsViewModel()
            {
                currentVacancy = vacancy,
                currentApplicant = new Applicant()
            };
            return View(viewModel);
        }

        [HttpPost]        
        public ActionResult Apply(string VacancyId, [Bind(Prefix = "currentApplicant")]Applicant applicant)
        {
            applicant.VacancyId = VacancyId;
            applicant.ApplicationDate = DateTime.Now;
            RavenSession.Store(applicant);

            return RedirectToAction("Index");
        }

    }
}
