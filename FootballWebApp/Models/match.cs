namespace FootballWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class match
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public match()
        {
            goals = new HashSet<goal>();
            red_cards = new HashSet<red_cards>();
            yellow_cards = new HashSet<yellow_cards>();
            TeamMatches = new HashSet<TeamMatch>();
        }

        [Key]
        public int match_id { get; set; }
        [Display(Name ="Team 1 Goals")]
        public int team1_score { get; set; }

        [Display(Name ="Team 2 Goals")]
        public int team2_score { get; set; }

        [StringLength(50)]
        [Display(Name ="Date")]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public string date { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name ="Match Status")]
        public string status { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<goal> goals { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<red_cards> red_cards { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<yellow_cards> yellow_cards { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TeamMatch> TeamMatches { get; set; }
    }
}
