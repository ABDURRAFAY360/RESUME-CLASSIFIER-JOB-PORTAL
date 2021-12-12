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
        [ValidateAntiForgeryToken]  
        public ActionResult UserSignUp(User user)
        {
            DbModel dbmodel = new DbModel();
            if (ModelState.IsValid)
            {
                var isEmailAlreadyExists = dbmodel.Users.Any(x => x.Email == user.Email);
                if (isEmailAlreadyExists)
                {
                    ModelState.AddModelError("Email", "User with this email already exists");
                    return View(user);
                }
                var PhoneNumberExists = dbmodel.Users.Any(x => x.Phone_number== user.Phone_number);
                if (PhoneNumberExists)
                {
                    ModelState.AddModelError("Phone_Number", "User with this Phone number already exists");
                    return View(user);
                }

            }
            using (dbmodel = new DbModel())
            {
                dbmodel.Users.Add(user);
                dbmodel.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successful";
            return View("View", user);
        }


        [HttpGet]
        public ActionResult UserLogin()
        { 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserLogin(FormCollection collection)
        {
            List<User> UserList = new List<User>();
            using (DbModel dbmodel = new DbModel())
            {
                UserList = dbmodel.Users.OrderBy(a => a.Email).ToList();
                foreach(User u in UserList)
                {
                    if (u.Email == collection["email"] && u.Password== collection["password"])
                    {
                        return View("View", u);
                    }
                }
            }
            ViewBag.Message = "unSuccessful";
            return View();

        }
    }
}