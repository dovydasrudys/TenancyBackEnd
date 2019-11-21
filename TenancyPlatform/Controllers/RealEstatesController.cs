using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TenancyPlatform.Contexts;
using TenancyPlatform.Models;

namespace TenancyPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RealEstatesController : ControllerBase
    {
        private readonly TenancyContext _context;

        public RealEstatesController(TenancyContext context)
        {
            _context = context;
        }

        // GET: api/RealEstates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RealEstate>>> GetRealEstates()
        {
            return await _context.RealEstates.ToListAsync();
        }

        // GET: api/RealEstates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RealEstate>> GetRealEstate(int id)
        {
            var realEstate = await _context.RealEstates.FindAsync(id);

            if (realEstate == null)
            {
                return NotFound();
            }

            return realEstate;
        }

        // PUT: api/RealEstates/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRealEstate(int id, RealEstate realEstate)
        {
            if (id != realEstate.Id)
            {
                return BadRequest();
            }

            _context.Entry(realEstate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RealEstateExists(id))
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

        // POST: api/RealEstates
        [HttpPost]
        public async Task<ActionResult<RealEstate>> PostRealEstate(RealEstate realEstate)
        {
            _context.RealEstates.Add(realEstate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRealEstate", new { id = realEstate.Id }, realEstate);
        }

        // DELETE: api/RealEstates/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RealEstate>> DeleteRealEstate(int id)
        {
            var realEstate = await _context.RealEstates.FindAsync(id);
            if (realEstate == null)
            {
                return NotFound();
            }

            _context.RealEstates.Remove(realEstate);
            await _context.SaveChangesAsync();

            return realEstate;
        }

        private bool RealEstateExists(int id)
        {
            return _context.RealEstates.Any(e => e.Id == id);
        }
    }
}
