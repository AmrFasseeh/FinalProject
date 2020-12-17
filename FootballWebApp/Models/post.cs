namespace FootballWebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public post()
        {
            tags = new HashSet<tag>();
        }

        public int id { get; set; }

        [Required]
        [Display(Name = "Post Title")]
        public string post_title { get; set; }

        [Column(TypeName = "text")]
        [Required]
        [Display(Name = "Post Content")]
        public string post_content { get; set; }

        [StringLength(50)]
        [Display(Name = "Post Image")]
        public string post_image { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Post Type")]
        public string post_type { get; set; }
        public string post_date { get; set; }
        public string updated_at { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tag> tags { get; set; }
    }
}
