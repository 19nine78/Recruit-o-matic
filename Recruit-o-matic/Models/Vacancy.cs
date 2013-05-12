using Raven.Imports.Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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

        [JsonIgnore]
        public bool IsClosingSoon
        {
            get
            {
                return this.ClosingDate - DateTime.Now <= new System.TimeSpan(3, 0, 0, 0) ? true : false;
            }
        }

        [JsonIgnore]
        public bool IsClosed
        {
            get
            {
                return this.ClosingDate < DateTime.Now ? true : false;
            }
        }
    }
}