using Raven.Client.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recruit_o_matic.Models
{
    public static class QueryHelpers
    {
        /// <summary>
        /// Filter helper to return o
        /// </summary>
        /// <param name="qry"></param>
        /// <returns></returns>
        public static IRavenQueryable<Vacancy> VacanciesVisibleToThePublic(this IRavenQueryable<Vacancy> qry)
        {
            return qry.Where(x => x.Published && x.ClosingDate >= DateTime.Now);
        }
    }
}