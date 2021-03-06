﻿using GAPTest.Common.Models;
using GAPTest.Web.Data.Entities;
using GAPTest.Web.Models;
using System.Threading.Tasks;

namespace GAPTest.Web.Helpers
{
    public interface IConverterHelper
    {
        Task<Policy> ToPolicyAsync(PolicyViewModel model, bool IsNew);
        PolicyViewModel ToPolicyViewModel(Policy policy);

        PolicyResponse ToPolicyResponse(Policy pet);
    }
}