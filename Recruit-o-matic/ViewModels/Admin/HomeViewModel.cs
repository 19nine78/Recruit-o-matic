using Recruit_o_matic.Models;
using Recruit_o_matic.Models.RavenDBIndexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recruit_o_matic.ViewModels.Admin
{
    public class HomeViewModel
    {
        public IList<Vacancy> Vacancies { get; set; }
        public IList<Vacancies_WithApplicantCount.VacancyApplicantCountResult> Counts { get; set; }
    }    
}