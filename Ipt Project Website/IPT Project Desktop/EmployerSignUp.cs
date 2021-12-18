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
using System.Configuration;
using Ipt_Project_Website.Models;

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
            //Regex check = new Regex(@"");
            //Match match = check.Match();

            Regex check;
            Match match;
            bool validated = false;

            string firstName = textBox1.Text;
            check = new Regex(@"^[A-Za-z]+([\ A-Za-z]+)*");
            match = check.Match(firstName);
            if (!match.Success)
            {
                MessageBox.Show("Please enter a valid first name!");
                textBox1.Clear();
            }

            string lastName = textBox2.Text;
            check = new Regex(@"^[A-Za-z]+([\ A-Za-z]+)*");
            match = check.Match(lastName);
            if (!match.Success)
            {
                MessageBox.Show("Please enter a valid last name!");
                textBox2.Clear();
            }

            string email = textBox3.Text;
            

            string password = textBox4.Text;
            check = new Regex(@"^(?=.*?[a-z])(?=.*?[0-9]).{8,}$");
            match = check.Match(password);
            if (!match.Success)
            {
                MessageBox.Show("Password must be minimum eight characters in length and should contain a number!");
                textBox4.Clear();
            }

            string currentEducation = textBox5.Text;

            string employmentStatus = textBox6.Text;

            string phoneNumber = textBox7.Text;
            check = new Regex(@"^(?:(?:\(?(?:00|\+)([1-4]\d\d|[1-9]\d?)\)?)?[\-\.\ \\\/]?)?((?:\(?\d{1,}\)?[\-\.\ \\\/]?){0,})(?:[\-\.\ \\\/]?(?:#|ext\.?|extension|x)[\-\.\ \\\/]?(\d+))?$");
            match = check.Match(phoneNumber);
            if (!match.Success)
            {
                MessageBox.Show("Please enter a valid phone number!");
                textBox7.Clear();
            }

            string city = textBox8.Text;
            
            string country = textBox9.Text;

            string companyName = textBox10.Text;

            string companyContactNumber = textBox11.Text;
            check = new Regex(@"^(?:(?:\(?(?:00|\+)([1-4]\d\d|[1-9]\d?)\)?)?[\-\.\ \\\/]?)?((?:\(?\d{1,}\)?[\-\.\ \\\/]?){0,})(?:[\-\.\ \\\/]?(?:#|ext\.?|extension|x)[\-\.\ \\\/]?(\d+))?$");
            match = check.Match(companyContactNumber);
            if (!match.Success)
            {
                MessageBox.Show("Please enter a valid contact number!");
                textBox11.Clear();
            }

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(currentEducation) || string.IsNullOrEmpty(employmentStatus) || string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(city) || string.IsNullOrEmpty(country) || string.IsNullOrEmpty(companyName) || string.IsNullOrEmpty(companyContactNumber))
            {
                MessageBox.Show("Please complete the form in order to Sign Up.");
            }

            DatabaseModel dbModel = new DatabaseModel();

            var EmailAlreadyExists = dbModel.Employers.Any(x => x.Email == email);
            if (EmailAlreadyExists)
            {
                MessageBox.Show("Email already exists! Please enter another email address.");
                textBox3.Clear();
            }
            var PhoneNumberExists = dbModel.Employers.Any(x => x.Phone_number == phoneNumber);
            if (PhoneNumberExists)
            {
                MessageBox.Show("Phone number already exists! Please enter another phone number.");
                textBox7.Clear();
            }

            if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(currentEducation) && !string.IsNullOrEmpty(employmentStatus) && !string.IsNullOrEmpty(phoneNumber) && !string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(country) && !string.IsNullOrEmpty(companyName) && !string.IsNullOrEmpty(companyContactNumber))
            {
                Employer employer = new Employer();
                employer.First_name = firstName;
                employer.Last_name = lastName;
                employer.Email = email;
                employer.Password = password;
                employer.Current_education = currentEducation;
                employer.Employment_status = employmentStatus;
                employer.Phone_number = phoneNumber;
                employer.City = city;
                employer.Country = country;
                employer.Company_name = companyName;
                employer.Company_contact_number = companyContactNumber;

                using (dbModel = new DatabaseModel())
                {
                    dbModel.Employers.Add(employer);
                    dbModel.SaveChanges();
                }

                MessageBox.Show("Sign Up successful!");

                this.Hide();
                EmployerLogin login = new EmployerLogin();
                login.ShowDialog();
                this.Close();
            }

            

            //string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C: \\Users\\umaim\\Desktop\\Workspaces\\IPT\\Project_IPT_New\\Ipt Project Website\\Ipt Project Website\\App_Data\\Database.mdf\";Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework";
            //SqlConnection connection = new SqlConnection(connectionString);
            
            //if(connection.State == ConnectionState.Open)
            //{
            //    string query = "insert into Employer(First_name, Last_name, Email, Password, Current_education, Employment_status, Phone_number, City, Country, Company_name, Company_contact_number) values('" + firstName + "','" + lastName + "','" + email + "','" + password + "','" + currentEducation + "','" + employmentStatus + "','" + phoneNumber + "','" + city + "','" + country + "','" + companyName + "','" + companyContactNumber + "')";
            //    SqlCommand command = new SqlCommand(query, connection);
            //    MessageBox.Show("Sign Up successful!");
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
