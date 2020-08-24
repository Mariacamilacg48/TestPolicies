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
    public class CoveringTypesController : Controller
    {
        private readonly DataContext _context;

        public CoveringTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: CoveringTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.CoveringTypes.ToListAsync());
        }

        // GET: CoveringTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coveringType = await _context.CoveringTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coveringType == null)
            {
                return NotFound();
            }

            return View(coveringType);
        }

        // GET: CoveringTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CoveringTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CoveringType coveringType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coveringType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coveringType);
        }

        // GET: CoveringTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coveringType = await _context.CoveringTypes.FindAsync(id);
            if (coveringType == null)
            {
                return NotFound();
            }
            return View(coveringType);
        }

        // POST: CoveringTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CoveringType coveringType)
        {
            if (id != coveringType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coveringType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoveringTypeExists(coveringType.Id))
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
            return View(coveringType);
        }

        // GET: CoveringTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var coveringType = await _context.CoveringTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coveringType == null)
            {
                return NotFound();
            }

            return View(coveringType);
        }

        // POST: CoveringTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coveringType = await _context.CoveringTypes.FindAsync(id);
            _context.CoveringTypes.Remove(coveringType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoveringTypeExists(int id)
        {
            return _context.CoveringTypes.Any(e => e.Id == id);
        }
    }
}
