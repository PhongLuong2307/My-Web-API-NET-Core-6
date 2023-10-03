using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApiNetCore6.Data;

namespace MyApiNetCore6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NovelsController : ControllerBase
    {
        private readonly NovelStoreContext _context;

        public NovelsController(NovelStoreContext context)
        {
            _context = context;
        }

        // GET: api/Novels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Novel>>> GetNovel()
        {
          if (_context.Novel == null)
          {
              return NotFound();
          }
            return await _context.Novel!.ToListAsync();
        }

        // GET: api/Novels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Novel>> GetNovel(int id)
        {
          if (_context.Novel == null)
          {
              return NotFound();
          }
            var novel = await _context.Novel!.FindAsync(id);

            if (novel == null)
            {
                return NotFound();
            }

            return novel;
        }

        // PUT: api/Novels/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNovel(int id, Novel novel)
        {
            if (id != novel.Id)
            {
                return BadRequest();
            }

            _context.Entry(novel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NovelExists(id))
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

        // POST: api/Novels
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Novel>> PostNovel(Novel novel)
        {
          if (_context.Novel == null)
          {
              return Problem("Entity set 'NovelStoreContext.Novel'  is null.");
          }
            _context.Novel!.Add(novel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNovel", new { id = novel.Id }, novel);
        }

        // DELETE: api/Novels/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNovel(int id)
        {
            if (_context.Novel == null)
            {
                return NotFound();
            }
            var novel = await _context.Novel!.FindAsync(id);
            if (novel == null)
            {
                return NotFound();
            }

            _context.Novel!.Remove(novel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NovelExists(int id)
        {
            return (_context.Novel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
