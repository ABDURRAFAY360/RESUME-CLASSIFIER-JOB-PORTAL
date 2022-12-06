using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IPT_Project_Desktop
{
    public partial class JobPost : Form
    {
        private int employerID;

        public JobPost()
        {
            InitializeComponent();
        }

        public JobPost(int eID)
        {
            employerID = eID;
            InitializeComponent();
            JobDesignationBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            EmployerPage employerPage = new EmployerPage(employerID);
            employerPage.ShowDialog();
            this.Close();
        }

        private void PostButton_Click(object sender, EventArgs e)
        {
            DatabaseModel dbModel = new DatabaseModel();
            Job_post post = new Job_post();
            
            string jobDesignation = JobDesignationBox.Text;
            string jobDescription = JobDescriptionBox.Text;

            if (string.IsNullOrEmpty(jobDesignation) || string.IsNullOrEmpty(jobDescription))
            {
                MessageBox.Show("Please complete the form in order to Post Job.");
            }

            if (!string.IsNullOrEmpty(jobDesignation) && !string.IsNullOrEmpty(jobDescription))
            {
                post.Employer_id = employerID;
                post.Job_designation = jobDesignation;
                post.Job_description = jobDescription;
                using (dbModel = new DatabaseModel())
                {
                    dbModel.Job_post.Add(post);
                    dbModel.SaveChanges();
                    MessageBox.Show("Job Posted Successfully!");
                    JobDesignationBox.SelectedIndex = -1;
                    JobDescriptionBox.Clear();
                }
            }          
        }
    }
}
