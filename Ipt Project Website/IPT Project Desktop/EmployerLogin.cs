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
    public partial class EmployerLogin : Form
    {
        public EmployerLogin()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string email = emailBox.Text;
            string password = passwordBox.Text;
            int employerID = 0;
            bool validated = false;

            List<Employer> UserList = new List<Employer>();
            using (DatabaseModel dbModel = new DatabaseModel())
            {
                UserList = dbModel.Employers.OrderBy(a => a.Email).ToList();
                foreach (Employer u in UserList)
                {
                    if (u.Email == email && u.Password == password)
                    {
                        validated = true;
                        employerID = u.id;                      
                    }                  
                }
                if(validated)
                {
                    MessageBox.Show("Login Successful!");
                    this.Hide();
                    EmployerPage employerPage = new EmployerPage(employerID);
                    employerPage.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid Email or Password!");
                }
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Homepage homepage = new Homepage();
            homepage.ShowDialog();
            this.Close();
        }

    }
}
