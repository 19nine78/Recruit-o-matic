using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Recruit_o_matic.Models
{
    public class Vacancy
    {
        public Vacancy()
        {
            ClosingDate = DateTime.Now.AddDays(14);
            Published = false;
        }

        public string Id { get; set; }

        [Required]
        [Display(Name="Vacancy Title")]
        public string Title { get; set; }

        [Required]
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Vacancy Description")]
        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }
        
        public DateTime? PublishedOn { get; set; }
        
        [DisplayName("Closing Date")]
        public DateTime ClosingDate { get; set; }
        
        public bool Published { get; set; }
    }
}