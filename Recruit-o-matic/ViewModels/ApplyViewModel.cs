using Recruit_o_matic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Recruit_o_matic.ViewModels
{
    public class ApplyViewModel : Applicant
    {
        public HttpPostedFileBase File { get; set; }
    }
}