using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FootballWebApp.Models
{
    public class TeamMatchNamesViewModel
    {
        public int match_id { get; set; }
        public string team1_name { get; set; }
        public string team2_name { get; set; }
    }
}