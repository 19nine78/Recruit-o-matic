using Recruit_o_matic.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recruit_o_matic.ViewModels.Admin
{
    public class VacancyGridViewModel
    {
        public PagedList<VacancyGridRow> Vacancies { get; set; }
        public string GridTitle { get; set; }
}

    public class VacancyGridRow
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime? PublishedOn { get; set; }
        public DateTime ClosingDate { get; set; }
        public bool Published { get; set; }
        public int ApplicantCount { get; set; }
        public bool IsClosingSoon { get; set; }
        public bool IsClosed { get; set; }
    }

}