using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FootballWebApp.Models;

namespace FootballWebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        

        FootballDB db = new FootballDB();
        
        public ActionResult Index()
        {
            ViewBag.matches = db.matches.ToList().Count;
            ViewBag.leagues = db.leagues.ToList().Count;
            ViewBag.teams = db.teams.ToList().Count;
            ViewBag.players = db.players.ToList().Count;
            ViewBag.goals = db.goals.ToList().Count;
            ViewBag.yellowcards = db.yellow_cards.ToList().Count;
            ViewBag.redcards = db.red_cards.ToList().Count;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}