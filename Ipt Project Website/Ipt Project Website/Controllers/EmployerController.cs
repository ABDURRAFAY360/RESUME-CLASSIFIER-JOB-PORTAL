using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ipt_Project_Website.Models;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;

namespace Ipt_Project_Website.Controllers
{
    public class EmployerController : Controller
    {
        //public ActionResult logincheck()
        //{
        //    if (Session["login"].ToString() == "0")
        //    {
        //        ViewBag.Message = "Please login first";
        //        return RedirectToRoute("Employerlogin");
        //    }
        //    else if (Session["login"].ToString() == "1" && Session["Employer"].ToString() != "0")
        //    {
        //        return RedirectToRoute("Employerlogin");
        //    }

        //    return RedirectToRoute("Homepage");
        //}
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
                        Session["login"] = 1;
                        Session["Employer"] = u;
                        return View("View", u);
                    }
                }
            }
            ViewBag.Message = "unSuccessful";
            return View();

        }


        [HttpGet]
        public ActionResult CreateJob()
        {
            if (Session["login"].ToString() == "0")
            {
                ViewBag.Message = "Please login first";
                return RedirectToRoute("Employerlogin");
            }
            else if (Session["login"].ToString() == "1" && Session["User"].ToString() != "0")
            {
                ViewBag.Message = "Please login first";
                return RedirectToRoute("Employerlogin");
            }
            Job_post jobs = new Job_post();
            return View(jobs);
        }
        [HttpPost]
        public async Task<ActionResult> CreateJob(Job_post job)
        {
            string FileName = System.IO.Path.GetFileNameWithoutExtension(job.UploadFile.FileName);
            string FileExtension = System.IO.Path.GetExtension(job.UploadFile.FileName);
            FileName = DateTime.Now.ToString("yyyyMMddss") + "-" + FileName.Trim() + FileExtension;
            string UploadPath = ConfigurationManager.AppSettings["JobDescription"].ToString() + FileName;
            job.UploadFile.SaveAs(UploadPath);
            string text = System.IO.File.ReadAllText(UploadPath);
            DbModel dbmodel = new DbModel();
            Job_post post = new Job_post();
            Employer emp = Session["Employer"] as Employer;
            post.Employer_id = 1;
            post.Job_description = text;
            post.Job_designation = job.Job_designation;
            /*Response.Write(text);
            Response.End();
            */
            dbmodel.Job_post.Add(post);
            dbmodel.SaveChanges();


            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration Successful";
            return View();
        }
        
        [HttpGet]
        public ActionResult ResumeSuggestion()
        {
            string JobDescriptionPath = ConfigurationManager.AppSettings["JobDescription"].ToString();
            List<string> jd_name = new List<string>();
            foreach (string txtName in Directory.GetFiles(JobDescriptionPath, "*.txt"))
            {
                jd_name.Add(System.IO.Path.GetFileNameWithoutExtension(txtName));
            }
            ViewBag.JobDescription = jd_name;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> ResumeSuggestion(Job_post job)
        {
            string UploadPath = ConfigurationManager.AppSettings["UploadFolder"].ToString();
            string JobDescriptionPath = ConfigurationManager.AppSettings["JobDescription"].ToString();
            string job_file_name = JobDescriptionPath + job.Job_description+".txt";
            string job_description = System.IO.File.ReadAllText(job_file_name);
            List<string> read_jd = new List<string>();
            read_jd.Add(job_description);
           List<string> read_resumes = new List<string>();
            foreach (string txtName in Directory.GetFiles(UploadPath, "*.pdf"))
            {
                StringBuilder text = new StringBuilder();
                using (PdfReader reader = new PdfReader(txtName))
                {
                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                    }
                    read_resumes.Add(text.ToString());
                }
            }
        /*    Response.Write(read_resumes[0][0]);
            Response.Write(read_resumes[1][0]);
            Response.Write(read_resumes[2][0]);
            Response.Write(read_resumes[3][0]);
            Response.Write(read_resumes[4][0]);
            Response.End();*/
            var user = new Dictionary<string, List<string>>
            {
                { "resumes", read_resumes},
                {"job_description",  read_jd}
            };
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            var response = await client.PostAsync("https://rafay.ap.ngrok.io/JobSuggestion", data);
            string result = await response.Content.ReadAsStringAsync();
            Response.Write(result);
            Response.End();
            return View();
        }
        public ActionResult EmployerLogout()
        {
            Session["login"] = 0;

            Session["Employer"] = 0;
            return RedirectToRoute("Homepage");
        }
    }
}
