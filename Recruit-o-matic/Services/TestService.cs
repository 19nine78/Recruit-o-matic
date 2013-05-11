using Recruit_o_matic.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Recruit_o_matic.Services
{
    public class TestService : ITestService
    {
        public string Do(string input)
        {
            return input.ToUpper();
        }
    }
}