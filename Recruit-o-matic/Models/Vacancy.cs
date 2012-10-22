using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

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
        public string Title { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? PublishedOn { get; set; }
        [DisplayName("Closing Date")]
        public DateTime ClosingDate { get; set; }
        public bool Published { get; set; }
    }
}