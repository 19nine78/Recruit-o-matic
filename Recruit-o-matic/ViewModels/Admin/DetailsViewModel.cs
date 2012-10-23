using Recruit_o_matic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recruit_o_matic.ViewModels.Admin
{
    public class DetailsViewModel
    {
        public Vacancy vacancy { get; set; }
        public IList<Applicant> applicants { get; set; }
    }
}