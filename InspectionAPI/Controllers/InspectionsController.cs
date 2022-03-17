using InspectionAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionAPI.Controllers
{
    public class InspectionsController : MyBaseController
    {
        public InspectionsController(DataContext context) : base(context)
        {
        }

        // GET: api/Inspections
        [HttpGet]
        [ActionName(nameof(GetInspectionsAsync))]
        public async Task<ActionResult<IEnumerable<Inspection>>> GetInspectionsAsync()
        {
            return await _context.Inspections.ToListAsync();
        }

        // GET: api/Inspections/5
        [HttpGet("{id}")]
        [ActionName(nameof(GetInspectionAsync))]
        public async Task<ActionResult<Inspection>> GetInspectionAsync(int id)
        {
            var inspection = await _context.Inspections.FindAsync(id);
            if (inspection == null)
            {
                return NotFound();
            }
            return inspection;
        }

        // PUT: api/Inspections/5
        // Protect from overposting attack: https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName(nameof(PutInspectionAsync))]
        public async Task<ActionResult> PutInspectionAsync(int id, Inspection inspection)
        {
            if (id != inspection.Id)
            {
                return BadRequest();
            }

            _context.Entry(inspection).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InspectionExists(id))
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

        // POST: api/Inspections
        // Protect from overposting attack: https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName(nameof(PostInspectionAsync))]
        public async Task<ActionResult<Inspection>> PostInspectionAsync(Inspection inspection)
        {
            _context.Inspections.Add(inspection);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInspectionAsync), new { id = inspection.Id }, inspection);
        }

        // DELETE: api/Inspections/5
        // Protect from overposting attack: https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpDelete("{id}")]
        [ActionName(nameof(DeleteInspectionAsync))]
        public async Task<ActionResult<Inspection>> DeleteInspectionAsync(int id)
        {
            var inspection = _context.Inspections.Find(id);
            if (inspection == null)
            {
                return NotFound();
            }
            _context.Inspections.Remove(inspection);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InspectionExists(int id)
        {
            return _context.Inspections.Any(e => e.Id == id);
        }
    }
}