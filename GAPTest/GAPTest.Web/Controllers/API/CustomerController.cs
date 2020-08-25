using GAPTest.Common.Models;
using GAPTest.Web.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace GAPTest.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public CustomerController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpPost]
        [Route("GetCustomerByEmail")]
        public async Task<IActionResult> GetCustomer(EmailRequestcs emailRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var customer = await _dataContext.Customers
                .Include(o => o.User)
                .Include(o => o.Policies)
                .ThenInclude(o => o.CoveringType)
                .Include(o => o.Policies)
                .ThenInclude(o => o.RiskType)
                .FirstOrDefaultAsync(o => o.User.UserName.ToLower() == emailRequest.Email.ToLower());

            var response = new CustomerResponse
            {
                Id = customer.Id,
                Name = customer.User.Name,
                Document = customer.User.Document,
                Address = customer.User.Address,
                CellPhone = customer.User.PhoneNumber,
                Policies = customer.Policies.Select(p => new PolicyResponse
                {
                    PolicyName = p.PolicyName,
                    Id = p.Id,
                    Description = p.Description,
                    PolicyStartDate = p.PolicyStartDate,
                    CoveringPeriod = p.CoveringPeriod,
                    CoveringPercentage=p.CoveringPercentage,
                    Price = p.Price,
                    State = p.State,
                    CoveringType = p.CoveringType.Name,
                    RiskType = p.RiskType.Name,
                }).ToList()
            };

            return Ok(response);
        }
    }
}
