using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ipt_Project_Website.Models;

namespace Ipt_Project_Website.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult UserSignUp()
        {
            User user = new User();
            return View(user);
        }
        [HttpPost]
        public ActionResult UserSignUp(User user)
        {
            using(DbModel dbmodel = new DbModel())
            {
                dbmodel.Users.Add(user);
                dbmodel.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successful";
            return View("UserSignUp", new User());
        }
    }
}