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
    public class goalsController : Controller
    {
        private FootballDB db = new FootballDB();

        // GET: goals
        public ActionResult Index()
        {
            var goals = db.goals.Include(g => g.match).Include(g => g.player);
            return View(goals.ToList());
        }

        // GET: goals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            goal goal = db.goals.Find(id);
            if (goal == null)
            {
                return HttpNotFound();
            }
            return View(goal);
        }

        // GET: goals/Create
        public ActionResult Create(int id)
        {

            var selectedMatch = db.TeamMatches.Where(a => a.match_id == id);
            var team1 = selectedMatch.First(x => x.home_Away == "home").team;
            var team2 = selectedMatch.First(x => x.home_Away == "away").team;
            var players1 = team1.players.Where(a => a.team_id == team1.team_id);
            var players2 = team2.players.Where(a => a.team_id == team2.team_id);
            ViewBag.players1 = players1;
            ViewBag.players2 = players2;
            ViewBag.team1 = selectedMatch.First(x => x.home_Away == "home").team;
            ViewBag.team2 = selectedMatch.First(x => x.home_Away == "away").team;
            ViewBag.match_id = id;
            return View();
        }

        // POST: goals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "goal_id,match_id")] goal goal, int player_id1, int player_id2)
        {
            if (ModelState.IsValid)
            {
                if(player_id1 != 0)
                {
                    goal.player_id = player_id1;
                }
                else if(player_id2 != 0)
                {
                    goal.player_id = player_id2;
                }
                db.goals.Add(goal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var selectedMatch = db.TeamMatches.Where(a => a.match_id == goal.match_id);
            var team1 = selectedMatch.First(x => x.home_Away == "home").team;
            var team2 = selectedMatch.First(x => x.home_Away == "away").team;
            var players1 = team1.players.Where(a => a.team_id == team1.team_id);
            var players2 = team2.players.Where(a => a.team_id == team2.team_id);
            ViewBag.players1 = players1;
            ViewBag.players2 = players2;
            ViewBag.team1 = selectedMatch.First(x => x.home_Away == "home").team;
            ViewBag.team2 = selectedMatch.First(x => x.home_Away == "away").team;

            return View(goal);
        }

        // GET: goals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            goal goal = db.goals.Find(id);
            if (goal == null)
            {
                return HttpNotFound();
            }
            ViewBag.match_id = new SelectList(db.matches, "match_id", "date", goal.match_id);
            ViewBag.player_id = new SelectList(db.players, "player_id", "fullname", goal.player_id);
            return View(goal);
        }

        // POST: goals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "goal_id,match_id,player_id")] goal goal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(goal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.match_id = new SelectList(db.matches, "match_id", "date", goal.match_id);
            ViewBag.player_id = new SelectList(db.players, "player_id", "fullname", goal.player_id);
            return View(goal);
        }

        // GET: goals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            goal goal = db.goals.Find(id);
            if (goal == null)
            {
                return HttpNotFound();
            }
            return View(goal);
        }

        // POST: goals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            goal goal = db.goals.Find(id);
            db.goals.Remove(goal);
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
