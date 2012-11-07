using AutoMapper;
using Recruit_o_matic.Models;
using Recruit_o_matic.ViewModels;
using Recruit_o_matic.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recruit_o_matic.Infrastructure
{
    public static class AutoMapperBootStrapper
    {
        public static void Bootstrap()
        {
            Mapper.CreateMap<Vacancy, VacancyGridRow>();
            Mapper.CreateMap<ApplyViewModel, Applicant>();
        }
    }
}