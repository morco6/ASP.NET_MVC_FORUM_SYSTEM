using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc_project1.Models;
using mvc_project1.DAL;

//this is the homepage controller

namespace mvc_project1.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        //action for displaing page
        public ActionResult showHome()
        {
            //check for cookies
            string cookie1 = "There is no cookie!";
            string cookie2 = "There is no cookie!";
            string cookie3 = "There is no cookie!";
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("Cookie1"))
            {
                cookie1 = this.ControllerContext.HttpContext.Request.Cookies["Cookie1"].Value;
            }
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("Cookie2"))
            {
                cookie2 = this.ControllerContext.HttpContext.Request.Cookies["Cookie2"].Value;
            }
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("Cookie3"))
            {
                cookie3 = this.ControllerContext.HttpContext.Request.Cookies["Cookie3"].Value;
            }
            ViewBag.MyCookie1 = cookie1;
            ViewBag.MyCookie2 = cookie2;
            ViewBag.MyCookie3 = cookie3;
            
            //set a connection to db (posts of forum) 
            subsDAL dal = new subsDAL();
            commDAL dal2 = new commDAL();

            List<subject> sub = dal.subject.ToList<subject>();
            List<CommentsModel> com2 = dal2.comm.ToList<CommentsModel>();

            subjects sub1 = new subjects();
            sub1.subject1 = new subject();
            sub1.list_subs = sub;
            sub1.com = new CommentsModel();
            sub1.list_coms = com2;

            //set connection to db (global message)
            gblmDAL dal1 = new gblmDAL();
            string g1 = dal1.gblm.OrderByDescending(x => x.id).FirstOrDefault().Message;
            ViewBag.global_message = g1;

            return View("myHome", sub1);
        }

        //action for delete cookies from the user computer
        public ActionResult RemoveCookie()
        {
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("Cookie1"))
            {
                HttpCookie cookie1 = this.ControllerContext.HttpContext.Request.Cookies["Cookie1"];
                cookie1.Expires = DateTime.Now.AddDays(-1);
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie1);
            }
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("Cookie2"))
            {
                HttpCookie cookie2 = this.ControllerContext.HttpContext.Request.Cookies["Cookie2"];
                cookie2.Expires = DateTime.Now.AddDays(-1);
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie2);
            }
            if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("Cookie3"))
            {
                HttpCookie cookie3 = this.ControllerContext.HttpContext.Request.Cookies["Cookie3"];
                cookie3.Expires = DateTime.Now.AddDays(-1);
                this.ControllerContext.HttpContext.Response.Cookies.Add(cookie3);
            }
            return RedirectToAction("showHome");
        }

        //using for delete posts from homepage (only for admin and managers
        public ActionResult Removeee(int id)
        {
            if (ModelState.IsValid)
            {
                subsDAL dal = new subsDAL();
                var todelete = dal.subject.Where(x => x.id == id).FirstOrDefault();
                if (todelete != null)
                {
                    dal.subject.Remove(todelete);
                    dal.SaveChanges();
                }
                //connection to db of comments -> used for delete comments from post
                commDAL dal1 = new commDAL();
                
                var todelete1 = dal1.comm.Where(c => c.id == id);
                IEnumerable<CommentsModel> list = dal1.comm.Where(x => x.id == id).ToList();
                dal1.comm.RemoveRange(list);
                dal1.SaveChanges();

                //i used tempdata to pass states messages to view
                TempData["notice2"] = "הודעה: הפוסט נמחק בהצלחה והוסר ממסד הנתונים";
                return RedirectToAction("showHome");
            }
            else
            {
                return View("Enter");
            }

        }

        
        public ActionResult CommentToPost(CommentsModel C1)
        {
            C1.Nickname1 = Request.Cookies["Cookie1"]?.Value;
            
            if (ModelState.IsValid)
            {
                commDAL dal = new commDAL();
                dal.comm.Add(C1);
                dal.SaveChanges();
                return RedirectToAction("showHome");
            }
            else
            {
                return View("Enter");
            }

        }


        
        //using for delete comments from homepage (only for admin and managers)
        public ActionResult Removecomment(int id)
        {
            if (ModelState.IsValid)
            {
                commDAL dal = new commDAL();
                var todelete = dal.comm.Where(x => x.k == id).FirstOrDefault();
                if (todelete != null)
                {
                    dal.comm.Remove(todelete);
                    dal.SaveChanges();
                }

                //i used tempdata to pass states messages to view
                TempData["notice2"] = "הודעה: התגובה נמחקה בהצלחה והוסרה ממסד הנתונים";
                return RedirectToAction("showHome");
            }
            else
            {
                return View("Enter");
            }

        }
    }
}