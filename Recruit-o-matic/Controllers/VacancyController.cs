using AutoMapper;
using Raven.Client;
using Raven.Json.Linq;
using Recruit_o_matic.Models;
using Recruit_o_matic.ViewModels;
using ServiceStack.Logging;
using System;
using System.IO;
using System.Web.Mvc;

namespace Recruit_o_matic.Controllers
{
    public class VacancyController : BaseRavenController
    {
        //
        // GET: /Vacancy/

        public ILog log { get; set; }

        public IDocumentStore Store { get; set; }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(string id)
        {
            log.Debug("test!");
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
                    Store
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
