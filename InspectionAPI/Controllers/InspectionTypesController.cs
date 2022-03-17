using InspectionAPI.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InspectionAPI.Controllers
{
    public class InspectionTypesController : MyBaseController
    {
        public InspectionTypesController(DataContext context) : base(context)
        {
        }

        // GET: api/Inspections
        [HttpGet]
        [ActionName(nameof(GetInspectionTypesAsync))]
        public async Task<ActionResult<IEnumerable<InspectionType>>> GetInspectionTypesAsync()
        {
            return await _context.InspectionTypes.ToListAsync();
        }

        // GET: api/InspectionTypes/5
        [HttpGet("{id}")]
        [ActionName(nameof(GetInspectionTypeAsync))]
        public async Task<ActionResult<InspectionType>> GetInspectionTypeAsync(int id)
        {
            var inspectionType = await _context.InspectionTypes.FindAsync(id);
            if (inspectionType == null)
            {
                return NotFound();
            }
            return inspectionType;
        }

        // PUT: api/Inspections/5
        // Protect from overposting attack: https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ActionName(nameof(PutInspectionTypeAsync))]
        public async Task<ActionResult> PutInspectionTypeAsync(int id, InspectionType inspectionType)
        {
            if (id != inspectionType.Id)
            {
                return BadRequest();
            }

            _context.Entry(inspectionType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InspectionTypeExists(id))
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

        // POST: api/InspectionTypes
        // Protect from overposting attack: https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ActionName(nameof(PostInspectionTypeAsync))]
        public async Task<ActionResult<InspectionType>> PostInspectionTypeAsync([FromBody] InspectionType inspectionType)
        {
            _context.InspectionTypes.Add(inspectionType);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInspectionTypeAsync), new { id = inspectionType.Id }, inspectionType);
        }

        // DELETE: api/Inspections/5
        // Protect from overposting attack: https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpDelete("{id}")]
        [ActionName(nameof(DeleteInspectionTypeAsync))]
        public async Task<ActionResult<InspectionType>> DeleteInspectionTypeAsync(int id)
        {
            var inspectionType = _context.InspectionTypes.Find(id);
            if (inspectionType == null)
            {
                return NotFound();
            }
            _context.InspectionTypes.Remove(inspectionType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InspectionTypeExists(int id)
        {
            return _context.InspectionTypes.Any(e => e.Id == id);
        }
    }
}