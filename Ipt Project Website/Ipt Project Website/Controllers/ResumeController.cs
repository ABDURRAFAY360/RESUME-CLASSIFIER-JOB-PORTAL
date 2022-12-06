using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ipt_Project_Website.Models;
using System.Web.SessionState;
using System.IO;
using System.Configuration;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Text;
using Newtonsoft.Json;
using System.Net.Http;

namespace Ipt_Project_Website.Controllers
{
    public class ResumeController : Controller
    {

        // GET: Resume
        [HttpGet]
        public ActionResult ResumeUpload()
        {
            if (Session["login"].ToString() == "0")
            {
                ViewBag.Message = "Please login first";
                return RedirectToRoute("Userlogin");
            }
            else if (Session["login"].ToString() == "1" && Session["Employer"].ToString() != "0")
            {
                ViewBag.Message = "Please login first";
                return RedirectToRoute("Userlogin");
            }
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ResumeUpload(Resume resume)
        {
            /*  Response.Write(file);
              Response.End();*/
            string FileName = System.IO.Path.GetFileNameWithoutExtension(resume.UploadFile.FileName);
            string FileExtension = System.IO.Path.GetExtension(resume.UploadFile.FileName);
            FileName = DateTime.Now.ToString("yyyyMMddss") + "-" + FileName.Trim() + FileExtension;
            string UploadPath = ConfigurationManager.AppSettings["UploadFolder"].ToString() + FileName;
            resume.UploadFile.SaveAs(UploadPath);
            DbModel dbmodel = new DbModel();
            StringBuilder text = new StringBuilder();
            using (PdfReader reader = new PdfReader(UploadPath))
            {
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }
            }
            var resume_text = text.ToString();
            var user = new Dictionary<string, string>
            {
                { "resume", resume_text}
            };
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            var client = new HttpClient();
            var response = await client.PostAsync("https://rafay.ap.ngrok.io", data);
            string result = await response.Content.ReadAsStringAsync();
            
            Dictionary<string, string> htmlAttributes =
            JsonConvert.DeserializeObject<Dictionary<string, string>>(result);
        

            string temp = Session["User_ID"].ToString();
            Resume _resume = new Resume();
            _resume.user_id = Int16.Parse(temp);
            _resume.filepath = UploadPath;
            _resume.Raw_text = resume_text;
            _resume.Formated_text = "ree";
            _resume.Predicted_labels = htmlAttributes["1"];
            dbmodel.Resumes.Add(_resume);
            dbmodel.SaveChanges();
           
            ViewBag.SuccessMessage = "Resume Uploaded";
            return View();
        }

    }
}