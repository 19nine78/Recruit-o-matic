using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Recruit_o_matic.ViewModels.Admin;

namespace Recruit_o_matic.Services.Interfaces
{
    public interface IVacancyService
    {
        VacancyGridViewModel BuildVacancyGridViewModel(int page, int pageSize);

    }
}