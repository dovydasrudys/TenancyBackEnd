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
    public class MessagesController : ControllerBase
    {
        private readonly TenancyContext _context;

        public MessagesController(TenancyContext context)
        {
            _context = context;
        }

        // GET: api/Messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetMessages()
        {
            string userId = User.FindFirst("id").Value;

            return await _context.Messages.Where(m => m.SenderId.ToString() == userId || m.ReceiverId.ToString() == userId).Select(m =>
                new
                {
                    m.Id,
                    m.Date,
                    m.SenderId,
                    m.ReceiverId,
                    m.Content
                }
                ).ToListAsync();
        }

        // POST: api/Messages
        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage(Message message)
        {
            string userId = User.FindFirst("id").Value;

            message.SenderId = int.Parse(userId);
            message.Date = DateTime.UtcNow;

            if (_context.Users.Find(message.ReceiverId) == null)
                return NotFound($"Receiver with id = {message.ReceiverId} could not be found");

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostMessage", new
            {
                message.Id,
                message.Date,
                message.SenderId,
                message.ReceiverId,
                message.Content
            });
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.Id == id);
        }
    }
}
