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
    public class yellowcardsController : Controller
    {
        private FootballDB db = new FootballDB();

        // GET: yellowcards
        public ActionResult Index()
        {
            var yellow_cards = db.yellow_cards.Include(y => y.match).Include(y => y.player).Include(y => y.team);
            return View(yellow_cards.ToList());
        }

        // GET: yellowcards/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            yellow_cards yellow_cards = db.yellow_cards.Find(id);
            if (yellow_cards == null)
            {
                return HttpNotFound();
            }
            return View(yellow_cards);
        }

        // GET: yellowcards/Create
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

        // POST: yellowcards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "yellow_card_id, match_id, team_id")] yellow_cards yellow_cards, int player_id_1, int player_id_2)
        {
            if (ModelState.IsValid)
            {
                if(player_id_1 != 0 && player_id_2 == 0)
                {
                    yellow_cards.player_id = player_id_1;
                } else if (player_id_1 == 0 && player_id_2 != 0)
                {
                    yellow_cards.player_id = player_id_2;
                }
                db.yellow_cards.Add(yellow_cards);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.match_id = yellow_cards.match_id;
            var match = db.matches.Find(yellow_cards.match_id);
            var team1 = match.TeamMatches.First(m => m.match_id == match.match_id);
            var team2 = match.TeamMatches.Last(m => m.match_id == match.match_id);
            List<TeamMatch> teams = new List<TeamMatch>();
            teams.Add(team1);
            teams.Add(team2);
            ViewBag.players1_id = db.players.Where(p => p.team_id == team1.team_id);
            ViewBag.players2_id = db.players.Where(p => p.team_id == team2.team_id);
            ViewBag.teams = teams;
            return View(yellow_cards);
        }

        // GET: yellowcards/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            yellow_cards yellow_cards = db.yellow_cards.Find(id);
            if (yellow_cards == null)
            {
                return HttpNotFound();
            }
            ViewBag.match_id = new SelectList(db.matches, "match_id", "date", yellow_cards.match_id);
            ViewBag.player_id = new SelectList(db.players, "player_id", "fullname", yellow_cards.player_id);
            ViewBag.team_id = new SelectList(db.teams, "team_id", "name", yellow_cards.team_id);
            return View(yellow_cards);
        }

        // POST: yellowcards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "yellow_card_id,match_id,player_id,team_id")] yellow_cards yellow_cards)
        {
            if (ModelState.IsValid)
            {
                db.Entry(yellow_cards).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.match_id = new SelectList(db.matches, "match_id", "date", yellow_cards.match_id);
            ViewBag.player_id = new SelectList(db.players, "player_id", "fullname", yellow_cards.player_id);
            ViewBag.team_id = new SelectList(db.teams, "team_id", "name", yellow_cards.team_id);
            return View(yellow_cards);
        }

        // GET: yellowcards/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            yellow_cards yellow_cards = db.yellow_cards.Find(id);
            if (yellow_cards == null)
            {
                return HttpNotFound();
            }
            return View(yellow_cards);
        }

        // POST: yellowcards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            yellow_cards yellow_cards = db.yellow_cards.Find(id);
            db.yellow_cards.Remove(yellow_cards);
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
