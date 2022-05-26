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
using TARpe19TodoApp.Models.Dto.Response;

namespace TARpe19TodoApp.Controllers
{
    [AllowAnonymous]
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public UsersController(ApiDbContext context)
        {
            _context = context;
        }

        // GET: api/Kasutajas
        [HttpGet]
        [AllowAnonymous]
        public async Task<List<GetUserResponse>> GetUser()
        {
            var users = await _context.Users.ToListAsync();


            //return  await _context.Users.ToListAsync();
            return users;
            
        }

        // GET: api/Kasutajas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u=>u.Id==id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Kasutajas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Kasutajas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /*[HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (ModelState.IsValid) { 
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }

            return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
        }
        */
        // POST: api/Kasutajas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(NewUserDto userdto)
        {
            if (ModelState.IsValid)
            {
                var contacttype = await _context.ContactTypes.FirstOrDefaultAsync(u => u.Id == userdto.ContactTypeId);

                if (contacttype == null)
                {

                    return NotFound();
                }

                var user = new User();
                user.firstName = userdto.firstName;
                user.lastName = userdto.lastName;
                var contact = new Contact();
                contact.Value = userdto.ContactValue;
                contact.ContactType = contacttype;

                user.Contacts = new List<Contact>();
                user.Contacts.Add(contact);

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }


            return new JsonResult("Somethign Went wrong") { StatusCode = 500 };
        }


        // DELETE: api/Kasutajas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.Include(c => c.Contacts).FirstOrDefaultAsync(k => k.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            foreach (var contact in user.Contacts)
            {
                _context.Contacts.Remove(contact);
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        private GetUserResponse ConvertToDto(User user)
        {
            var result =

                return result;
        }
    }
}
