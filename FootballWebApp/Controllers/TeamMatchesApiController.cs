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
    public class TeamMatchesApiController : ApiController
    {
        private FootballDB db = new FootballDB();

        // GET: api/TeamMatchesApi
        public IQueryable<TeamMatch> GetTeamMatches()
        {
            return db.TeamMatches;
        }

        // GET: api/TeamMatchesApi/5
        [ResponseType(typeof(TeamMatch))]
        public IHttpActionResult GetTeamMatch(int id)
        {
            TeamMatch teamMatch = db.TeamMatches.Find(id);
            if (teamMatch == null)
            {
                return NotFound();
            }

            return Ok(teamMatch);
        }

        // PUT: api/TeamMatchesApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTeamMatch(int id, TeamMatch teamMatch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teamMatch.match_id)
            {
                return BadRequest();
            }

            db.Entry(teamMatch).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamMatchExists(id))
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

        // POST: api/TeamMatchesApi
        [ResponseType(typeof(TeamMatch))]
        public IHttpActionResult PostTeamMatch(TeamMatch teamMatch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TeamMatches.Add(teamMatch);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (TeamMatchExists(teamMatch.match_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = teamMatch.match_id }, teamMatch);
        }

        // DELETE: api/TeamMatchesApi/5
        [ResponseType(typeof(TeamMatch))]
        public IHttpActionResult DeleteTeamMatch(int id)
        {
            TeamMatch teamMatch = db.TeamMatches.Find(id);
            if (teamMatch == null)
            {
                return NotFound();
            }

            db.TeamMatches.Remove(teamMatch);
            db.SaveChanges();

            return Ok(teamMatch);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeamMatchExists(int id)
        {
            return db.TeamMatches.Count(e => e.match_id == id) > 0;
        }
    }
}