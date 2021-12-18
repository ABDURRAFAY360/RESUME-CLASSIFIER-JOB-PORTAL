using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;

namespace IPT_Project_Desktop
{
    public partial class ViewApplicants : Form
    {
        private int employerID;
        private List<int> jobs = new List<int>();
        private IDictionary<int, int> Applicants = new Dictionary<int, int>();

        public ViewApplicants()
        {
            InitializeComponent();
        }

        public ViewApplicants(int eID)
        {
            employerID = eID;
            InitializeComponent();
            JobsBox.DropDownStyle = ComboBoxStyle.DropDownList;
            CandidatesBox.DropDownStyle = ComboBoxStyle.DropDownList;

        }

        private void ViewApplicants_Load(object sender, EventArgs e)
        {
            List<Job_post> JobsList = new List<Job_post>();
            
            using (DatabaseModel dbModel = new DatabaseModel())
            {
                JobsList = dbModel.Job_post.OrderBy(a => a.Employer_id).ToList();
                foreach (Job_post j in JobsList)
                {
                    if (j.Employer_id == employerID)
                    {
                        JobsBox.Items.Add(j.Job_designation);
                        jobs.Add(j.Job_id);
                    }
                }
            }
            
            using (DatabaseModel dbModel = new DatabaseModel())
            {              
                List<Job_applicant> ApplicantsList = new List<Job_applicant>();
                ApplicantsList = dbModel.Job_applicant.OrderBy(a => a.applicant_id).ToList();

                foreach (Job_applicant a in ApplicantsList)
                {

                    if (jobs.Contains(a.job_id))
                    {
                        Applicants.Add(a.job_id, a.applicant_id);
                    }
                }
            }

            //string path = @"C:\Users\umaim\Desktop\debug.json";
            //IDictionary<int, int> test = new Dictionary<int, int>();
            //string jsonObj = JsonConvert.SerializeObject(Applicants, Formatting.Indented);
            //File.WriteAllText(path, jsonObj);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            EmployerPage employerPage = new EmployerPage(employerID);
            employerPage.ShowDialog();
            this.Close();
        }

        private void JobsBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedjob = JobsBox.Text;

            CandidatesBox.Text = "";
            CandidatesBox.Items.Clear();

            using (DatabaseModel dbModel = new DatabaseModel())
            {
                List<Job_post> JobsList = new List<Job_post>();
                JobsList = dbModel.Job_post.OrderBy(a => a.Employer_id).ToList();

                foreach (Job_post j in JobsList)
                {
                    if (j.Job_designation == selectedjob)
                    {
                        if (Applicants.ContainsKey(j.Job_id))
                        {
                            CandidatesBox.Items.Add(Applicants[j.Job_id]);
                        }
                    }
                }

            }
        }

        private void ViewButton_Click(object sender, EventArgs e)
        {
            int applicantID = Convert.ToInt32(CandidatesBox.Text);
            string path = "";

            using (DatabaseModel dbModel = new DatabaseModel())
            {
                List<Resume> ResumeList = new List<Resume>();
                ResumeList = dbModel.Resumes.OrderBy(a => a.user_id).ToList();

                foreach (Resume r in ResumeList)
                {
                    if(r.user_id == applicantID)
                    {
                        path = r.filepath;
                    }
                }
            }

            Process.Start(path);

        }
    }
}