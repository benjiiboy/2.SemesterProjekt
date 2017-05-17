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
    public class SkemaController : ApiController
    {
        private DataBaseContext db = new DataBaseContext();

        // GET: api/Skema
        public IQueryable<Skema> GetSkema()
        {
            return db.Skema;
        }

        // GET: api/Skema/5
        [ResponseType(typeof(Skema))]
        public async Task<IHttpActionResult> GetSkema(int id)
        {
            Skema skema = await db.Skema.FindAsync(id);
            if (skema == null)
            {
                return NotFound();
            }

            return Ok(skema);
        }

        // PUT: api/Skema/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSkema(int id, Skema skema)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != skema.Vac_Id)
            {
                return BadRequest();
            }

            db.Entry(skema).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkemaExists(id))
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

        // POST: api/Skema
        [ResponseType(typeof(Skema))]
        public async Task<IHttpActionResult> PostSkema(Skema skema)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Skema.Add(skema);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = skema.Vac_Id }, skema);
        }

        // DELETE: api/Skema/5
        [ResponseType(typeof(Skema))]
        public async Task<IHttpActionResult> DeleteSkema(int id)
        {
            Skema skema = await db.Skema.FindAsync(id);
            if (skema == null)
            {
                return NotFound();
            }

            db.Skema.Remove(skema);
            await db.SaveChangesAsync();

            return Ok(skema);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SkemaExists(int id)
        {
            return db.Skema.Count(e => e.Vac_Id == id) > 0;
        }
    }
}