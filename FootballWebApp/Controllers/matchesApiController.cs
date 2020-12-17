using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FootballWebApp.Models;

namespace FootballWebApp.Controllers
{
    public class matchesApiController : ApiController
    {
        private FootballDB db = new FootballDB();

        // GET: api/matchesApi
        public IQueryable<match> Getmatches()
        {
            return db.matches;
        }

        // GET: api/matchesApi/5
        [ResponseType(typeof(match))]
        public IHttpActionResult Getmatch(int id)
        {
            match match = db.matches.Find(id);
            if (match == null)
            {
                return NotFound();
            }

            return Ok(match);
        }

        // PUT: api/matchesApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putmatch(int id, match match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != match.match_id)
            {
                return BadRequest();
            }

            db.Entry(match).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!matchExists(id))
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

        // POST: api/matchesApi
        [ResponseType(typeof(match))]
        public IHttpActionResult Postmatch(match match)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.matches.Add(match);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = match.match_id }, match);
        }

        // DELETE: api/matchesApi/5
        [ResponseType(typeof(match))]
        public IHttpActionResult Deletematch(int id)
        {
            match match = db.matches.Find(id);
            if (match == null)
            {
                return NotFound();
            }

            db.matches.Remove(match);
            db.SaveChanges();

            return Ok(match);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool matchExists(int id)
        {
            return db.matches.Count(e => e.match_id == id) > 0;
        }
    }
}