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
using VacAppWS;

namespace VacAppWS.Controllers
{
    public class BarnController : ApiController
    {
        private VaccAppContext db = new VaccAppContext();

        // GET: api/Barn
        public IQueryable<Barn> GetBarn()
        {
            return db.Barn;
        }

        // GET: api/Barn/5
        [ResponseType(typeof(Barn))]
        public async Task<IHttpActionResult> GetBarn(int id)
        {
            Barn barn = await db.Barn.FindAsync(id);
            if (barn == null)
            {
                return NotFound();
            }

            return Ok(barn);
        }

        // PUT: api/Barn/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBarn(int id, Barn barn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != barn.Barn_Id)
            {
                return BadRequest();
            }

            db.Entry(barn).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BarnExists(id))
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

        // POST: api/Barn
        [ResponseType(typeof(Barn))]
        public async Task<IHttpActionResult> PostBarn(Barn barn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Barn.Add(barn);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = barn.Barn_Id }, barn);
        }

        // DELETE: api/Barn/5
        [ResponseType(typeof(Barn))]
        public async Task<IHttpActionResult> DeleteBarn(int id)
        {
            Barn barn = await db.Barn.FindAsync(id);
            if (barn == null)
            {
                return NotFound();
            }

            db.Barn.Remove(barn);
            await db.SaveChangesAsync();

            return Ok(barn);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BarnExists(int id)
        {
            return db.Barn.Count(e => e.Barn_Id == id) > 0;
        }
    }
}