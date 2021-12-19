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
    public partial class EmployerPage : Form
    {
        private int employerID;
        public EmployerPage()
        {
            InitializeComponent();
        }

        public EmployerPage(int eID)
        {
            employerID = eID;
            string name = "";
            List<Employer> UserList = new List<Employer>();
            using (DatabaseModel dbModel = new DatabaseModel())
            {
                UserList = dbModel.Employers.OrderBy(a => a.Email).ToList();
                foreach (Employer u in UserList)
                {
                    if (u.id == employerID)
                    {
                        name = Convert.ToString(u.First_name) + " " + Convert.ToString(u.Last_name);
                    }
                }
            }
            InitializeComponent();
            label2.Text = "Hello, " + name + "!";
            Refresh();
        }

        private void JobButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            JobPost jobPost = new JobPost(employerID);
            jobPost.ShowDialog();
            this.Close();
        }

        private void ViewButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            ViewApplicants viewApplicants = new ViewApplicants(employerID);
            viewApplicants.ShowDialog();
            this.Close();
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Homepage homepage = new Homepage();
            homepage.ShowDialog();
            this.Close();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
