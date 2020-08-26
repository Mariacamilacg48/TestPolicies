using GAPTest.Common.Models;
using GAPTest.Web.Data;
using GAPTest.Web.Data.Entities;
using GAPTest.Web.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GAPTest.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PoliciesController : ControllerBase
    {
        private readonly DataContext _datacontext;
        private readonly IConverterHelper _converterHelper;

        public PoliciesController(DataContext datacontext,
            IConverterHelper converterHelper)
        {
            _datacontext = datacontext;
            _converterHelper = converterHelper;
        }

        [HttpPost]
        [Route("PostPolicy")]

        public async Task<IActionResult> PostPolicy(PolicyRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _datacontext.Customers.FindAsync(request.CustomerId);
            if (customer == null)
            {
                return BadRequest("Not valid customer.");
            }

            var coveringType = await _datacontext.CoveringTypes.FindAsync(request.CoveringTypeId);
            if (coveringType == null)
            {
                return BadRequest("Not valid policy covering type.");
            }

            var riskType = await _datacontext.RiskTypes.FindAsync(request.RiskTypeId);
            if (coveringType == null)
            {
                return BadRequest("Not valid policy covering type.");
            }

            var policy = new Policy
            {
                PolicyName = request.PolicyName,
                Description = request.Description,
                PolicyStartDate = request.PolicyStartDate,
                CoveringPeriod = request.CoveringPeriod,
                Price = request.Price,
                Customer = customer,
                CoveringType = coveringType,
                RiskType = riskType
            };

            _datacontext.Policies.Add(policy);
            await _datacontext.SaveChangesAsync();
            return Ok(_converterHelper.ToPolicyResponse(policy));
        }

    }
}
