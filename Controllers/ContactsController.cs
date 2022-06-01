using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TARpe19TodoApp.Data;
using TARpe19TodoApp.Models;
using TARpe19TodoApp.Models.Dto;

namespace TARpe19TodoApp.Controllers
{
    [AllowAnonymous]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public ContactsController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Kontakts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> GetContact()
        {
            return await _context.Contacts.ToListAsync();
        }

        // GET: api/Kontakts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
            {
                return NotFound();
            }

            return contact;
        }

        // PUT: api/Kontakts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContact(int id, Contact contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }

            _context.Entry(contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
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

        // POST: api/Kontakts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(NewContactDto contactdto)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == contactdto.UserId);

                if (user == null)
                {

                    return NotFound();
                }

                var contacttype = await _context.ContactTypes.FirstOrDefaultAsync(u => u.Id == contactdto.ContactTypeId);

                if (contacttype == null)
                {

                    return NotFound();
                }

                return CreatedAtAction(nameof(GetContact), new { id = contact.Id });
            }


            return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
        }

        // DELETE: api/Kontakts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.Id == id);
        }
    }
}
