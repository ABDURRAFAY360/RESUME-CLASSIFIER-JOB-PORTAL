using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Data;
using System.Data.SqlClient;

namespace IPT_Project_Desktop
{
    public partial class EmployerSignUp : Form
    {
        public EmployerSignUp()
        {
            InitializeComponent();
        }

        private void SignUpButton_Click(object sender, EventArgs e)
        {
            string firstName;
            string lastName;
            string email;
            string password;
            string currentEducation;
            string employmentStatus;
            string phoneNumber;
            string city;
            string country;
            string companyName;
            string companyContactNumber;
            string connectionString = ConfigurationManager.ConnectionStrings["IPT_Project_Desktop.Properties.Settings.DBConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            //if(con.State == ConnectionState.Open)
            //{
            //    string q = @"select id from employer where ";
            //}
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
