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
                currentApplicant = new ApplyViewModel()
            };
            return View(viewModel);
        }

        [HttpPost]        
        public ActionResult Apply(string VacancyId, [Bind(Prefix = "currentApplicant")]ApplyViewModel applicantVM)
        {

            //need to do some validation on the file (type etc.) then save it somewhere

            //Explore AutoMapper for doing the below
            var applicant = new Applicant()
            {
                VacancyId = VacancyId,
                FullName = applicantVM.FullName,
                Address = applicantVM.Address,
                EmailAddress = applicantVM.EmailAddress,
                CoverNote = applicantVM.CoverNote,
                TelephoneNumber = applicantVM.TelephoneNumber,
                ApplicationDate = DateTime.Now
            };
                        
            RavenSession.Store(applicant);

            return RedirectToAction("Index");
        }

    }
}
