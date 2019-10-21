using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace mvc_project1.Models
{
    public class CommentsModel
    {
        [Key]
        public int k { get; set; }

        public int id { get; set; }

        public string Comment1 { get; set; }

        public string Nickname1 { get; set; }
        
    }
}