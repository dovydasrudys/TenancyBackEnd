using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TenancyPlatform.Contexts;
using TenancyPlatform.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace TenancyPlatform.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class FailuresController : ControllerBase
    {
        private readonly TenancyContext _context;

        public FailuresController(TenancyContext context)
        {
            _context = context;
        }

        // GET: api/Failures
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Failure>>> GetFailures()
        {
            return await _context.Failures.ToListAsync();
        }

        // GET: api/Failures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Failure>> GetFailure(int id)
        {
            var failure = await _context.Failures.FindAsync(id);

            if (failure == null)
            {
                return NotFound();
            }

            return failure;
        }

        // PUT: api/Failures/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFailure(int id, Failure failure)
        {
            if (id != failure.Id)
            {
                return BadRequest();
            }

            _context.Entry(failure).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FailureExists(id))
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

        // POST: api/Failures
        [HttpPost]
        public async Task<ActionResult<Failure>> PostFailure(Failure failure)
        {
            _context.Failures.Add(failure);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFailure", new { id = failure.Id }, failure);
        }

        // DELETE: api/Failures/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Failure>> DeleteFailure(int id)
        {
            var failure = await _context.Failures.FindAsync(id);
            if (failure == null)
            {
                return NotFound();
            }

            _context.Failures.Remove(failure);
            await _context.SaveChangesAsync();

            return failure;
        }

        private bool FailureExists(int id)
        {
            return _context.Failures.Any(e => e.Id == id);
        }
    }
}
