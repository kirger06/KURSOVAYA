using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rieltors.API.Data;
using Rieltors.API.Models;

namespace Rieltors.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SellerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/seller
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seller>>> GetSellers()
        {
            var sellers = await _context.Sellers
                .Include(s => s.Properties)
                .Include(s => s.Deals)
                .ToListAsync();
            return Ok(sellers);
        }

        // GET: api/seller/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Seller>> GetSeller(int id)
        {
            var seller = await _context.Sellers
                .Include(s => s.Properties)
                .Include(s => s.Deals)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (seller == null)
                return NotFound(new { message = "Продавец не найден" });

            return Ok(seller);
        }

        // GET: api/seller/email/{email}
        [HttpGet("email/{email}")]
        public async Task<ActionResult<Seller>> GetSellerByEmail(string email)
        {
            var seller = await _context.Sellers
                .FirstOrDefaultAsync(s => s.Email == email);

            if (seller == null)
                return NotFound(new { message = "Продавец не найден" });

            return Ok(seller);
        }

        // GET: api/seller/statistics
        [HttpGet("statistics")]
        public async Task<ActionResult<object>> GetStatistics()
        {
            var stats = new
            {
                TotalSellers = await _context.Sellers.CountAsync(),
                TotalProperties = await _context.Properties.CountAsync(),
                ActiveProperties = await _context.Properties.CountAsync(p => p.IsActive),
                TotalDealsAmount = await _context.Deals.Where(d => d.Status == "Completed").SumAsync(d => d.Amount)
            };
            return Ok(stats);
        }

        // POST: api/seller
        [HttpPost]
        public async Task<ActionResult<Seller>> CreateSeller(CreateSellerDto createDto)
        {
            // Проверка уникальности email
            if (await _context.Sellers.AnyAsync(s => s.Email == createDto.Email))
                return BadRequest(new { message = "Email уже существует" });

            // Проверка уникальности телефона
            if (await _context.Sellers.AnyAsync(s => s.Phone == createDto.Phone))
                return BadRequest(new { message = "Номер телефона уже существует" });

            var seller = new Seller
            {
                FullName = createDto.FullName,
                Email = createDto.Email,
                Phone = createDto.Phone,
                Address = createDto.Address,
                PassportData = createDto.PassportData,
                BankDetails = createDto.BankDetails,
                Notes = createDto.Notes,
                RegistrationDate = DateTime.UtcNow
            };

            _context.Sellers.Add(seller);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSeller), new { id = seller.Id }, seller);
        }

        // PUT: api/seller/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSeller(int id, Seller seller)
        {
            if (id != seller.Id)
                return BadRequest();

            var existingSeller = await _context.Sellers.FindAsync(id);
            if (existingSeller == null)
                return NotFound(new { message = "Продавец не найден" });

            existingSeller.FullName = seller.FullName;
            existingSeller.Email = seller.Email;
            existingSeller.Phone = seller.Phone;
            existingSeller.Address = seller.Address;
            existingSeller.PassportData = seller.PassportData;
            existingSeller.BankDetails = seller.BankDetails;
            existingSeller.Notes = seller.Notes;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // GET: api/seller/5/properties
        [HttpGet("{id}/properties")]
        public async Task<ActionResult<IEnumerable<Property>>> GetSellerProperties(int id)
        {
            var properties = await _context.Properties
                .Where(p => p.SellerId == id && p.IsActive)
                .ToListAsync();

            return Ok(properties);
        }

        // DELETE: api/seller/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeller(int id)
        {
            var seller = await _context.Sellers
                .Include(s => s.Properties)
                .Include(s => s.Deals)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (seller == null)
                return NotFound(new { message = "Продавец не найден" });

            // Проверка на наличие активных сделок
            if (seller.Deals.Any(d => d.Status != "Completed"))
                return BadRequest(new { message = "Невозможно удалить продавца с активными сделками" });

            _context.Sellers.Remove(seller);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}