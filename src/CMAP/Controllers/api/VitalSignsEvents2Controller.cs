using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using CMAP.Models;
using System.Collections.Generic;
using System.Linq;
using CMAP.Model;

namespace CMAP.Controllers
{
    [Produces("application/json")]
    [Route("api/Tenants/{tenantId}/Persons/{personId}/[controller]")]
    public class VitalSignsEvents2Controller : Controller
    {
        private ApplicationDbContext _context;

        public VitalSignsEvents2Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/VitalSignsEvents
        [HttpGet]
        public IEnumerable<VitalSignsEvent> GetVitalSignsEvents([FromRoute] int tenantId, [FromRoute] int personId)
        {
            var data =
                _context.VitalSignsEvents
                .Include(t => t.VitalSigns)
                //.ForPatientDescByEventDT(personId)
                ;
            return data;
        }

        // GET: api/VitalSignsEvents/5
        [HttpGet("{id}", Name = "GetVitalSignsEvent")]
        public IActionResult GetVitalSignsEvent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            var vitalSignsEvent = _context.VitalSignsEvents
                .Include(t => t.VitalSigns)
                .SingleOrDefault(m => m.Id == id);

            if (vitalSignsEvent == null)
            {
                return HttpNotFound();
            }

            return Ok(vitalSignsEvent);
        }

        // PUT: api/VitalSignsEvents/5
        [HttpPut("{id}")]
        public IActionResult PutVitalSignsEvent(int id, [FromBody] VitalSignsEvent vitalSignsEvent)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            if (id != vitalSignsEvent.Id)
            {
                return HttpBadRequest();
            }

            _context.Update(vitalSignsEvent, GraphBehavior.IncludeDependents);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VitalSignsEventExists(id))
                {
                    return HttpNotFound();
                }
                else
                {
                    throw;
                }
            }

            return new HttpStatusCodeResult(StatusCodes.Status204NoContent);
        }

        // POST: api/VitalSignsEvents
        [HttpPost]
        public IActionResult PostVitalSignsEvent([FromBody] VitalSignsEvent vitalSignsEvent, [FromRoute] int personId)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            vitalSignsEvent.PatientId = personId;
            _context.VitalSignsEvents.Add(vitalSignsEvent, GraphBehavior.IncludeDependents);

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (VitalSignsEventExists(vitalSignsEvent.Id))
                {
                    return new HttpStatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetVitalSignsEvent", new { id = vitalSignsEvent.Id }, vitalSignsEvent);
        }

        // DELETE: api/VitalSignsEvents/5
        [HttpDelete("{id}")]
        public IActionResult DeleteVitalSignsEvent(int id)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            var vitalSignsEvent = _context.VitalSignsEvents
                .Include(t => t.VitalSigns)
                .SingleOrDefault(m => m.Id == id);

            if (vitalSignsEvent == null)
            {
                return HttpNotFound();
            }

            _context.VitalSignsEvents.Remove(vitalSignsEvent);
            _context.SaveChanges();

            return Ok(vitalSignsEvent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VitalSignsEventExists(int id)
        {
            return _context.VitalSignsEvents.Count(e => e.Id == id) > 0;
        }
    }
}