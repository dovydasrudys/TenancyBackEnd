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
    public class ContractsController : ControllerBase
    {
        private readonly TenancyContext _context;

        public ContractsController(TenancyContext context)
        {
            _context = context;
        }

        // GET: api/Contracts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetContracts()
        {
            return await _context.Contracts.Select(c =>
                new
                {
                    c.Id,
                    c.Price,
                    c.Start,
                    c.Duration,
                    c.TenantId,
                    c.LandlordId,
                    c.RealEstateId
                }
                ).ToListAsync();
        }

        // GET: api/Contracts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetContract(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);

            if (contract == null)
            {
                return NotFound();
            }

            return new
            {
                contract.Id,
                contract.Price,
                contract.Start,
                contract.Duration,
                contract.TenantId,
                contract.LandlordId,
                contract.RealEstateId
            };
        }

        // PUT: api/Contracts/5
        [Authorize(Roles = "landlord")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContract(int id, Contract contract)
        {
            if (id != contract.Id)
            {
                return BadRequest();
            }

            Contract x = _context.Contracts.AsNoTracking().FirstOrDefault(z => z.Id == id);
            if (x == null)
                return NotFound();

            if (User.FindFirst("id").Value != x.LandlordId.ToString())
                return Forbid();

            if(_context.Users.Find(contract.TenantId) == null)
                return NotFound($"Tenant with id = {contract.TenantId} could not be found");

            if (_context.Users.Find(contract.LandlordId) == null)
                return NotFound($"Landlord with id = {contract.LandlordId} could not be found");

            if (_context.Users.Find(contract.RealEstateId) == null)
                return NotFound($"RealEstate with id = {contract.RealEstateId} could not be found");

            _context.Entry(contract).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractExists(id))
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

        // POST: api/Contracts
        [Authorize(Roles = "landlord")]
        [HttpPost]
        public async Task<ActionResult<Contract>> PostContract(Contract contract)
        {
            if (_context.Users.Find(contract.TenantId) == null)
                return NotFound($"Tenant with id = {contract.TenantId} could not be found");

            if (_context.Users.Find(contract.LandlordId) == null)
                return NotFound($"Landlord with id = {contract.LandlordId} could not be found");

            if (_context.Users.Find(contract.RealEstateId) == null)
                return NotFound($"RealEstate with id = {contract.RealEstateId} could not be found");

            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostContract", new
            {
                contract.Id,
                contract.Price,
                contract.Start,
                contract.Duration,
                contract.TenantId,
                contract.LandlordId,
                contract.RealEstateId
            });
        }

        // DELETE: api/Contracts/5
        [Authorize(Roles = "landlord")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> DeleteContract(int id)
        {
            var contract = await _context.Contracts.FindAsync(id);

            if (User.FindFirst("id").Value != contract.LandlordId.ToString())
                return Forbid();

            if (contract == null)
            {
                return NotFound();
            }

            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();

            return new
            {
                contract.Id,
                contract.Price,
                contract.Start,
                contract.Duration,
                contract.TenantId,
                contract.LandlordId,
                contract.RealEstateId
            };
        }

        private bool ContractExists(int id)
        {
            return _context.Contracts.Any(e => e.Id == id);
        }
    }
}
