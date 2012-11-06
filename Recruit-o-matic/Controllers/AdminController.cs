﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Raven.Client;
using Raven.Client.Linq;
using Recruit_o_matic.Models;
using Recruit_o_matic.ViewModels.Admin;
using Recruit_o_matic.Models.RavenDBIndexes;
using Raven.Json.Linq;
using System.IO;
using Raven.Abstractions.Data;
using AutoMapper;
using Recruit_o_matic.Infrastructure;

namespace Recruit_o_matic.Controllers
{
    [Authorize]
    public class AdminController : BaseRavenController
    {
        //
        // GET: /Admin/

        public ActionResult Index(int page = 1, int pageSize = 3)
        {

            var viewModel = new AdminHomeViewModel();

            viewModel.Vacancies = BuildVacancyGridViewModel(page, pageSize);
            viewModel.TotalApplicants = RavenSession.Query<Applicant>().Count();
            viewModel.TotalPublishedVacancies = viewModel.Vacancies.Vacancies.Where(x => x.Published).Count();

            return View(viewModel);
        }

        public ActionResult VacancyPaging(int page, int pageSize)
        {
            return PartialView("_vacancyGrid", BuildVacancyGridViewModel(page, pageSize));
        }

        public ActionResult Details(string id)
        {

            var vacancy = RavenSession.Include<Applicant>(x => x.VacancyId)
                                      .Load<Vacancy>(id);

            var applicants = RavenSession.Query<Applicant>()
                                         .Where(x => x.VacancyId == id)
                                         .ToList();

            var viewModel = new DetailsViewModel()
            {
                Vacancy = vacancy,
                Applicants = applicants
            };

            return View(viewModel);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Vacancy vacancy)
        {
            var tmp = ModelState.IsValid;

            if (ModelState.IsValid)
            {
                try
                {
                    vacancy.CreatedOn = DateTime.Now;
                    RavenSession.Store(vacancy);
                    return RedirectToAction("Index");
                }
                catch
                {
                    ModelState.AddModelError(string.Empty, "There was a problem saving the Vacancy to the Database.");
                    return View(vacancy);
                }
            }
            else
            {
                return View(vacancy);
            }
        }

        public ActionResult Edit(string id)
        {
            var vacancy = RavenSession.Load<Vacancy>(id);

            return View(vacancy);
        }

        [HttpPost]
        public ActionResult Edit(Vacancy vacancy)
        {
            if (ModelState.IsValid)
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
            else
            {
                return View(vacancy);
            }
        }

        public ActionResult Delete(string id)
        {
            var vacancy = RavenSession.Load<Vacancy>(id);
            return View(vacancy);
        }

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
                vacancy.PublishedOn = DateTime.Now;

            return RedirectToAction("Index");
        }

        public ActionResult GetCV(string id)
        {

            Attachment attachement;

            try
            {
                attachement = MvcApplication.Store.DatabaseCommands.GetAttachment(id);
            }
            catch
            {
                return HttpNotFound("Atachement " + id + " was not found :(");
            }

            return File(ReadFully(attachement.Data()), attachement.Metadata["ContentType"].ToString(), id.Replace("/", "-"));

        }

        private static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        private VacancyGridViewModel BuildVacancyGridViewModel(int page, int pageSize)
        {
            RavenQueryStatistics stats;

            var vacancies = RavenSession.Query<Vacancy>()
                                        .Statistics(out stats)
                                        .Customize(x => x.WaitForNonStaleResults())
                                        .OrderBy(x => x.CreatedOn)
                                        .Skip((page - 1) * pageSize)
                                        .Take(3)
                                        .ToList();

            var applicantCounts = RavenSession.Query<Applicant, Vacancies_WithApplicantCount>()
                                              .Where(x => x.VacancyId.In<string>(vacancies.Select(y => y.Id)))
                                              .Customize(x => x.WaitForNonStaleResults())
                                              .As<Vacancies_WithApplicantCount.VacancyApplicantCountResult>()
                                              .ToList();

            var tmp = Mapper.Map<List<Vacancy>, List<VacancyGridRow>>(vacancies);
            //TODO: can automapper do this?
            tmp.ToList().ForEach(x => x.ApplicantCount = applicantCounts.Where(y => y.VacancyId == x.Id)
                                                                                        .Select(y => y.Count)
                                                                                        .FirstOrDefault());

            var viewModel = new VacancyGridViewModel();
            viewModel.TotalRecords = stats.TotalResults;
            viewModel.Vacancies = new PagedList<VacancyGridRow>(tmp, (page - 1), pageSize, stats.TotalResults);

            return viewModel;
        }
    }
}
