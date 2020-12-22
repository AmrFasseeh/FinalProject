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
    public class playersController : Controller
    {
        private FootballDB db = new FootballDB();

        // GET: players
        /*public ActionResult Index(int? id)
        {
            var players = db.players.Where(p => p.team_id==id);
            var teams = db.teams;
            ViewBag.currentTeam = id;
            ViewBag.teams = teams;
            return View(players.ToList());
        }*/

        // GET: players/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            player player = db.players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // GET: players/Create
        public ActionResult Create(int id)
        {
            var selectedTeam = db.teams.Single(t => t.team_id == id);
            ViewBag.team_name = selectedTeam.name;
            ViewBag.team_id=id;
            return View();
        }

        // POST: players/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "player_id,fullname,age,number")] player player,int team_id)
        {
            if (ModelState.IsValid)
            {
                player.team_id = team_id;
                db.players.Add(player);
                db.SaveChanges();
                return RedirectToAction("getSingleTeam", "teams", new { id = player.team_id });
            }

            var selectedTeam = db.teams.Single(t => t.team_id == player.team_id);
            ViewBag.team_name = selectedTeam.name;
            ViewBag.team_id = player.team_id;
            return View(player);
        }

        // GET: players/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            player player = db.players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            ViewBag.team_id = new SelectList(db.teams, "team_id", "name", player.team_id);
            return View(player);
        }

        // POST: players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "player_id,fullname,age,number,team_id")] player player)
        {
            if (ModelState.IsValid)
            {
                db.Entry(player).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.team_id = new SelectList(db.teams, "team_id", "name", player.team_id);
            return View(player);
        }

        // GET: players/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            player player = db.players.Find(id);
            if (player == null)
            {
                return HttpNotFound();
            }
            return View(player);
        }

        // POST: players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            player player = db.players.Find(id);
            db.players.Remove(player);
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
