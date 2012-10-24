using Recruit_o_matic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recruit_o_matic.ViewModels.Admin
{
    public class DetailsViewModel
    {
        public Vacancy Vacancy { get; set; }
        public IList<Applicant> Applicants { get; set; }
    }
}