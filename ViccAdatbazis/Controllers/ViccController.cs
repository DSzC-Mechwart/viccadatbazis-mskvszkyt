using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViccAdatbazis.Data;
using ViccAdatbazis.Models;

namespace ViccAdatbazis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViccController : ControllerBase
    {
        private readonly ViccDbContext _context;

        public ViccController(ViccDbContext context)
        {
            _context = context;
        }

        //lekérdezések
        [HttpGet]
        public async Task<ActionResult<List<Vicc>>> GetViccek()
        {
            return await _context.Viccek.Where(y => y.Aktiv).ToListAsync();
        }

        //lekérdezés
        [HttpGet("{id}")]
        public async Task<ActionResult<Vicc>> GetVicc(int id)
        {
            var vicc = await _context.Viccek.FindAsync(id);
            return vicc == null ? NotFound() : Ok(vicc);
        }

        //feltöltés
        [HttpPost]
        public async Task<ActionResult> PostVicc(Vicc ujVicc)
        {
            _context.Viccek.Add(ujVicc);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetVicc", new { id = ujVicc.Id }, ujVicc);
        }


        //módosítás
        [HttpPut("{id}")]
        public async Task<ActionResult> PutVicc(int id, Vicc modositott)
        {
            if (modositott.Id != id)
            {
                return BadRequest();
            }

            _context.Entry(modositott).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        //törlés
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteVicc(int id)
        {
            var torlendo = _context.Viccek.Find(id);
            if (torlendo == null) return NotFound();

            if (torlendo.Aktiv)
            {
                torlendo.Aktiv = false;
                _context.Entry(torlendo).State = EntityState.Modified;
            }

            else _context.Viccek.Remove(torlendo);

            await _context.SaveChangesAsync();
            return NoContent();
        }

        //like

        //dislike

    }
}
