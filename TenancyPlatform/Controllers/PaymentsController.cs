﻿using System;
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
    public class PaymentsController : ControllerBase
    {
        private readonly TenancyContext _context;

        public PaymentsController(TenancyContext context)
        {
            _context = context;
        }

        // GET: api/Payments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetPayments()
        {
            return await _context.Payments.Include(p => p.Services).ToListAsync();
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetPayment(int id)
        {
            var payment = await _context.Payments.FindAsync(id);

            if (payment == null)
            {
                return NotFound();
            }

            return new
            {
                payment.Id,
                payment.ContractId,
                payment.PaymentStatus
            };
        }

        // PUT: api/Payments/5
        [Authorize(Roles = "landlord")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayment(int id, Payment payment)
        {
            if (id != payment.Id)
            {
                return BadRequest();
            }

            Payment x = _context.Payments.AsNoTracking().Include(z => z.Contract).FirstOrDefault(z => z.Id == id);
            if (x == null)
                return NotFound();

            if (User.FindFirst("id").Value != x.Contract.LandlordId.ToString())
                return Forbid();

            if(_context.Contracts.Find(payment.ContractId) == null)
                return NotFound($"Contract with id = {payment.ContractId} could not be found");

            _context.Entry(payment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentExists(id))
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

        // POST: api/Payments
        [Authorize(Roles = "landlord")]
        [HttpPost]
        public async Task<ActionResult<Payment>> PostPayment(Payment payment)
        {
            if (_context.Contracts.Find(payment.ContractId) == null)
                return NotFound($"Contract with id = {payment.ContractId} could not be found");

            payment.IssueDate = DateTime.Now;
            _context.Payments.Add(payment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostPayment", new
            {
                payment.Id,
                payment.ContractId,
                payment.PaymentStatus
            });
        }

        // DELETE: api/Payments/5
        [Authorize(Roles = "landlord")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<object>> DeletePayment(int id)
        {
            var payment = await _context.Payments.Include(p => p.Contract).FirstOrDefaultAsync(p => p.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            if (User.FindFirst("id").Value != payment.Contract.LandlordId.ToString())
                return Forbid();

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return new
            {
                payment.Id,
                payment.ContractId,
                payment.PaymentStatus
            };
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.Id == id);
        }
    }
}
