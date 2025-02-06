using DentalClinicManagement.BL.Repositories;
using DentalClinicManagement.DAL.DataBase;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace DentalClinicManagement.PL
{
    public partial class LoginForm : Form
    {
        ReceptionistRepo _receptionistRepo;
        DentistRepo _Dentist;
        public LoginForm()
        {
            InitializeComponent();

            _receptionistRepo = new ReceptionistRepo();
            _Dentist = new DentistRepo();
        }


        private void LoginForm_Load(object sender, EventArgs e)
        {
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            UserRepo userRepo = new UserRepo(new AppDBContext());

            string username = txtUserName.Text;
            string password = txtUserPassword.Text;

            string userType;
            var user = userRepo.ValidateUser(username, password, out userType);

            if (user != null)
            {
                MessageBox.Show($"Welcome, {username}! You are logged in as a {userType}.",
                                "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 🔹 Redirect based on user type
                if (userType == "Receptionist")
                {
                    //ReceptionistDashboard dashboard = new ReceptionistDashboard();
                    //dashboard.Show();
                }
                else if (userType == "Dentist")
                {
                    //DentistDashboard dashboard = new DentistDashboard();
                    //dashboard.Show();
                }

                this.Hide(); // Hide login form after successful login
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.",
                                "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
