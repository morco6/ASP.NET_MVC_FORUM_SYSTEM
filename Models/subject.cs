using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

//subject model for storing in data base
namespace mvc_project1.Models
{
    public class subject
    {
        public int id { get; set; }

        [Required(ErrorMessage = "חובה להזין כותרת")]
        public string Subject { get; set; }
        
        public string Nickname { get; set; }

        public DateTime? Time { get; set; }

        public string Text { get; set; }
    }
}