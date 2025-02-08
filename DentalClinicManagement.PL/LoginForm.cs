using DentalClinicManagement.BL.Repositories;
using DentalClinicManagement.DAL.DataBase;
using DentalClinicManagement.DAL.Models;
using System;
using System.Windows.Forms;

namespace DentalClinicManagement.PL
{
    public partial class LoginForm : Form
    {
        private readonly AppDBContext _dbContext;

        public LoginForm()
        {
            InitializeComponent();
            _dbContext = new AppDBContext();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string password = txtUserPassword.Text;

            var receptionistRepo = new UserRepo<Receptionist>(_dbContext);
            var (receptionist, userType) = receptionistRepo.ValidateUser(username, password);

            if (receptionist == null)
            {
                var dentistRepo = new UserRepo<Dentist>(_dbContext);
                var (dentist, dentistUserType) = dentistRepo.ValidateUser(username, password);

                receptionist = dentist;
                userType = dentistUserType;
            }

            if (receptionist != null)
            {
                MessageBox.Show($"Welcome, {username}! You are logged in as a {userType}.",
                                "Login Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 🔹 Redirect based on user type
                if (userType == "Receptionist")
                {
                    Welcome welcome = new Welcome(receptionist);
                    
                    welcome.ShowDialog();
                    this.Hide();
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
