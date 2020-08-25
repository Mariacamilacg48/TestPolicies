using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GAPTest.Web.Data;
using GAPTest.Web.Data.Entities;

namespace GAPTest.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class RiskTypesController : ControllerBase
    {
        private readonly DataContext _context;

        public RiskTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/RiskTypes
        [HttpGet]
        public IEnumerable<RiskType> GetRiskTypes()
        {
            return _context.RiskTypes;
        }

        // GET: api/RiskTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRiskType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var riskType = await _context.RiskTypes.FindAsync(id);

            if (riskType == null)
            {
                return NotFound();
            }

            return Ok(riskType);
        }

        // PUT: api/RiskTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRiskType([FromRoute] int id, [FromBody] RiskType riskType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != riskType.Id)
            {
                return BadRequest();
            }

            _context.Entry(riskType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RiskTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RiskTypes
        [HttpPost]
        public async Task<IActionResult> PostRiskType([FromBody] RiskType riskType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.RiskTypes.Add(riskType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRiskType", new { id = riskType.Id }, riskType);
        }

        // DELETE: api/RiskTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRiskType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var riskType = await _context.RiskTypes.FindAsync(id);
            if (riskType == null)
            {
                return NotFound();
            }

            _context.RiskTypes.Remove(riskType);
            await _context.SaveChangesAsync();

            return Ok(riskType);
        }

        private bool RiskTypeExists(int id)
        {
            return _context.RiskTypes.Any(e => e.Id == id);
        }
    }
}