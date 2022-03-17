using InspectionAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionAPI.Controllers
{
    public class StatusController : MyBaseController
    {
        public StatusController(DataContext context) : base(context)
        {
        }

        // GET: api/Statuses
        [HttpGet]
        [ActionName(nameof(GetStatusesAsync))]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatusesAsync()
        {
            return await _context.Statuses.ToListAsync();
        }

        // GET: api/Statuses/5
        [HttpGet("{id}")]
        [ActionName(nameof(GetStatusAsync))]
        public async Task<ActionResult<Status>> GetStatusAsync(int id)
        {
            var status = await _context.Statuses.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }
            return status;
        }

        // PUT: api/Statuses/5
        // Protect from overposting attack: https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName(nameof(PutStatusAsync))]
        public async Task<ActionResult> PutStatusAsync(int id, Status status)
        {
            if (id != status.Id)
            {
                return BadRequest();
            }

            _context.Entry(status).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusExists(id))
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

        // POST: api/Statuses
        // Protect from overposting attack: https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName(nameof(PostStatusAsync))]
        public async Task<ActionResult<Status>> PostStatusAsync(Status status)
        {
            _context.Statuses.Add(status);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStatusAsync), new { id = status.Id }, status);
        }

        // DELETE: api/Inspections/5
        // Protect from overposting attack: https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpDelete("{id}")]
        [ActionName(nameof(DeleteStatusAsync))]
        public async Task<ActionResult<Status>> DeleteStatusAsync(int id)
        {
            var status = _context.Statuses.Find(id);
            if (status == null)
            {
                return NotFound();
            }
            _context.Statuses.Remove(status);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StatusExists(int id)
        {
            return _context.Statuses.Any(e => e.Id == id);
        }
    }
}