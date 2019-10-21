using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//spaciel model!! -> for spaciel text editor of posting a message in the forum
namespace mvc_project1.Models
{
    public class PersonModel
    {
        [Display(Name = "Text")]
        [Required(ErrorMessage = "חובה להזין הודעה")]

        [AllowHtml]
        public string Text { get; set; }
    }
}