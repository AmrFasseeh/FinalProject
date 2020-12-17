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
    public class leaguesApiController : ApiController
    {
        private FootballDB db = new FootballDB();

        // GET: api/leaguesApi
        public IQueryable<league> Getleagues()
        {
            return db.leagues;
        }

        // GET: api/leaguesApi/5
        [ResponseType(typeof(league))]
        public IHttpActionResult Getleague(int id)
        {
            league league = db.leagues.Find(id);
            if (league == null)
            {
                return NotFound();
            }

            return Ok(league);
        }

        // PUT: api/leaguesApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putleague(int id, league league)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != league.league_id)
            {
                return BadRequest();
            }

            db.Entry(league).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!leagueExists(id))
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

        // POST: api/leaguesApi
        [ResponseType(typeof(league))]
        public IHttpActionResult Postleague(league league)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.leagues.Add(league);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = league.league_id }, league);
        }

        // DELETE: api/leaguesApi/5
        [ResponseType(typeof(league))]
        public IHttpActionResult Deleteleague(int id)
        {
            league league = db.leagues.Find(id);
            if (league == null)
            {
                return NotFound();
            }

            db.leagues.Remove(league);
            db.SaveChanges();

            return Ok(league);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool leagueExists(int id)
        {
            return db.leagues.Count(e => e.league_id == id) > 0;
        }
    }
}