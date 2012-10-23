using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raven.Client;
using Raven.Client.Linq;
using Recruit_o_matic.Models;
using Recruit_o_matic.ViewModels.Admin;
using Recruit_o_matic.Models.RavenDBIndexes;

namespace Recruit_o_matic.Controllers
{
    public class AdminController : BaseRavenController
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {

            var viewModel = new HomeViewModel()
            {
                //TODO: A bit fragile, will break down if more than 128 vacancies (Raven soft limit)
                // vacancies needs to be pages & counts tied to it.
                
                Vacancies = RavenSession.Query<Vacancy>()
                                        .OrderBy(x => x.CreatedOn)
                                        .ToList(),
                
                Counts = RavenSession.Query<Applicant, Vacancies_WithApplicantCount>()
                                                     .As<Vacancies_WithApplicantCount.VacancyApplicantCountResult>()
                                                     .ToList()
            };

            return View(viewModel);
        }

        //
        // GET: /Admin/Details/5

        public ActionResult Details(string id)
        {
            var vacancy = RavenSession.Include<Applicant>(x => x.VacancyId).Load<Vacancy>(id);
            var applicants = RavenSession.Query<Applicant>().Where(x => x.VacancyId == id).ToList(); 

            var viewModel = new DetailsViewModel()
            {
                vacancy = vacancy,
                applicants = applicants

            };

            return View(viewModel);
        }

        //
        // GET: /Admin/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Admin/Create

        [HttpPost]
        public ActionResult Create(Vacancy vacancy)
        {
            try
            {
                // TODO: Add insert logic here
                vacancy.CreatedOn = DateTime.Now;
                RavenSession.Store(vacancy);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Edit/5

        public ActionResult Edit(string id)
        {
            var vacancy = RavenSession.Load<Vacancy>(id);

            return View(vacancy);
        }

        //
        // POST: /Admin/Edit/5

        [HttpPost]
        public ActionResult Edit(Vacancy vacancy)
        {
            try
            {
                RavenSession.Store(vacancy);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Admin/Delete/5

        public ActionResult Delete(string id)
        {
            var vacancy = RavenSession.Load<Vacancy>(id);
            return View(vacancy);
        }

        //
        // POST: /Admin/Delete/5

        [HttpPost]
        public ActionResult DeleteConfirm(string id)
        {
            try
            {
                var vacancy = RavenSession.Load<Vacancy>(id);
                RavenSession.Delete<Vacancy>(vacancy);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Publish(string id)
        {
            var vacancy = RavenSession.Load<Vacancy>(id);

            vacancy.Published = vacancy.Published ? false : true;

            if (vacancy.Published && vacancy.PublishedOn == null)
            {
                vacancy.PublishedOn = DateTime.Now;
            }

            return RedirectToAction("Index");


        }
    }
}
