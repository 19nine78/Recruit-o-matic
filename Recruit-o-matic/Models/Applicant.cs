using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recruit_o_matic.Models
{
    public class Applicant
    {
        //Personal/contact details

        //cover details

        //CV

        public string Id { get; set; }
        public string VacancyId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string CoverNote { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string AttachementId { get; set; }
        public string DownloadFileName { get; set; }
    }
}