using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recruit_o_matic.Models
{
    public class Applicant
    {
        public string Id { get; set; }
        public string VacancyId { get; set; }
        public string FullName { get; set; }
        public DateTime ApplicationDate { get; set; }
    }
}