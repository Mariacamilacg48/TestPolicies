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
                Policies = customer.Policies.Select(o => new PolicyResponse
                {
                    Id = o.Id,
                    PolicyName = o.PolicyName,
                    Description = o.Description,
                    PolicyStartDate = o.PolicyStartDate,
                    CoveringPeriod = o.CoveringPeriod,
                    Price = o.Price,
                    State = o.State,
                    CoveringType = o.CoveringType.Name,
                    RiskType = o.RiskType.Name,
                }).ToList()
            };

            return Ok(response);
        }
    }
}
