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
    public class teamsController : Controller
    {
        private FootballDB db = new FootballDB();

        // GET: teams
        public ActionResult Index()
        {
            var teams = db.teams.Include(t => t.league);
            return View(teams.ToList());
        }

        // GET: teams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            team team = db.teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // GET: teams/Create
        public ActionResult Create(int id)
        {
            var selectedLeague = db.leagues.Single(l => l.league_id == id);
            ViewBag.league_name = selectedLeague.name;
            ViewBag.league_id = id;
            return View();
        }

        // POST: teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "team_id,name,coach,goals_for,goals_against,points,wins,draws,loss")] team team,int id)
        {
            if (ModelState.IsValid)
            {
                team.league_id = id;
                db.teams.Add(team);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.league_id = new SelectList(db.leagues, "league_id", "name", team.league_id);
            return View(team);
        }

        // GET: teams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            team team = db.teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            ViewBag.league_id = new SelectList(db.leagues, "league_id", "name", team.league_id);
            return View(team);
        }

        // POST: teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "team_id,name,coach,goals_for,goals_against,points,wins,draws,loss,league_id")] team team)
        {
            if (ModelState.IsValid)
            {
                db.Entry(team).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.league_id = new SelectList(db.leagues, "league_id", "name", team.league_id);
            return View(team);
        }

        // GET: teams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            team team = db.teams.Find(id);
            if (team == null)
            {
                return HttpNotFound();
            }
            return View(team);
        }

        // POST: teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            team team = db.teams.Find(id);
            db.teams.Remove(team);
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
