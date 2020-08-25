using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GAPTest.Web.Data;
using GAPTest.Web.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace GAPTest.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class CoveringTypesController : ControllerBase
    {
        private readonly DataContext _context;

        public CoveringTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/CoveringTypes
        [HttpGet]
        public IEnumerable<CoveringType> GetCoveringTypes()
        {
            return _context.CoveringTypes;
        }

        // GET: api/CoveringTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCoveringType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coveringType = await _context.CoveringTypes.FindAsync(id);

            if (coveringType == null)
            {
                return NotFound();
            }

            return Ok(coveringType);
        }

        // PUT: api/CoveringTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoveringType([FromRoute] int id, [FromBody] CoveringType coveringType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != coveringType.Id)
            {
                return BadRequest();
            }

            _context.Entry(coveringType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CoveringTypeExists(id))
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

        // POST: api/CoveringTypes
        [HttpPost]
        public async Task<IActionResult> PostCoveringType([FromBody] CoveringType coveringType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CoveringTypes.Add(coveringType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCoveringType", new { id = coveringType.Id }, coveringType);
        }

        // DELETE: api/CoveringTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoveringType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coveringType = await _context.CoveringTypes.FindAsync(id);
            if (coveringType == null)
            {
                return NotFound();
            }

            _context.CoveringTypes.Remove(coveringType);
            await _context.SaveChangesAsync();

            return Ok(coveringType);
        }

        private bool CoveringTypeExists(int id)
        {
            return _context.CoveringTypes.Any(e => e.Id == id);
        }
    }
}