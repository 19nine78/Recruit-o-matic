using Recruit_o_matic.Models;
using Recruit_o_matic.Models.RavenDBIndexes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recruit_o_matic.ViewModels.Admin
{
    public class AdminHomeViewModel
    {
        public VacancyGridViewModel Vacancies { get; set; }
        public int TotalPublishedVacancies { get; set; }
        public int TotalApplicants { get; set; }
    }    
}