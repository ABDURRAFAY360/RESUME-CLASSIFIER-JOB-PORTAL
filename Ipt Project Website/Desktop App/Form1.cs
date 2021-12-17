using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// To import Models of Database
using Desktop_App.Models;
namespace Desktop_App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DBModel model = new DBModel();
            var f=model.Employers.ToList();
            foreach(var v in f)
            {
                MessageBox.Show(v.Email);
            }

        }
    }
}
