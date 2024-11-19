using ContactManagementAPI.Models;
using ContactManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly ContactService _contactService;

        public ContactsController()
        {
            _contactService = new ContactService();
        }

        // GET: api/contacts
        [HttpGet]
        public IActionResult GetAll()
        {
            var contacts = _contactService.GetAll();
            return Ok(contacts);
        }

        // GET: api/contacts/{id}
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var contact = _contactService.GetById(id);
            if (contact == null) return NotFound($"Contact with ID {id} not found.");
            return Ok(contact);
        }

        // POST: api/contacts
        [HttpPost]
        public IActionResult Create([FromBody] Contact contact)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var newContact = _contactService.Add(contact);
            return CreatedAtAction(nameof(GetById), new { id = newContact.Id }, newContact);
        }

        // PUT: api/contacts/{id}
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Contact contact)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            if (id != contact.Id) return BadRequest("ID mismatch.");

            var updated = _contactService.Update(contact);
            if (!updated) return NotFound($"Contact with ID {id} not found.");

            return NoContent();
        }

        // DELETE: api/contacts/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _contactService.Delete(id);
            if (!deleted) return NotFound($"Contact with ID {id} not found.");
            return NoContent();
        }
    }
}
