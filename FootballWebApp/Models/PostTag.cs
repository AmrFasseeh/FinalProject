using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FootballWebApp.Models
{
    public class PostTag
    {
        [Key, Column(Order = 1)]
        public int postId { get; set; }
        [Key, Column(Order = 2)]
        public int tagId { get; set; }
        public virtual post Post { get; set; }
        public virtual tag Tag { get; set; }
    }
}