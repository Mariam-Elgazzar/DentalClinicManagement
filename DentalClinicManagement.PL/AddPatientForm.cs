using DentalClinicManagement.BL.Repositories;
using DentalClinicManagement.DAL.Models;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DentalClinicManagement.PL
{
    public partial class AddPatientForm : MaterialForm
    {
        private readonly PatientRepo _patientRepo;

        public AddPatientForm()
        {
            InitializeComponent();
            _patientRepo = new PatientRepo();

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
            this.Text = "Add New Patient";
            this.Size = new Size(400, 900);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblName = new Label { Text = "Full Name", Location = new Point(20, 80) };
            MaterialTextBox txtName = new MaterialTextBox { Location = new Point(20, 110), Width = 350 };

            Label lblEmail = new Label { Text = "Email", Location = new Point(20, 160) };
            MaterialTextBox txtEmail = new MaterialTextBox { Location = new Point(20, 190), Width = 350 };

            Label lblPhone = new Label { Text = "Phone", Location = new Point(20, 240) };
            MaterialTextBox txtPhone = new MaterialTextBox { Location = new Point(20, 270), Width = 350 };

            Label lblAddress = new Label { Text = "Address", Location = new Point(20, 320) };
            MaterialTextBox txtAddress = new MaterialTextBox { Location = new Point(20, 350), Width = 350 };

            Label lblGender = new Label { Text = "Gender", Location = new Point(20, 400) };
            ComboBox cmbGender = new ComboBox
            {
                Location = new Point(20, 430),
                Width = 350,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbGender.Items.AddRange(new string[] { "Male", "Female" });

            Label lblDOB = new Label { Text = "Date of Birth", Location = new Point(20, 480) };
            DateTimePicker dtpDOB = new DateTimePicker
            {
                Location = new Point(20, 510),
                Width = 350,
                Format = DateTimePickerFormat.Short
            };

            Label lblAllergies = new Label { Text = "Allergies", Location = new Point(20, 560) };
            MaterialTextBox txtAllergies = new MaterialTextBox { Location = new Point(20, 590), Width = 350 };

            MaterialButton btnSave = new MaterialButton
            {
                Text = "Save",
                Location = new Point(20, 640),
                Width = 350,
                HighEmphasis = true
            };

            btnSave.Click += (sender, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtPhone.Text))
                {
                    MessageBox.Show("Name and Phone are required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Patient newPatient = new Patient
                {
                    Name = txtName.Text,
                    Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text,
                    Phone = txtPhone.Text,
                    Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text,
                    gender = (Gender)cmbGender.SelectedIndex,
                    DOB = dtpDOB.Value,
                    Allergies = string.IsNullOrWhiteSpace(txtAllergies.Text) ? null : txtAllergies.Text
                };

                _patientRepo.Add(newPatient);
                MessageBox.Show("Patient added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            };

            this.Controls.AddRange(new Control[]
            {
                lblName, txtName,
                lblEmail, txtEmail,
                lblPhone, txtPhone,
                lblAddress, txtAddress,
                lblGender, cmbGender,
                lblDOB, dtpDOB,
                lblAllergies, txtAllergies,
                btnSave
            });
        }
    }
}
