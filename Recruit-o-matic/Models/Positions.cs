using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Recruit_o_matic.Models
{
    public class Position
    {
        public Position()
        {
            CreatedOn = DateTime.Now;
            Published = false;
        }

        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [DisplayName("Created On")]
        public DateTime CreatedOn { get; set; }
        public DateTime? PublishedOn { get; set; }
        [DisplayName("Closing Date")]
        public DateTime? ClosingDate { get; set; }
        public bool Published { get; set; }
    }
}