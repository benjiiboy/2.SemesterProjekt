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
using VaccAppWS;

namespace VaccAppWS.Controllers
{
    public class BørnController : ApiController
    {
        private BarnContext db = new BarnContext();

        // GET: api/Børn
        public IQueryable<Børn> GetBørn()
        {
            return db.Børn;
        }

        // GET: api/Børn/5
        [ResponseType(typeof(Børn))]
        public async Task<IHttpActionResult> GetBørn(int id)
        {
            Børn børn = await db.Børn.FindAsync(id);
            if (børn == null)
            {
                return NotFound();
            }

            return Ok(børn);
        }

        // PUT: api/Børn/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBørn(int id, Børn børn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != børn.ID)
            {
                return BadRequest();
            }

            db.Entry(børn).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BørnExists(id))
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

        // POST: api/Børn
        [ResponseType(typeof(Børn))]
        public async Task<IHttpActionResult> PostBørn(Børn børn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Børn.Add(børn);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = børn.ID }, børn);
        }

        // DELETE: api/Børn/5
        [ResponseType(typeof(Børn))]
        public async Task<IHttpActionResult> DeleteBørn(int id)
        {
            Børn børn = await db.Børn.FindAsync(id);
            if (børn == null)
            {
                return NotFound();
            }

            db.Børn.Remove(børn);
            await db.SaveChangesAsync();

            return Ok(børn);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BørnExists(int id)
        {
            return db.Børn.Count(e => e.ID == id) > 0;
        }
    }
}