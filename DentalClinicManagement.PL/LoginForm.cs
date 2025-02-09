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
            Dentist dentist = null;
            if (receptionist == null)
            {
                var dentistRepo = new UserRepo<Dentist>(_dbContext);
                var (_dentist, dentistUserType) = dentistRepo.ValidateUser(username, password);
                dentist= _dentist;
                userType = dentistUserType;
            }

            if (receptionist != null || dentist !=null)
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
                    DentistForm form = new DentistForm(dentist);
                    form.ShowDialog();
                    this.Hide();

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
