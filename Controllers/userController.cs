using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc_project1.Models;
using mvc_project1.DAL;

//this controller using for adding user to database
namespace mvc_project1.Controllers
{
    [Authorize]
    public class userController : Controller
    {
        // GET: user
        public ActionResult Enter()
        {
            //open connection to db and using for displaying of global message
            gblmDAL dal = new gblmDAL();
            string g1 = dal.gblm.OrderByDescending(x => x.id).FirstOrDefault().Message;
            ViewBag.global_message = g1;

            return View();
        }

        public ActionResult redirect()
        {//using for redirection to another page if neccesery 
            return RedirectToAction("showHome", "Home", new { area = "" });
        }

        public ActionResult Submit(User userr)
        {//adding user to db
            if(ModelState.IsValid)
            {
                usersDAL dal = new usersDAL();
                dal.Users.Add(userr);
                dal.SaveChanges();
                return RedirectToAction("Loginuser", "Login", new { area = "" });

            }
            else
            {
                return View("Enter", userr);
            }

        }
    }
}