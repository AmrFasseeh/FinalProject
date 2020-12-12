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
            List<TeamMatchNamesViewModel> teams = new List<TeamMatchNamesViewModel>();

            TeamMatchNamesViewModel team1 = new TeamMatchNamesViewModel();
            team1.match_id = db.TeamMatches.FirstOrDefault(x => x.match_id == 1).match_id;
            team1.team1_name = db.TeamMatches.FirstOrDefault(x => x.match_id == 1).team.name;
            teams.Add(team1);

            for (int i = 1, index1 = 0; i < db.matches.Count(); i++)
            {
                int found = 1;
                
                if (teams.Count > 0)
                {
                    for (int j = 0; j < teams.Count; j++)
                    {
                        var s = teams[teams.Count-1];
                        var y = db.TeamMatches.AsEnumerable().Where(x => x.match_id == i).ToList();
                        if (s.match_id == y[0].match_id)
                        {
                            teams[teams.Count-1].team2_name = y[1].team.name;
                            found = 0;
                        }
                    }
                }
                if (found == 1)
                {
                    TeamMatchNamesViewModel team = new TeamMatchNamesViewModel();
                    team.match_id = db.TeamMatches.FirstOrDefault(x => x.match_id == i).match_id;
                    team.team1_name = db.TeamMatches.FirstOrDefault(x => x.match_id == i).team.name;
                    teams.Add(team);
                }
                
            }
            ViewBag.Teams = teams;
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
        public ActionResult Create([Bind(Include = "match_id,team1_score,team2_score,date,status")] match match, int team1, int team2)
        {
            TeamMatch teamMatch1 = new TeamMatch();
            TeamMatch teamMatch2 = new TeamMatch();
            if (ModelState.IsValid)
            {
                teamMatch1.match_id = match.match_id;
                teamMatch1.team_id = team1;
                db.TeamMatches.Add(teamMatch1);
                teamMatch2.match_id = match.match_id;
                teamMatch2.team_id = team2;
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
            return View(match);
        }

        // POST: matches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "match_id,team1_score,team2_score,date,status")] match match)
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
