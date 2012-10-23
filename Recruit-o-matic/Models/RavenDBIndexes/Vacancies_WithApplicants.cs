using Raven.Client.Indexes;
using Raven.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recruit_o_matic.Models.RavenDBIndexes
{
    public class Vacancies_WithApplicants : AbstractIndexCreationTask<Applicant>
    {
       
    }
}