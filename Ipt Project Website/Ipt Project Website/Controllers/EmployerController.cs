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
        public ActionResult logincheck()
        {
            if (Session["login"].ToString() == "0")
            {
                ViewBag.Message = "Please login first";
                return RedirectToRoute("Employerlogin");
            }
            else if (Session["login"].ToString() == "1" && Session["User"].ToString() != "0")
            {
                return RedirectToRoute("Employerlogin");
            }

            return RedirectToRoute("Homepage");
        }
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
            if (Session["login"].ToString() == "1" && Session["User"].ToString() != "0")
            {
                ViewBag.Message = "Please login first";
                return RedirectToRoute("Homepage");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployerLogin(FormCollection collection)
        {
            Session["login"] = "0";
            Session["User"] = 0;
            Session["User_ID"] = 0;
            Session["Employer_ID"] = 0;
            Session["Model"] = 0;
            Session["UserModel"] = 0;
            Session["EmployerModel"] = 0;
            Session["Employer"] = 0;
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
            post.Employer_id = emp.id;
            post.Job_description = text;
            post.Job_designation = job.Job_designation;
            /*Response.Write(text);
            Response.End();
            */
            dbmodel.Job_post.Add(post);
            dbmodel.SaveChanges();
            ViewBag.SuccessMessage = "Registration Successful";
            return View();
        }
        
        [HttpGet]
        public ActionResult ResumeSuggestion()
        {
            if (Session["login"].ToString() == "0")
            {
                ViewBag.Message = "Please login first";
                return RedirectToRoute("Employerlogin");
            }
            else if (Session["login"].ToString() == "1" && Session["User"].ToString() != "0")
            {
                return RedirectToRoute("Employerlogin");
            }
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
            List<string> file_paths = new List<string>();
        
                foreach (string txtName in Directory.GetFiles(UploadPath, "*.pdf"))
                {
                    file_paths.Add(txtName);
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
           
            var user = new Dictionary<string, List<string>>
            {
                { "resumes", read_resumes},
                {"job_description",  read_jd}
            };
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            var response = await client.PostAsync("https://rafay.ap.ngrok.io/JobSuggestion", data);
            string x = await response.Content.ReadAsStringAsync();
            Dictionary<string, double> suggestion_dic = new Dictionary<string, double>();
            try
            { 
            Dictionary<string, string> htmlAttributes =
            JsonConvert.DeserializeObject<Dictionary<string, string>>(x);
            int index = 0;
            foreach (KeyValuePair<string, string> kvp in htmlAttributes)
            {
                //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                suggestion_dic.Add(file_paths[index].ToString(), Convert.ToDouble(kvp.Value));
                index++;
            }
            var sortedDict = suggestion_dic.OrderByDescending(v => v.Value).ToDictionary(v => v.Key, v => v.Value);
            
           ViewBag.Resumes = sortedDict;

            return View("SuggestedResume");
            }
            catch (Exception ex)
            {
                ViewBag.Resumes = 0;
                ViewBag.Exception = "No resumes are uploaded";
                return View("ErrorSuggestedResume");
            }
        }

        public ActionResult job_applicant()
        {
            if (Session["login"].ToString() == "0")
            {
                ViewBag.Message = "Please login first";
                return RedirectToRoute("Employerlogin");
            }
            else if (Session["login"].ToString() == "1" && Session["User"].ToString() != "0")
            {
                return RedirectToRoute("Employerlogin");
            }
            DbModel dbmodel = new DbModel();
            List<Job_post> job_post = new List<Job_post>();
            var new_post = dbmodel.Job_post.ToList();
            var emp = Session["Employer"] as Employer;
            foreach (var job in new_post)
            {
                if(job.Employer_id == emp.id)
                {
                    job_post.Add(job);
                }
            }
            ViewBag.Jobs = job_post;
            return View();
        }

        public ActionResult applicant_view(int job_id)
        {
            if (Session["login"].ToString() == "0")
            {
                ViewBag.Message = "Please login first";
                return RedirectToRoute("Employerlogin");
            }
            else if (Session["login"].ToString() == "1" && Session["User"].ToString() != "0")
            {
                return RedirectToRoute("Employerlogin");
            }
            DbModel dbmodel = new DbModel();
            List<Job_applicant> job_post = new List<Job_applicant>();
            List<Resume> new_resume = new List<Resume>();
            var resume = dbmodel.Resumes.ToList();
            var app = dbmodel.Job_applicant.ToList();
            foreach (var job in app)
            {
                if(job.job_id == job_id)
                {
                    job_post.Add(job);
                }
            }
            foreach (var job in job_post)
            {
                foreach (var new_r in resume)
                {
                    if (job.applicant_id == new_r.user_id)
                    {
                        new_resume.Add(new_r);
                    }
                }
             
            }
            ViewBag.Resumes = new_resume;
            return View();
        }
        [HttpGet]
        public ActionResult ViewResumes()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ViewResumes(Job_post job)
        {
            /*List<Employer> UserList = new List<Employer>();*/
            try
            { 
                List<Resume> UserList = new List<Resume>();
                using (DbModel dbmodel = new DbModel())
                {
                    var ResumeList = dbmodel.Resumes.ToList();
                    foreach (Resume u in ResumeList)
                    {
                        if (u.Predicted_labels == job.Job_designation)
                        {
                            UserList.Add(u);
                        }
                    }
                }
                ViewBag.UserList = UserList;
                return View("NewViewResumes");
            }
            catch(Exception ex)
            {
                ViewBag.newerror = "No resumes are uploaded";
                return View("ErrorNewViewResumes");
            }
        }

        public FileResult Resume_Viewer(string Name)
        {
            
            byte[] bytes = System.IO.File.ReadAllBytes(Name);
            return new FileContentResult(bytes, "application/pdf");
        }
        public ActionResult EmployerLogout()
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
