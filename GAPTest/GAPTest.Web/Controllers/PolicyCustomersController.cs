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

namespace GAPTest.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PolicyCustomersController : Controller
    {
        private readonly DataContext _context;

        public PolicyCustomersController(DataContext context)
        {
            _context = context;
        }

        // GET: PolicyCustomers
        public async Task<IActionResult> Index()
        {
            return View(await _context.PolicyCustomer.ToListAsync());
        }

        // GET: PolicyCustomers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policyCustomer = await _context.PolicyCustomer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (policyCustomer == null)
            {
                return NotFound();
            }

            return View(policyCustomer);
        }

        // GET: PolicyCustomers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PolicyCustomers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CoveringPercentage,State")] PolicyCustomer policyCustomer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(policyCustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(policyCustomer);
        }

        // GET: PolicyCustomers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policyCustomer = await _context.PolicyCustomer.FindAsync(id);
            if (policyCustomer == null)
            {
                return NotFound();
            }
            return View(policyCustomer);
        }

        // POST: PolicyCustomers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CoveringPercentage,State")] PolicyCustomer policyCustomer)
        {
            if (id != policyCustomer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(policyCustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PolicyCustomerExists(policyCustomer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(policyCustomer);
        }

        // GET: PolicyCustomers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var policyCustomer = await _context.PolicyCustomer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (policyCustomer == null)
            {
                return NotFound();
            }

            return View(policyCustomer);
        }

        // POST: PolicyCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var policyCustomer = await _context.PolicyCustomer.FindAsync(id);
            _context.PolicyCustomer.Remove(policyCustomer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PolicyCustomerExists(int id)
        {
            return _context.PolicyCustomer.Any(e => e.Id == id);
        }
    }
}
