using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using Dapper;
using System.Data.SqlClient;
using DentalClinicManagement.DAL.DataBase;
using DentalClinicManagement.BL.Services;
using DentalClinicManagement.DAL.Models;

namespace DentalClinicManagement.PL
{
    public partial class DentistForm : MaterialForm
    {
        private DentistService _dentistService = new DentistService();
        private DataGridView dgvPatients;
        private MaterialButton btnLogout, btnShowProfile;
        private Panel buttonPanel;
        private int doctorId;
        Dentist loggedindentist;
        public DentistForm(Dentist dentist)
        {
            loggedindentist = dentist;
            doctorId = dentist.Id;
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue600, Primary.Blue700, Primary.Blue200, Accent.LightBlue200, TextShade.WHITE);

            SetupUI();
            LoadDoctorPatients();
        }

        private void SetupUI()
        {
            dgvPatients = new DataGridView
            {
                Location = new System.Drawing.Point(40, 80),
                Size = new System.Drawing.Size(700, 300),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                BorderStyle = BorderStyle.FixedSingle,
                RowTemplate = { Height = 30 }
            };
            Controls.Add(dgvPatients);

            buttonPanel = new Panel
            {
                Location = new System.Drawing.Point(30, 350),
                Size = new System.Drawing.Size(700, 50)
            };
            Controls.Add(buttonPanel);

            btnShowProfile = new MaterialButton
            {
                Text = "Show Profile",
                Location = new System.Drawing.Point(10, 10)
            };
            btnShowProfile.Click += BtnShowProfile_Click;
            buttonPanel.Controls.Add(btnShowProfile);

            btnLogout = new MaterialButton
            {
                Text = "Logout",
                Location = new System.Drawing.Point(200, 10)
            };
            btnLogout.Click += BtnLogout_Click;
            buttonPanel.Controls.Add(btnLogout);
        }

        private void LoadDoctorPatients()
        {
            var patients = _dentistService.GetDoctorPatients(doctorId);
            dgvPatients.DataSource = patients;
        }

        private void BtnShowProfile_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Doctor Profile feature coming soon!");
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            this.Hide();
            this.Close();
        }
    }
}
