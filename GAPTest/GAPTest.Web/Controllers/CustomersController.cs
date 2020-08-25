using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GAPTest.Web.Data;
using GAPTest.Web.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using GAPTest.Web.Models;
using GAPTest.Web.Helpers;

namespace GAPTest.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CustomersController : Controller
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;
        private readonly ICombosHelper _combosHelper;
        private readonly IConverterHelper _converterHelper;

        public CustomersController(DataContext context,
            IUserHelper userHelper,
            ICombosHelper combosHelper,
            IConverterHelper converterHelper
            )
        {
            _context = context;
            _userHelper = userHelper;
            _combosHelper = combosHelper;
            _converterHelper = converterHelper;
        }

        // GET: Customers
        public IActionResult Index()
        {
            return View(_context.Customers
                .Include(o => o.User)
                .Include (o => o.Policies));
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(o => o.User)
                .Include(o => o.Policies)
                .ThenInclude (c => c.CoveringType)
                .Include(o => o.Policies)
                .ThenInclude(r => r.RiskType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Name = model.Name,
                    Document = model.Document,
                    PhoneNumber = model.CellPhone,
                    Address = model.Address,
                    Email = model.Username,
                    UserName = model.Username
                };

                var response = await _userHelper.AddUserAsync(user, model.Password);

                if (response.Succeeded)
                {
                    var userInDB = await _userHelper.GetUserByEmailAsync(model.Username);
                    await _userHelper.AddUserToRoleAsync(userInDB, "Customer");

                    var customer = new Customer
                    {
                        PolicyCustomers = new List<PolicyCustomer>(),
                        Policies = new List<Policy>(),
                        User = userInDB
                    };

                    _context.Customers.Add(customer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError(string.Empty, response.Errors.FirstOrDefault().Description);
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(o => o.User)
                .FirstOrDefaultAsync(o => o.Id == id.Value);
            if (customer == null)
            {
                return NotFound();
            }

            var model = new EditUserViewModel
            {
                Address = customer.User.Address,
                Document = customer.User.Document,
                Name = customer.User.Name,
                Id = customer.Id,
                CellPhone = customer.User.PhoneNumber
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = await _context.Customers
                    .Include(o => o.User)
                    .FirstOrDefaultAsync(o => o.Id == model.Id);

                customer.User.Document = model.Document;
                customer.User.Name = model.Name;
                customer.User.PhoneNumber = model.CellPhone;
                customer.User.Address = model.Address;

                await _userHelper.UpdateUserAsync(customer.User);
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .Include(p => p.User)
                .Include(p => p.Policies)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            if (customer.Policies.Count > 0)
            {
                ModelState.AddModelError(string.Empty, "This customer can't be removed");
                return RedirectToAction(nameof(Index));


            }

             await _userHelper.DeleteUserAsync(customer.User.Email);

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }

        public async Task<IActionResult> AddPolicy(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id.Value);
               
            if (customer == null)
            {
                return NotFound();
            }

            var model = new PolicyViewModel
            {
                CustomerId = customer.Id,
                CoveringTypes = _combosHelper.GetComboCoveringTypes(),
                RiskTypes = _combosHelper.GetComboRiskTypes(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddPolicy(PolicyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var policy = await _converterHelper.ToPolicyAsync(model, true);
                _context.Policies.Add(policy);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Customers", new { @id = model.CustomerId});
            }

            model.CoveringTypes = _combosHelper.GetComboCoveringTypes();
            model.RiskTypes = _combosHelper.GetComboRiskTypes();

            return View(model);
        }

        public async Task<IActionResult> EditPolicy(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policy = await _context.Policies
                .Include(p => p.Customer)
                .Include(p => p.CoveringType)
                .Include (p => p.RiskType)
                .FirstOrDefaultAsync(p => p.Id==id);

            if (policy == null)
            {
                return NotFound();
            }

            return View(_converterHelper.ToPolicyViewModel(policy));
        }
        [HttpPost]
        public async Task<IActionResult> EditPolicy(PolicyViewModel model)
        {
            if (ModelState.IsValid)
            {
                var policy = await _converterHelper.ToPolicyAsync(model, false);
                _context.Policies.Update(policy);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Customers", new { @id = model.CustomerId });
            }

            model.CoveringTypes = _combosHelper.GetComboCoveringTypes();
            model.RiskTypes = _combosHelper.GetComboRiskTypes();

            return View(model);
        }

        public async Task<IActionResult> DeletePolicy(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policy = await _context.Policies
                .Include(p => p.Customer)
                .FirstOrDefaultAsync(p => p.Id == id.Value);

            if (policy == null)
            {
                return NotFound();
            }

            _context.Policies.Remove(policy);
            await _context.SaveChangesAsync();
            return RedirectToAction($"{nameof(Details)}/{policy.Customer.Id}");
        }

    }
}
