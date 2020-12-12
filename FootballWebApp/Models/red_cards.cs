namespace FootballWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class red_cards
    {
        [Key]
        public int red_card_id { get; set; }

        public int? match_id { get; set; }

        public int? player_id { get; set; }

        public int? team_id { get; set; }

        public virtual match match { get; set; }

        public virtual player player { get; set; }

        public virtual team team { get; set; }
    }
}
