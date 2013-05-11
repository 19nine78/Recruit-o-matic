using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Raven.Client;
using Recruit_o_matic.Infrastructure;
using Recruit_o_matic.Models;
using Recruit_o_matic.Models.RavenDBIndexes;
using Recruit_o_matic.ViewModels.Admin;
using Raven.Client.Linq;
using Recruit_o_matic.Services.Interfaces;

namespace Recruit_o_matic.Services
{
    public class VacancyService : IVacancyService
    {
        private readonly IDocumentSession _ravenSession;

        public VacancyService()
        {
           // this._ravenSession = session;
        }

        public VacancyGridViewModel BuildVacancyGridViewModel(int page, int pageSize)
        {
            RavenQueryStatistics stats;

            var vacancies = _ravenSession.Query<Vacancy>()
                                        .Statistics(out stats)
                                        .Customize(x => x.WaitForNonStaleResults())
                                        .OrderBy(x => x.CreatedOn)
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();

            var applicantCounts = _ravenSession.Query<Applicant, Vacancies_WithApplicantCount>()
                                              .Where(x => x.VacancyId.In<string>(vacancies.Select(y => y.Id)))
                                              .Customize(x => x.WaitForNonStaleResults())
                                              .As<Vacancies_WithApplicantCount.VacancyApplicantCountResult>()
                                              .ToList();

            var vacancyRows = Mapper.Map<List<Vacancy>, List<VacancyGridRow>>(vacancies);

            //TODO: can automapper do this?
            vacancyRows.ToList().ForEach(x => x.ApplicantCount = applicantCounts.Where(y => y.VacancyId == x.Id)
                                                                                       .Select(y => y.Count)
                                                                                        .FirstOrDefault());

            var viewModel = new VacancyGridViewModel();

            viewModel.Vacancies = new PagedList<VacancyGridRow>(vacancyRows, (page - 1), pageSize, stats.TotalResults);

            return viewModel;
        }
    }
}