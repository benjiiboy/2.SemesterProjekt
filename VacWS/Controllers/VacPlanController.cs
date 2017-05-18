﻿using System;
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
    public class VacplanController : ApiController
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: api/Vacplan
        public IQueryable<Vacplan> GetVacplan()
        {
            return db.Vacplan;
        }

        // GET: api/Vacplan/5
        [ResponseType(typeof(Vacplan))]
        public async Task<IHttpActionResult> GetVacplan(int id)
        {
            Vacplan vacplan = await db.Vacplan.FindAsync(id);
            if (vacplan == null)
            {
                return NotFound();
            }

            return Ok(vacplan);
        }

        // PUT: api/Vacplan/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutVacplan(int id, Vacplan vacplan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vacplan.Plan_Id)
            {
                return BadRequest();
            }

            db.Entry(vacplan).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VacplanExists(id))
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

        // POST: api/Vacplan
        [ResponseType(typeof(Vacplan))]
        public async Task<IHttpActionResult> PostVacplan(Vacplan vacplan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vacplan.Add(vacplan);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (VacplanExists(vacplan.Plan_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = vacplan.Plan_Id }, vacplan);
        }

        // DELETE: api/Vacplan/5
        [ResponseType(typeof(Vacplan))]
        public async Task<IHttpActionResult> DeleteVacplan(int id)
        {
            Vacplan vacplan = await db.Vacplan.FindAsync(id);
            if (vacplan == null)
            {
                return NotFound();
            }

            db.Vacplan.Remove(vacplan);
            await db.SaveChangesAsync();

            return Ok(vacplan);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VacplanExists(int id)
        {
            return db.Vacplan.Count(e => e.Plan_Id == id) > 0;
        }
    }
}