using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc_project1.Models;
using mvc_project1.DAL;
using System.Data.Entity;

//controller for admin page

namespace mvc_project1.Controllers
{
    [Authorize]
    public class AdminPanelController : Controller
    {
        // GET: AdminPanel
        public ActionResult Index()
        {
            return View();
        }

        //display page
        public ActionResult paneladmin()
        {//check cookies
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

            //db connection (global message)
            gblmDAL dal = new gblmDAL();
            string g1 = dal.gblm.OrderByDescending(x => x.id).FirstOrDefault().Message;
            ViewBag.global_message = g1;

            //db connection (users)
            usersDAL dal2 = new usersDAL();
            List<User> us = dal2.Users.ToList<User>();
            usersdb us1 = new usersdb();

            us1.user = new User();
            us1.users = us;
            return View(us1);
            
        }

        //redirection when neccesery
        public ActionResult redirect()
        {
            return RedirectToAction("showHome", "Home", new { area = "" });
        }

        //using for posting a new global message that will appear all over the site
        public ActionResult globalMessage(gblm m1)
        {
            if (ModelState.IsValid)
            {
                gblmDAL dal = new gblmDAL();
                dal.gblm.Add(m1);
                dal.SaveChanges();

                TempData["notice"] = "פורסם בהצלחה";
                
                return RedirectToAction("paneladmin");
            }
            else
            {
                return View();
            }
            
        }

        //using for adding user option of the admin panel
        public ActionResult Submit(User userr)
        {
            if (ModelState.IsValid)
            {
                usersDAL dal = new usersDAL();
                dal.Users.Add(userr);
                dal.SaveChanges();
                //passing message
                TempData["notice1"] = "הודעה: המשתמש החדש נוצר בהצלחה ונכנס למסד הנתונים";
                return RedirectToAction("paneladmin");
            }
            else
            {
                return View("Enter", userr);
            }

        }

        //using for remove users from db 
        public ActionResult Removee(int id)
        {
            if (ModelState.IsValid)
            {
                usersDAL dal = new usersDAL();
                var todelete = dal.Users.Where(x => x.id == id).FirstOrDefault();
                if (todelete != null)
                {
                    dal.Users.Remove(todelete);
                    dal.SaveChanges();
                }
                TempData["notice2"] = "הודעה: המשתמש נמחק בהצלחה והוסר ממסד הנתונים";
                return RedirectToAction("paneladmin");
            }
            else
            {
                return View("Enter");
            }

        }

        //action for Asynchronous display
        public ActionResult asyncGetManagersJason()
        {
            usersDAL dal2 = new usersDAL();
            List<User> users1 = dal2.Users.ToList<User>();
            
            return Json(users1, JsonRequestBehavior.AllowGet);
        }


    }
}