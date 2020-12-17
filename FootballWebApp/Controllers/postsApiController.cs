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
    public class postsApiController : ApiController
    {
        private FootballDB db = new FootballDB();

        // GET: api/postsApi
        public IQueryable<post> Getposts()
        {
            return db.posts;
        }

        // GET: api/postsApi/5
        [ResponseType(typeof(post))]
        public IHttpActionResult Getpost(int id)
        {
            post post = db.posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        // PUT: api/postsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putpost(int id, post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != post.id)
            {
                return BadRequest();
            }

            db.Entry(post).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!postExists(id))
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

        // POST: api/postsApi
        [ResponseType(typeof(post))]
        public IHttpActionResult Postpost(post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.posts.Add(post);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = post.id }, post);
        }

        // DELETE: api/postsApi/5
        [ResponseType(typeof(post))]
        public IHttpActionResult Deletepost(int id)
        {
            post post = db.posts.Find(id);
            if (post == null)
            {
                return NotFound();
            }

            db.posts.Remove(post);
            db.SaveChanges();

            return Ok(post);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool postExists(int id)
        {
            return db.posts.Count(e => e.id == id) > 0;
        }
    }
}