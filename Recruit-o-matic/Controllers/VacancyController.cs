using AutoMapper;
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
                {
                    VacancyId = id
                }
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Details([Bind(Prefix = "currentApplicant")]ApplyViewModel applicantVM)
        {
            if (ModelState.IsValid)
            {

                var applicant = Mapper.Map<ApplyViewModel, Applicant>(applicantVM);
                applicant.ApplicationDate = DateTime.Now;

                RavenSession.Store(applicant);

                if (applicantVM.File != null)
                {
                    applicant.AttachementId = GenerateAttachementId(applicant.Id);
                    Stream data = applicantVM.File.InputStream; 
                   
                    //better way to get a reference to this?
                    MvcApplication.Store
                                  .DatabaseCommands
                                  .PutAttachment(applicant.AttachementId, null, data, new RavenJObject { 
                                                                                                            {"Details","CV for vacancy " + applicant.VacancyId},
                                                                                                            {"ContentType",applicantVM.File.ContentType}
                                                                                                       });
                }
                    return RedirectToAction("/ThankYou");

            }
            else
            {
                return RedirectToAction("");
            }

            
        }

        public ActionResult ThankYou()
        {
            return View();
        }

        private string GenerateAttachementId(string applicantId)
        {

            return applicantId + "/cv";
        }
    }
}
