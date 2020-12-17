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
    public class redcardsApiController : ApiController
    {
        private FootballDB db = new FootballDB();

        // GET: api/redcardsApi
        public IQueryable<red_cards> Getred_cards()
        {
            return db.red_cards;
        }

        // GET: api/redcardsApi/5
        [ResponseType(typeof(red_cards))]
        public IHttpActionResult Getred_cards(int id)
        {
            red_cards red_cards = db.red_cards.Find(id);
            if (red_cards == null)
            {
                return NotFound();
            }

            return Ok(red_cards);
        }

        // PUT: api/redcardsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putred_cards(int id, red_cards red_cards)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != red_cards.red_card_id)
            {
                return BadRequest();
            }

            db.Entry(red_cards).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!red_cardsExists(id))
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

        // POST: api/redcardsApi
        [ResponseType(typeof(red_cards))]
        public IHttpActionResult Postred_cards(red_cards red_cards)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.red_cards.Add(red_cards);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = red_cards.red_card_id }, red_cards);
        }

        // DELETE: api/redcardsApi/5
        [ResponseType(typeof(red_cards))]
        public IHttpActionResult Deletered_cards(int id)
        {
            red_cards red_cards = db.red_cards.Find(id);
            if (red_cards == null)
            {
                return NotFound();
            }

            db.red_cards.Remove(red_cards);
            db.SaveChanges();

            return Ok(red_cards);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool red_cardsExists(int id)
        {
            return db.red_cards.Count(e => e.red_card_id == id) > 0;
        }
    }
}