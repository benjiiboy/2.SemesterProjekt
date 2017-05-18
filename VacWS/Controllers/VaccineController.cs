using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using VacWS;

namespace VacWS.Controllers
{
    public class VaccineController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: api/Vaccine
        public IQueryable<Vaccine> GetVaccine()
        {
            return db.Vaccine;
        }

        // GET: api/Vaccine/5
        [ResponseType(typeof(Vaccine))]
        public async Task<IHttpActionResult> GetVaccine(int id)
        {
            Vaccine vaccine = await db.Vaccine.FindAsync(id);
            if (vaccine == null)
            {
                return NotFound();
            }

            return Ok(vaccine);
        }

        // PUT: api/Vaccine/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutVaccine(int id, Vaccine vaccine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vaccine.Vac_Id)
            {
                return BadRequest();
            }

            db.Entry(vaccine).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VaccineExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Vaccine
        [ResponseType(typeof(Vaccine))]
        public async Task<IHttpActionResult> PostVaccine(Vaccine vaccine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vaccine.Add(vaccine);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VaccineExists(vaccine.Vac_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = vaccine.Vac_Id }, vaccine);
        }

        // DELETE: api/Vaccine/5
        [ResponseType(typeof(Vaccine))]
        public async Task<IHttpActionResult> DeleteVaccine(int id)
        {
            Vaccine vaccine = await db.Vaccine.FindAsync(id);
            if (vaccine == null)
            {
                return NotFound();
            }

            db.Vaccine.Remove(vaccine);
            await db.SaveChangesAsync();

            return Ok(vaccine);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VaccineExists(int id)
        {
            return db.Vaccine.Count(e => e.Vac_Id == id) > 0;
        }
    }
}