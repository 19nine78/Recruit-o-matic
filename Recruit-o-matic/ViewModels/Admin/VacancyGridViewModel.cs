using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recruit_o_matic.ViewModels.Admin
{
    public class VacancyGridViewModel
    {
        public VacancyGridViewModel()
        {
            Vacancies = new List<VacancyGridRow>();
        }

        public IList<VacancyGridRow> Vacancies { get; set; }

    }

    public class VacancyGridRow
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime? PublishedOn { get; set; }
        public DateTime ClosingDate { get; set; }
        public bool Published { get; set; }
        public int ApplicantCount { get; set; }
    }

}