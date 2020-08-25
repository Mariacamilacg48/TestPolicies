using GAPTest.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GAPTest.Web.Helpers
{
    public class LogicHelper : ILogicHelper
    {
        public bool IsHighRiskType(int riskTypeId)
        {
            if (riskTypeId == 4)
            {
                return true;
            }
             return false;
        }
    }
}
