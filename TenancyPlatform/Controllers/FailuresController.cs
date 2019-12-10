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
        public async Task<ActionResult<IEnumerable<object>>> GetFailures()
        {
            return await _context.Failures.Select(f =>
                new
                {
                    f.Id,
                    f.Description,
                    f.IsFixed,
                    f.ContractId,
                    f.ReporterId
                }
                ).ToListAsync();
        }

        // GET: api/Failures/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetFailure(int id)
        {
            var failure = await _context.Failures.FindAsync(id);

            if (failure == null)
            {
                return NotFound();
            }

            return new
            {
                failure.Id,
                failure.Description,
                failure.IsFixed,
                failure.ContractId,
                failure.ReporterId
            };
        }

        // PUT: api/Failures/5
        [Authorize(Roles = "tenant")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFailure(int id, Failure failure)
        {
            
            if (id != failure.Id)
            {
                return BadRequest();
            }

            Failure x = _context.Failures.AsNoTracking().FirstOrDefault(z => z.Id == id);
            if (x == null)
                return NotFound();

            if (User.FindFirst("id").Value != x.ReporterId.ToString())
                return Forbid();

            if (_context.Users.Find(failure.ReporterId) == null)
                return NotFound($"Reporter with id = {failure.ReporterId} could not be found");

            if (_context.Users.Find(failure.ContractId) == null)
                return NotFound($"Contract with id = {failure.ContractId} could not be found");

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
        [Authorize(Roles = "tenant")]
        [HttpPost]
        public async Task<ActionResult<Failure>> PostFailure(Failure failure)
        {
            if (_context.Users.Find(failure.ReporterId) == null)
                return NotFound($"Reporter with id = {failure.ReporterId} could not be found");

            if (_context.Users.Find(failure.ContractId) == null)
                return NotFound($"Contract with id = {failure.ContractId} could not be found");

            _context.Failures.Add(failure);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostFailure", new
            {
                failure.Id,
                failure.Description,
                failure.IsFixed,
                failure.ContractId,
                failure.ReporterId
            });
        }

        // DELETE: api/Failures/5
        [Authorize(Roles = "tenant")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> DeleteFailure(int id)
        {
            var failure = await _context.Failures.FindAsync(id);
            if (failure == null)
            {
                return NotFound();
            }

            if (User.FindFirst("id").Value != failure.ReporterId.ToString())
                return Forbid();

            _context.Failures.Remove(failure);
            await _context.SaveChangesAsync();

            return new
            {
                failure.Id,
                failure.Description,
                failure.IsFixed,
                failure.ContractId,
                failure.ReporterId
            };
        }

        private bool FailureExists(int id)
        {
            return _context.Failures.Any(e => e.Id == id);
        }
    }
}
