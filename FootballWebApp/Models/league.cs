namespace FootballWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class league
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public league()
        {
            teams = new HashSet<team>();
        }

        [Key]
        public int league_id { get; set; }

        [StringLength(255)]
        [Display(Name ="League")]
        public string name { get; set; }

        [StringLength(255)]
        [Display(Name ="Country")]
        public string countries { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<team> teams { get; set; }
    }
}
