using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ipt_Project_Website.Models;

namespace Ipt_Project_Website.Controllers
{
    public class EmployerController : Controller
    {
        [HttpGet]
        public ActionResult EmployerSignUp()
        {
            Employer employer = new Employer();
            return View(employer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployerSignUp(Employer employer)
        {
            DbModel dbmodel = new DbModel();
            if (ModelState.IsValid)
            {
                var isEmailAlreadyExists = dbmodel.Employers.Any(x => x.Email == employer.Email);
                if (isEmailAlreadyExists)
                {
                    ModelState.AddModelError("Email", "User with this email already exists");
                    return View(employer);
                }
                var PhoneNumberExists = dbmodel.Employers.Any(x => x.Phone_number == employer.Phone_number);
                if (PhoneNumberExists)
                {
                    ModelState.AddModelError("Phone_number", "User with this Phone number already exists");
                    return View(employer);
                }
            }

            using (dbmodel = new DbModel())
            {
                dbmodel.Employers.Add(employer);
                dbmodel.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successful";
            return View("View", employer);

        }

        [HttpGet]
        public ActionResult EmployerLogin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployerLogin(FormCollection collection)
        {
            List<Employer> UserList = new List<Employer>();
            using (DbModel dbmodel = new DbModel())
            {
                UserList = dbmodel.Employers.OrderBy(a => a.Email).ToList();
                foreach (Employer u in UserList)
                {
                    if (u.Email == collection["email"] && u.Password == collection["password"])
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
