using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FootballWebApp.Models
{
    public class TeamMatch
    {
        [Key,Column(Order=1)]
        public int match_id { get; set; }

        [Key, Column(Order = 2)]
        public int team_id { get; set; }
        public virtual match match { get; set; }
        public virtual team team { get; set; }
    }
}