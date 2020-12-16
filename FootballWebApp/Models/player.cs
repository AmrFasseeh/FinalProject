namespace FootballWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class player
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public player()
        {
            goals = new HashSet<goal>();
            red_cards = new HashSet<red_cards>();
            yellow_cards = new HashSet<yellow_cards>();
        }

        [Key]
        public int player_id { get; set; }

        [StringLength(255)]
        [Display(Name ="Name")]
        public string fullname { get; set; }
        [Display(Name ="Age")]
        public int? age { get; set; }
        [Display(Name ="Number")]
        public int? number { get; set; }

        public int? team_id { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<goal> goals { get; set; }

        public virtual team team { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<red_cards> red_cards { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<yellow_cards> yellow_cards { get; set; }
    }
}
