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
    public class VaccinersController : ApiController
    {
        private VacContext db = new VacContext();

        // GET: api/Vacciners
        public IQueryable<Vacciner> GetVacciner()
        {
            return db.Vacciner;
        }

        // GET: api/Vacciners/5
        [ResponseType(typeof(Vacciner))]
        public async Task<IHttpActionResult> GetVacciner(int id)
        {
            Vacciner vacciner = await db.Vacciner.FindAsync(id);
            if (vacciner == null)
            {
                return NotFound();
            }

            return Ok(vacciner);
        }

        // PUT: api/Vacciners/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutVacciner(int id, Vacciner vacciner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vacciner.Vac_Id)
            {
                return BadRequest();
            }

            db.Entry(vacciner).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VaccinerExists(id))
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

        // POST: api/Vacciners
        [ResponseType(typeof(Vacciner))]
        public async Task<IHttpActionResult> PostVacciner(Vacciner vacciner)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vacciner.Add(vacciner);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = vacciner.Vac_Id }, vacciner);
        }

        // DELETE: api/Vacciners/5
        [ResponseType(typeof(Vacciner))]
        public async Task<IHttpActionResult> DeleteVacciner(int id)
        {
            Vacciner vacciner = await db.Vacciner.FindAsync(id);
            if (vacciner == null)
            {
                return NotFound();
            }

            db.Vacciner.Remove(vacciner);
            await db.SaveChangesAsync();

            return Ok(vacciner);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VaccinerExists(int id)
        {
            return db.Vacciner.Count(e => e.Vac_Id == id) > 0;
        }
    }
}