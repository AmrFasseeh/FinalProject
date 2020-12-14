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
    public class matchesController : Controller
    {
        private FootballDB db = new FootballDB();

        // GET: matches
        public ActionResult Index()
        {
            return View(db.matches.ToList());
        }

        // GET: matches/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            match match = db.matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        // GET: matches/Create
        public ActionResult Create()
        {
            ViewBag.team1 = new SelectList(db.teams.ToList(), "team_id", "name");
            ViewBag.team2 = new SelectList(db.teams.ToList(), "team_id", "name");
            return View();
        }

        // POST: matches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "match_id,date,status")] match match, int team1, int team2)
        {
            TeamMatch teamMatch1 = new TeamMatch();
            TeamMatch teamMatch2 = new TeamMatch();
            if (ModelState.IsValid)
            {
                teamMatch1.match_id = match.match_id;
                teamMatch1.team_id = team1;
                teamMatch1.home_Away = "home";
                db.TeamMatches.Add(teamMatch1);
                teamMatch2.match_id = match.match_id;
                teamMatch2.team_id = team2;
                teamMatch2.home_Away = "away";
                db.TeamMatches.Add(teamMatch2);
                db.matches.Add(match);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(match);
        }

        // GET: matches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            match match = db.matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            ViewBag.team1 = new SelectList(db.teams.ToList(), "team_id", "name");
            ViewBag.team2 = new SelectList(db.teams.ToList(), "team_id", "name");
            return View(match);
        }

        // POST: matches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "match_id,date,status")] match match)
        {
            if (ModelState.IsValid)
            {
                db.Entry(match).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(match);
        }

        // GET: matches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            match match = db.matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        // POST: matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            match match = db.matches.Find(id);
            db.matches.Remove(match);
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
