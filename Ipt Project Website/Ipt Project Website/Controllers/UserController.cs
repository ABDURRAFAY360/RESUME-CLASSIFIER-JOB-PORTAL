﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ipt_Project_Website.Models;
using System.Web.SessionState;
using System.Configuration;
using System.IO;

namespace Ipt_Project_Website.Controllers
{
    public class UserController : Controller
    {
        public ActionResult logincheck()
        {
            if (Session["login"].ToString() == "0")
            {
                ViewBag.Message = "Please login first";
                return RedirectToRoute("Userlogin");
            }
            else if(Session["login"].ToString()=="1" && Session["Employer"].ToString() != "0")
            {
                return RedirectToRoute("Userlogin");
            }

            return RedirectToRoute("Homepage");
        }
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
                        Session["login"] = 1;
                        Session["User_ID"] = u.id;
                        Session["User"] = u;
                        return RedirectToRoute("Homepage");
                    }
                }
            }
            ViewBag.Message = "unSuccessful";
            return View();
        }
        public ActionResult JobApply()
        {
            //string JobDescriptionPath = ConfigurationManager.AppSettings["JobDescription"].ToString();
            //List<string> jd_name = new List<string>();
            //foreach (string txtName in Directory.GetFiles(JobDescriptionPath, "*.txt"))
            //{
            //    jd_name.Add(System.IO.Path.GetFileNameWithoutExtension(txtName));
            //}
            //ViewBag.JobDescription = jd_name;
            List<Job_post> job_post = new List<Job_post>();
            List<Employer> employer = new List<Employer>();
            if (Session["login"].ToString() == "0")
            {
                ViewBag.Message = "Please login first";
                return RedirectToRoute("Userlogin");
            }
            else if (Session["login"].ToString() == "1" && Session["Employer"].ToString() != "0")
            {
                return RedirectToRoute("Userlogin");
            }
            using (DbModel dbmodel = new DbModel())
            {
                ViewBag.EmployerList = dbmodel.Employers.ToList();
                ViewBag.JobList = dbmodel.Job_post.ToList();
            }

            return View();
        }

        public ActionResult job_apply(int job_id)
        {
            DbModel dbmodel = new DbModel();
            Job_applicant applicant = new Job_applicant();
            List<Job_applicant> app = new List<Job_applicant>();
            app = dbmodel.Job_applicant.ToList();
            int max = 0;
            foreach (Job_applicant job_applicant in app)
            {
                if (max < job_applicant.id)
                {
                    max = job_applicant.id;
                }
            }
            max++;
            applicant.id = max;
            var emp  = Session["User"] as User;
            applicant.job_id = job_id;
            applicant.applicant_id = emp.id;
            dbmodel.Job_applicant.Add(applicant);
            dbmodel.SaveChanges();
            return RedirectToRoute("JobApply");
        }
        public ActionResult UserLogout()
        {
            Session["login"] = "0";
            Session["User"] = 0;
            Session["User_ID"] = 0;
            Session["Employer_ID"] = 0;
            Session["Model"] = 0;
            Session["UserModel"] = 0;
            Session["EmployerModel"] = 0;
            Session["Employer"] = 0;
            return RedirectToRoute("Homepage");
        }
    }
}