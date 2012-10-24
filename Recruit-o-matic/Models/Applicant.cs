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

        [Required]
        [Display(Name="Full Name")]
        public string FullName { get; set; }

        [Required]        
        [Display(Name="Address")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Contact Number")]
        [DataType(DataType.PhoneNumber)]
        public string TelephoneNumber { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Cover Letter")]
        public string CoverNote { get; set; }       
        
        public DateTime ApplicationDate { get; set; }

        public string AttachementId { get; set; }
        public string DownloadFileName { get; set; }
    }
}