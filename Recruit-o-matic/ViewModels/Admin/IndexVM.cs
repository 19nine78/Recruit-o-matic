using Recruit_o_matic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recruit_o_matic.ViewModels.Admin
{
    public class IndexVM
    {
        public IList<Position> Positions { get; set; }
    }
}