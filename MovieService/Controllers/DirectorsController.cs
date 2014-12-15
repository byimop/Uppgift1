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
using MovieService.Models;

namespace MovieService.Controllers
{
    public class DirectorsController : ApiController
    {
        private MovieServiceContext db = new MovieServiceContext();

        // GET: api/Directors
        public IQueryable<Director> GetDirectors()
        {
            return db.Directors;
        }

        // GET: api/Directors/5
        [ResponseType(typeof(Director))]
        public async Task<IHttpActionResult> GetDirector(int id)
        {
            Director director = await db.Directors.FindAsync(id);
            if (director == null)
            {
                return NotFound();
            }

            return Ok(director);
        }

        // PUT: api/Directors/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDirector(int id, Director director)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != director.Id)
            {
                return BadRequest();
            }

            db.Entry(director).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DirectorExists(id))
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

        // POST: api/Directors
        [ResponseType(typeof(Director))]
        public async Task<IHttpActionResult> PostDirector(Director director)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Directors.Add(director);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = director.Id }, director);
        }

        // DELETE: api/Directors/5
        [ResponseType(typeof(Director))]
        public async Task<IHttpActionResult> DeleteDirector(int id)
        {
            Director director = await db.Directors.FindAsync(id);
            if (director == null)
            {
                return NotFound();
            }

            db.Directors.Remove(director);
            await db.SaveChangesAsync();

            return Ok(director);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DirectorExists(int id)
        {
            return db.Directors.Count(e => e.Id == id) > 0;
        }
    }
}