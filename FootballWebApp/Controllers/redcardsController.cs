using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FootballWebApp.Models;

namespace FootballWebApp.Controllers
{
    [Authorize]
    public class redcardsController : Controller
    {
        private FootballDB db = new FootballDB();

        // GET: redcards
        /*public ActionResult Index()
        {
            var red_cards = db.red_cards.Include(r => r.match).Include(r => r.player).Include(r => r.team);
            return View(red_cards.ToList());
        }*/

        // GET: redcards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            red_cards red_cards = db.red_cards.Find(id);
            if (red_cards == null)
            {
                return HttpNotFound();
            }
            return View(red_cards);
        }

        // GET: redcards/Create
        public ActionResult Create(int id)
        {
            ViewBag.match_id = id;
            var match = db.matches.Find(id);
            var team1 = match.TeamMatches.First(m => m.match_id == match.match_id);
            var team2 = match.TeamMatches.Last(m => m.match_id == match.match_id);
            List<TeamMatch> teams = new List<TeamMatch>();
            teams.Add(team1);
            teams.Add(team2);
            ViewBag.players1_id = db.players.Where(p => p.team_id == team1.team_id);
            ViewBag.players2_id = db.players.Where(p => p.team_id == team2.team_id);
            ViewBag.teams = teams;
            return View();
        }

        // POST: redcards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "red_card_id, match_id, team_id")] red_cards red_cards, int player_id_1, int player_id_2)
        {
            if (ModelState.IsValid)
            {
                if (player_id_1 != 0 && player_id_2 == 0)
                {
                    red_cards.player_id = player_id_1;
                }
                else if (player_id_1 == 0 && player_id_2 != 0)
                {
                    red_cards.player_id = player_id_2;
                }
                db.red_cards.Add(red_cards);
                db.SaveChanges();
                return RedirectToAction("Index", "matches");
            }

            ViewBag.match_id = red_cards.match_id;
            var match = db.matches.Find(red_cards.match_id);
            var team1 = match.TeamMatches.First(m => m.match_id == match.match_id);
            var team2 = match.TeamMatches.Last(m => m.match_id == match.match_id);
            List<TeamMatch> teams = new List<TeamMatch>();
            teams.Add(team1);
            teams.Add(team2);
            ViewBag.players1_id = db.players.Where(p => p.team_id == team1.team_id);
            ViewBag.players2_id = db.players.Where(p => p.team_id == team2.team_id);
            ViewBag.teams = teams;
            return View(red_cards);
        }

        // GET: redcards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            red_cards red_cards = db.red_cards.Find(id);
            if (red_cards == null)
            {
                return HttpNotFound();
            }
            ViewBag.match_id = new SelectList(db.matches, "match_id", "date", red_cards.match_id);
            ViewBag.player_id = new SelectList(db.players, "player_id", "fullname", red_cards.player_id);
            ViewBag.team_id = new SelectList(db.teams, "team_id", "name", red_cards.team_id);
            return View(red_cards);
        }

        // POST: redcards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "red_card_id,match_id,player_id,team_id")] red_cards red_cards)
        {
            if (ModelState.IsValid)
            {
                db.Entry(red_cards).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.match_id = new SelectList(db.matches, "match_id", "date", red_cards.match_id);
            ViewBag.player_id = new SelectList(db.players, "player_id", "fullname", red_cards.player_id);
            ViewBag.team_id = new SelectList(db.teams, "team_id", "name", red_cards.team_id);
            return View(red_cards);
        }

        // GET: redcards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            red_cards red_cards = db.red_cards.Find(id);
            if (red_cards == null)
            {
                return HttpNotFound();
            }
            return View(red_cards);
        }

        // POST: redcards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            red_cards red_cards = db.red_cards.Find(id);
            db.red_cards.Remove(red_cards);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
