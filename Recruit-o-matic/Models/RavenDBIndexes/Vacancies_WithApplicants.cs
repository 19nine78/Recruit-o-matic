using Raven.Client.Indexes;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recruit_o_matic.Models.RavenDBIndexes
{
    public class Vacancies_WithApplicantCount : AbstractIndexCreationTask<Applicant, Vacancies_WithApplicantCount.VacancyApplicantCountResult>
    {
        public class VacancyApplicantCountResult
        {
            public int Count { get; set; }
            public string VacancyId { get; set; }   
        }

        public Vacancies_WithApplicantCount()
        {
            Map = applicants => from applicant in applicants
                                select new { Count = 1, VacancyId = applicant.VacancyId };

            Reduce = results => from result in results
                                group result by result.VacancyId
                                    into g
                                    select new
                                    {
                                        Count = g.Sum(x => x.Count),
                                        VacancyId = g.Key
                                    };

        }
    }    
}