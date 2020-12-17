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
    public class tagsApiController : ApiController
    {
        private FootballDB db = new FootballDB();

        // GET: api/tagsApi
        public IQueryable<tag> Gettags()
        {
            return db.tags;
        }

        // GET: api/tagsApi/5
        [ResponseType(typeof(tag))]
        public IHttpActionResult Gettag(int id)
        {
            tag tag = db.tags.Find(id);
            if (tag == null)
            {
                return NotFound();
            }

            return Ok(tag);
        }

        // PUT: api/tagsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttag(int id, tag tag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tag.id)
            {
                return BadRequest();
            }

            db.Entry(tag).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tagExists(id))
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

        // POST: api/tagsApi
        [ResponseType(typeof(tag))]
        public IHttpActionResult Posttag(tag tag)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tags.Add(tag);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tag.id }, tag);
        }

        // DELETE: api/tagsApi/5
        [ResponseType(typeof(tag))]
        public IHttpActionResult Deletetag(int id)
        {
            tag tag = db.tags.Find(id);
            if (tag == null)
            {
                return NotFound();
            }

            db.tags.Remove(tag);
            db.SaveChanges();

            return Ok(tag);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tagExists(int id)
        {
            return db.tags.Count(e => e.id == id) > 0;
        }
    }
}