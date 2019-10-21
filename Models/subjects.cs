using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_project1.Models
{
    public class subjects
    {
        public subject subject1 { get; set; }

        public List<subject> list_subs { get; set; }

        public CommentsModel com { get; set; }

        public List<CommentsModel> list_coms { get; set; }
    }
}