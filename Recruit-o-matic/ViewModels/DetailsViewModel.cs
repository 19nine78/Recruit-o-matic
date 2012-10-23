using Recruit_o_matic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recruit_o_matic.ViewModels
{
    public class DetailsViewModel
    {
        public Vacancy currentVacancy { get; set; }
        public Applicant currentApplicant { get; set; }
    }
}