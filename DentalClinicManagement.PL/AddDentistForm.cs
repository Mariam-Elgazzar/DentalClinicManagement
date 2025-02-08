using DentalClinicManagement.BL.Repositories;
using DentalClinicManagement.DAL.Models;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DentalClinicManagement.PL
{
    public partial class AddDentistForm : MaterialForm
    {
        private readonly DentistRepo _dentistRepo;

        public AddDentistForm()
        {
            InitializeComponent();
            _dentistRepo = new DentistRepo();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue600, Primary.Blue700, Primary.Blue200,
                Accent.LightBlue200, TextShade.WHITE);

            SetupUI();
        }

        private void SetupUI()
        {
            this.Text = "Add New Dentist";
            this.Size = new Size(400, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblName = new Label { Text = "Full Name", Location = new Point(20, 70) };
            MaterialTextBox txtName = new MaterialTextBox { Location = new Point(20, 100), Width = 350 };

            Label lblEmail = new Label { Text = "Email", Location = new Point(20, 160) };
            MaterialTextBox txtEmail = new MaterialTextBox { Location = new Point(20, 200), Width = 350 };

            Label lblPhone = new Label { Text = "Phone", Location = new Point(20, 260) };
            MaterialTextBox txtPhone = new MaterialTextBox { Location = new Point(20, 300), Width = 350 };

            Label lblSpecialist = new Label { Text = "Specialization", Location = new Point(20, 360) };
            MaterialTextBox txtSpecialist = new MaterialTextBox { Location = new Point(20, 400), Width = 350 };

            Label lblPassword = new Label { Text = "Password", Location = new Point(20, 460) };
            MaterialTextBox txtPassword = new MaterialTextBox
            {
                Location = new Point(20, 500),
                Width = 350,
                // UseSystemPasswordChar = true // إخفاء كلمة المرور
            };

            MaterialButton btnSave = new MaterialButton
            {
                Text = "Save",
                Location = new Point((400/2-40), 560),
                Width = 400,
                HighEmphasis = true
            };

            btnSave.Click += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtName.Text) ||
                    string.IsNullOrWhiteSpace(txtPhone.Text) ||
                    string.IsNullOrWhiteSpace(txtSpecialist.Text) ||
                    string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Name, Phone, Specialization, and Password are required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Dentist newDentist = new Dentist
                {
                    Name = txtName.Text,
                    Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text,
                    Phone = txtPhone.Text,
                    Specialist = txtSpecialist.Text,
                    Password = txtPassword.Text // يفضل التشفير هنا
                };

                _dentistRepo.Add(newDentist);
                MessageBox.Show("Dentist added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            };

            this.Controls.AddRange(new Control[]
            {
                lblName, txtName,
                lblEmail, txtEmail,
                lblPhone, txtPhone,
                lblSpecialist, txtSpecialist,
                lblPassword, txtPassword,
                btnSave
            });
        }
    }
}
