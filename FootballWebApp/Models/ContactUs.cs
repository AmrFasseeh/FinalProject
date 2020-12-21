using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FootballWebApp.Models
{
    public class ContactUs
    {
        [Key]
        public int Id { get; set; }
        public string SenderName { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }

        [Column(TypeName = "text")]
        public string Message { get; set; }
    }
}