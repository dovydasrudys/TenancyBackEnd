using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TenancyPlatform.Contexts;
using TenancyPlatform.Models;

namespace TenancyPlatform.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertsController : ControllerBase
    {
        private readonly TenancyContext _context;

        public AdvertsController(TenancyContext context)
        {
            _context = context;
        }

        // GET: api/Adverts
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetAdverts()
        {
            return await _context.Adverts.Include(a => a.RealEstate).ToListAsync();
        }

        // GET: api/Adverts/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetAdvert(int id)
        {
            var advert = await _context.Adverts.FindAsync(id);

            if (advert == null)
            {
                return NotFound();
            }

            return new
            {
                advert.Id,
                advert.Description,
                advert.LoanPrice,
                advert.OwnerId,
                advert.RealEstateId
            };
        }

        // PUT: api/Adverts/5
        [Authorize(Roles = "landlord")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdvert(int id, Advert advert)
        {
            if (id != advert.Id)
            {
                return BadRequest();
            }

            Advert x = _context.Adverts.AsNoTracking().FirstOrDefault(z => z.Id == id);
            if (x == null)
                return NotFound();

            if (User.FindFirst("id").Value != x.OwnerId.ToString())
                return Forbid();

            if (_context.Users.Find(advert.OwnerId) == null)
                return NotFound($"Owner with id = {advert.OwnerId} could not be found");

            if (_context.RealEstates.Find(advert.RealEstateId) == null)
                return NotFound($"RealEstate with id = {advert.RealEstateId} could not be found");

            _context.Entry(advert).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdvertExists(id))
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

        // POST: api/Adverts
        [Authorize(Roles = "landlord")]
        [HttpPost]
        public async Task<ActionResult<Advert>> PostAdvert(Advert advert)
        {
            if (_context.Users.Find(advert.OwnerId) == null)
                return NotFound($"Owner with id = {advert.OwnerId} could not be found");

            if (_context.RealEstates.Find(advert.RealEstateId) == null)
                return NotFound($"RealEstate with id = {advert.RealEstateId} could not be found");

            _context.Adverts.Add(advert);
            await _context.SaveChangesAsync();

            Advert adv = _context.Adverts.AsNoTracking().Include(a => a.RealEstate).FirstOrDefault(a => a.Id == advert.Id);

            return CreatedAtAction("PostAdvert", adv);
        }

        // DELETE: api/Adverts/5
        [Authorize(Roles = "landlord")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> DeleteAdvert(int id)
        {
            var advert = await _context.Adverts.FindAsync(id);

            if (User.FindFirst("id").Value != advert.OwnerId.ToString())
                return Forbid();

            if (advert == null)
            {
                return NotFound();
            }

            _context.Adverts.Remove(advert);
            await _context.SaveChangesAsync();

            return new
            {
                advert.Id,
                advert.Description,
                advert.LoanPrice,
                advert.OwnerId,
                advert.RealEstateId
            };
        }

        private bool AdvertExists(int id)
        {
            return _context.Adverts.Any(e => e.Id == id);
        }
    }
}
