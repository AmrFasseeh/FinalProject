namespace FootballWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tag
    {
        [Key]
        public int id { get; set; }
        [Display(Name ="Tag Title")]
        [Required]
        public string tag_title { get; set; }

        public int post_id { get; set; }

        public virtual post post { get; set; }
    }
}
