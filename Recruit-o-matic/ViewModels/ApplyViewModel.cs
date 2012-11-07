using Recruit_o_matic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recruit_o_matic.ViewModels
{
    public class ApplyViewModel
    {

        public string VacancyId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string CoverNote { get; set; }
        public HttpPostedFileBase File { get; set; }
    }
}