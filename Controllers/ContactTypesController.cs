using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TARpe19TodoApp.Data;
using TARpe19TodoApp.Models;
using TARpe19TodoApp.Models.Dto.Requests;
using TARpe19TodoApp.Models.Dto.Response;

namespace TARpe19TodoApp.Controllers
{
    [AllowAnonymous]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactTypesController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public ContactTypesController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/KontaktiTüüp
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactType>>> GetContactType()
        {
            return await _context.ContactTypes.ToListAsync();
        }

        // GET: api/KontaktiTüüp/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactType>> GetContactType(int id)
        {
            var contactType = await _context.ContactTypes.FindAsync(id);

            if (contactType == null)
            {
                
                return NotFound();
            }

            return contactType;
        }

        // PUT: api/KontaktiTüüp/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactType(int id, ContactType contactType)
        {
            if (id != contactType.Id)
            {
                return BadRequest();
            }

            _context.Entry(contactType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactTypeExists(id))
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

        // POST: api/KontaktiTüüp
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContactTypeResponse>> PostContactType(ContactTypeRequest contactTypeRequest)
        {
            if (ModelState.IsValid){

                var contactType = new ContactType();
                contactType.Type = contactTypeRequest.Type;



                _context.ContactTypes.Add(contactType);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetContactType), new { id = contactType.Id });
            }
            return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
        }

        // DELETE: api/KontaktiTüüp/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactType(int id)
        {
            var contactType = await _context.ContactTypes.FindAsync(id);
            if (contactType == null)
            {
                return NotFound();
            }

            _context.ContactTypes.Remove(contactType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactTypeExists(int id)
        {
            return _context.ContactTypes.Any(e => e.Id == id);
        }
    }
}
