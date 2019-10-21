using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

//model for user storing in data base
namespace mvc_project1.Models
{
    public class User
    {
        
        public int id { get; set; }

        [Required(ErrorMessage ="חובה להזין שם משתמש")]
        public string NickName { get; set; }

        [Required(ErrorMessage = "חובה להזין סיסמא תקינה")]
        public string Password { get; set; }

        public string Email { get; set; }
        public string PrivateName { get; set; }

        
        public int Age { get; set; }
        public int Permission { get; set; }

    }
}