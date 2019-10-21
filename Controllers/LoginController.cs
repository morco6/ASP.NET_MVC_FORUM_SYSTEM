using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvc_project1.Models;
using mvc_project1.DAL;

//controller for login to the site
namespace mvc_project1.Controllers
{
    [Authorize]
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Loginuser()
        {
            //displaying global message
            gblmDAL dal = new gblmDAL();
            string g1 = dal.gblm.OrderByDescending(x => x.id).FirstOrDefault().Message;
            ViewBag.global_message = g1;//pass global message

            return View();
        }

        //action for creating a new cookie in the user computer --> when finished, redirect to homepage
        public ActionResult CreateCookie(User userr)
        {
            string searchNickName = userr.NickName;
            string searchPassword = userr.Password;
            string searchPermission = userr.Permission.ToString();

            HttpCookie cookie1 = new HttpCookie("Cookie1");
            cookie1.Value = searchNickName;
            HttpCookie cookie2 = new HttpCookie("Cookie2");
            cookie2.Value = searchPassword;
            HttpCookie cookie3 = new HttpCookie("Cookie3");
            cookie3.Value = searchPermission;
            this.ControllerContext.HttpContext.Response.Cookies.Add(cookie1);
            this.ControllerContext.HttpContext.Response.Cookies.Add(cookie2);
            this.ControllerContext.HttpContext.Response.Cookies.Add(cookie3);
            //return View("myHome");
            return RedirectToAction("showHome", "Home", new { area = "" });
            
        }

        
        //that action using for searching the user that include the details of the user input
        public ActionResult Search(User userr)
        {
            if (ModelState.IsValid)
            {
                usersDAL dal = new usersDAL();
                string searchNickName = Request.Form["NickName"].ToString();
                string searchPassword = Request.Form["Password"].ToString();
                List<User> username = (from x in dal.Users where x.NickName.Contains(searchNickName) select x).ToList<User>();
                List<User> password = (from x in dal.Users where x.Password.Contains(searchPassword) select x).ToList<User>();
                
                if((username.Capacity != 0) && (password.Capacity != 0) && (searchNickName.Equals(username[0].NickName)) && (searchPassword.Equals(password[0].Password)))
                {
                    userr.Permission = username[0].Permission;

                    return RedirectToAction("CreateCookie", userr);
                    
                }
                else
                {
                    ViewBag.Message = string.Format("משתמש לא קיים במערכת, נסה שוב");
                    return View("Loginuser");
                }
            }
            else
            {
                return View("Loginuser");
            }

        }
    }
}