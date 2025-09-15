
using BookStoreApi.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_simple.Data;




namespace BookStoreApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public AuthorsController(AppDbContext db) => _db = db;

        [HttpGet]
        public Task<List<Author>> Get() => _db.Authors.ToListAsync();

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Author>> Get(int id)
        {
            var a = await _db.Authors.FindAsync(id);
            return a is null ? NotFound() : Ok(a);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Author a)
        {
            _db.Authors.Add(a);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = a.Id }, a);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, Author a)
        {
            if (id != a.Id) return BadRequest();
            _db.Entry(a).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var a = await _db.Authors.FindAsync(id);
            if (a is null) return NotFound();
            _db.Authors.Remove(a);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
