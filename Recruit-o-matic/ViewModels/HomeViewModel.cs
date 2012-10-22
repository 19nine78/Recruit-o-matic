using Recruit_o_matic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recruit_o_matic.ViewModels
{
    public class HomeViewModel
    {
        public IList<Vacancy> currentPositions { get; set; }
    }
}