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
    public class RealEstatesController : ControllerBase
    {
        private readonly TenancyContext _context;

        public RealEstatesController(TenancyContext context)
        {
            _context = context;
        }

        // GET: api/RealEstates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetRealEstates()
        {
            return await _context.RealEstates.Select(re =>
                new
                {
                    re.Id,
                    re.Country,
                    re.City,
                    re.Street,
                    re.HouseNr,
                    re.Floor,
                    re.Area,
                    re.BuildYear,
                    re.OwnerId
                }
                ).ToListAsync();
        }

        // GET: api/RealEstates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetRealEstate(int id)
        {
            var realEstate = await _context.RealEstates.FindAsync(id);

            if (realEstate == null)
            {
                return NotFound();
            }

            return new
            {
                realEstate.Id,
                realEstate.Country,
                realEstate.City,
                realEstate.Street,
                realEstate.HouseNr,
                realEstate.Floor,
                realEstate.Area,
                realEstate.BuildYear,
                realEstate.OwnerId
            };
        }

        // PUT: api/RealEstates/5
        [Authorize(Roles = "landlord")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRealEstate(int id, RealEstate realEstate)
        {
            if (id != realEstate.Id)
            {
                return BadRequest();
            }

            RealEstate x = _context.RealEstates.AsNoTracking().FirstOrDefault(z => z.Id == id);
            if (x == null)
                return NotFound();

            if (User.FindFirst("id").Value != x.OwnerId.ToString())
                return Forbid();

            if (_context.Users.Find(realEstate.OwnerId) == null)
                return NotFound($"Owner with id = {realEstate.OwnerId} could not be found");

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
        [Authorize(Roles = "landlord")]
        [HttpPost]
        public async Task<ActionResult<RealEstate>> PostRealEstate(RealEstate realEstate)
        {
            if (_context.Users.Find(realEstate.OwnerId) == null)
                return NotFound($"Owner with id = {realEstate.OwnerId} could not be found");

            _context.RealEstates.Add(realEstate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostRealEstate", new
            {
                realEstate.Id,
                realEstate.Country,
                realEstate.City,
                realEstate.Street,
                realEstate.HouseNr,
                realEstate.Floor,
                realEstate.Area,
                realEstate.BuildYear,
                realEstate.OwnerId
            });
        }

        // DELETE: api/RealEstates/5
        [Authorize(Roles = "landlord")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> DeleteRealEstate(int id)
        {
            var realEstate = await _context.RealEstates.FindAsync(id);
            if (realEstate == null)
            {
                return NotFound();
            }

            if (User.FindFirst("id").Value != realEstate.OwnerId.ToString())
                return Forbid();

            _context.RealEstates.Remove(realEstate);
            await _context.SaveChangesAsync();

            return new
            {
                realEstate.Id,
                realEstate.Country,
                realEstate.City,
                realEstate.Street,
                realEstate.HouseNr,
                realEstate.Floor,
                realEstate.Area,
                realEstate.BuildYear,
                realEstate.OwnerId
            };
        }

        private bool RealEstateExists(int id)
        {
            return _context.RealEstates.Any(e => e.Id == id);
        }
    }
}
