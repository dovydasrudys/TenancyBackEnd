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
    public class ServicesController : ControllerBase
    {
        private readonly TenancyContext _context;

        public ServicesController(TenancyContext context)
        {
            _context = context;
        }

        // GET: api/Services
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetServices()
        {
            return await _context.Services.Select(s =>
                new
                {
                    s.Id,
                    s.Description,
                    s.Amount,
                    s.PaymentId
                }
                ).ToListAsync();
        }

        // GET: api/Services/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetService(int id)
        {
            var service = await _context.Services.FindAsync(id);

            if (service == null)
            {
                return NotFound();
            }

            return new
            {
                service.Id,
                service.Description,
                service.Amount,
                service.PaymentId
            };
        }

        // POST: api/Services
        [HttpPost]
        public async Task<ActionResult<Service>> PostService(Service service)
        {
            if (_context.Payments.Find(service.PaymentId) == null)
                return NotFound($"Payment with id = {service.PaymentId} could not be found");

            _context.Services.Add(service);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostService", new
            {
                service.Id,
                service.Description,
                service.Amount,
                service.PaymentId
            });
        }

        // DELETE: api/Services/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> DeleteService(int id)
        {
            var service = await _context.Services.Include(s => s.Payment).ThenInclude(p => p.Contract).FirstOrDefaultAsync(s => s.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            if (User.FindFirst("id").Value != service.Payment.Contract.LandlordId.ToString())
                return Forbid();

                _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return new
            {
                service.Id,
                service.Description,
                service.Amount,
                service.PaymentId
            };
        }

        private bool ServiceExists(int id)
        {
            return _context.Services.Any(e => e.Id == id);
        }
    }
}
