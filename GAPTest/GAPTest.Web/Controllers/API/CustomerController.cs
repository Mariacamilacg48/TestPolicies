using GAPTest.Common.Models;
using GAPTest.Web.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using GAPTest.Web.Helpers;
using GAPTest.Web.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace GAPTest.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomerController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IUserHelper _userHelper;

        public CustomerController(DataContext dataContext,
            IUserHelper userHelper
            )
        {
            _dataContext = dataContext;
            _userHelper = userHelper;
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

        [HttpPost]
        [Route("PostUser")]
        public async Task<IActionResult> PostUser(UserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();

            }

            var user = await _userHelper.GetUserByEmailAsync(request.Email);
            if (user != null)
            {
                return BadRequest(new Response<object>
                {
                    IsSuccess = false,
                    Message = "This email already exists."
                });
            }

            user = new User
            {
                Name = request.Name,
                Document = request.Document,
                Address = request.Email,
                PhoneNumber = request.CellPhone,
                Email = request.Email,
                UserName = request.Email,
            };

            var result = await _userHelper.AddUserAsync(user, request.Password);

            if (result != IdentityResult.Success)
            {
                return BadRequest(result.Errors.FirstOrDefault().Description);
            }

            await _userHelper.AddUserToRoleAsync(user, "Customer");
            _dataContext.Customers.Add(new Customer { User = user });
            await _dataContext.SaveChangesAsync();

            return Ok(new Response<object> { IsSuccess = true,
            Message="Created"}); 
        }

        [HttpPut]
        [Route("PutUser")]
        public async Task<IActionResult> PutUser(UserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userHelper.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                return BadRequest("User not found.");
            }

            user.Name = request.Name;
            user.Document = request.Document;
            user.Address = request.Address;
            user.PhoneNumber = request.CellPhone;

            var respose = await _userHelper.UpdateUserAsync(user);

            if (!respose.Succeeded)
            {
                return BadRequest(respose.Errors.FirstOrDefault().Description);
            }

            var updatedUser = await _userHelper.GetUserByEmailAsync(request.Email);
            return Ok(updatedUser);
        }
    }
}
