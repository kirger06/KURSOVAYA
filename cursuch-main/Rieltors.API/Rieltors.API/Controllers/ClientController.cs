using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rieltors.API.Data;
using Rieltors.API.Models;

namespace Rieltors.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
            return await _context.Clients.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
                return NotFound();
            return client;
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<Client>>> GetByStatus(string status)
        {
            var clients = await _context.Clients
                .Where(c => c.Status == status)
                .ToListAsync();
            return Ok(clients);
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<Client>>> GetActiveClients()
        {
            var clients = await _context.Clients
                .Where(c => c.Status == "Активный поиск")
                .ToListAsync();
            return Ok(clients);
        }

        [HttpPost]
        public async Task<ActionResult<Client>> CreateClient(Client client)
        {
            if (await _context.Clients.AnyAsync(c => c.Email == client.Email))
                return BadRequest(new { message = "Email already exists" });

            if (await _context.Clients.AnyAsync(c => c.PhoneNumber == client.PhoneNumber))
                return BadRequest(new { message = "Phone number already exists" });

            client.RegistrationDate = DateTime.UtcNow;
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClient), new { id = client.Id }, client);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, Client client)
        {
            if (id != client.Id)
                return BadRequest();

            var existingClient = await _context.Clients.FindAsync(id);
            if (existingClient == null)
                return NotFound();

            existingClient.FirstName = client.FirstName;
            existingClient.LastName = client.LastName;
            existingClient.PhoneNumber = client.PhoneNumber;
            existingClient.Email = client.Email;
            existingClient.MinPrice = client.MinPrice;
            existingClient.MaxPrice = client.MaxPrice;
            existingClient.PreferredDistrict = client.PreferredDistrict;
            existingClient.MinRooms = client.MinRooms;
            existingClient.PropertyType = client.PropertyType;
            existingClient.Status = client.Status;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
                return NotFound();

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}