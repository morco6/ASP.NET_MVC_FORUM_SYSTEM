using mvc_project1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc_project1.DAL;

//this controller is using for open post in the home page (only for members) 
namespace mvc_project1.Controllers
{
    [Authorize]
    public class openpostController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View(new PersonModel());
        }
        
        //i used "httppost" to allow html tags in posts 
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(PersonModel person)
        {
            return View(person);
        }

        //display page --> i used cookies (without sessons) in all over the website to verify the users
        public ActionResult showPage()
        {
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

            gblmDAL dal = new gblmDAL();
            string g1 = dal.gblm.OrderByDescending(x => x.id).FirstOrDefault().Message;
            ViewBag.global_message = g1;

            return View();
        }

        //redirection to another page
        public ActionResult redirect()
        {
            return RedirectToAction("showHome", "Home", new { area = "" });
        }

        //i used "httppost" to allow html tags in posts
        //that action is for adding a new post to database
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult newpost(subject s1)
        {
            s1.Nickname = Request.Cookies["Cookie1"]?.Value;
            s1.Time = DateTime.Now;
            if (ModelState.IsValid)
            {
                subsDAL dal = new subsDAL();
                dal.subject.Add(s1);
                dal.SaveChanges();
                return RedirectToAction("showHome", "Home", new { area = "" });
            }
            else
            {
                return View();
            }
            
        }
    }
}