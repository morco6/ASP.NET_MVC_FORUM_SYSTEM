using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//model for global message that admi can post -> will display all over the site
namespace mvc_project1.Models
{
    public class gblm
    {
        
        public int id { get; set; }

        public string Message { get; set; }
    }
}