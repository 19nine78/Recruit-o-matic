using Raven.Json.Linq;
using Recruit_o_matic.Models;
using Recruit_o_matic.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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

            if (applicantVM.File != null)
            {
                //need to do some validation on the file (type etc.) then save it somewhere
                Stream data = applicantVM.File.InputStream;
                applicant.AttachementId = applicant.Id + "/CV";
                MvcApplication.Store.DatabaseCommands.PutAttachment(applicant.AttachementId, null, data, new RavenJObject { {"Details","CV for vacancy " + VacancyId},
                                                                                                                        {"ContentType",applicantVM.File.ContentType}
                                                                                                                      });
            }

            return RedirectToAction("Index");
        }

    }
}
