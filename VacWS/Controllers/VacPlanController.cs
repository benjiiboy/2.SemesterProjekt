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
    public class VacPlanController : ApiController
    {
        private DataBaseContext db = new DataBaseContext();

        // GET: api/VacPlan
        public IQueryable<VacPlan> GetVacPlan()
        {
            return db.VacPlan;
        }

        // GET: api/VacPlan/5
        [ResponseType(typeof(VacPlan))]
        public async Task<IHttpActionResult> GetVacPlan(int id)
        {
            VacPlan vacPlan = await db.VacPlan.FindAsync(id);
            if (vacPlan == null)
            {
                return NotFound();
            }

            return Ok(vacPlan);
        }

        // PUT: api/VacPlan/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutVacPlan(int id, VacPlan vacPlan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vacPlan.Plan_Id)
            {
                return BadRequest();
            }

            db.Entry(vacPlan).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VacPlanExists(id))
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

        // POST: api/VacPlan
        [ResponseType(typeof(VacPlan))]
        public async Task<IHttpActionResult> PostVacPlan(VacPlan vacPlan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VacPlan.Add(vacPlan);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = vacPlan.Plan_Id }, vacPlan);
        }

        // DELETE: api/VacPlan/5
        [ResponseType(typeof(VacPlan))]
        public async Task<IHttpActionResult> DeleteVacPlan(int id)
        {
            VacPlan vacPlan = await db.VacPlan.FindAsync(id);
            if (vacPlan == null)
            {
                return NotFound();
            }

            db.VacPlan.Remove(vacPlan);
            await db.SaveChangesAsync();

            return Ok(vacPlan);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VacPlanExists(int id)
        {
            return db.VacPlan.Count(e => e.Plan_Id == id) > 0;
        }
    }
}