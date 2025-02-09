using DentalClinicManagement.BL.Repositories;
using DentalClinicManagement.DAL.Models;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DentalClinicManagement.PL
{
    public partial class EditPatientForm : MaterialForm
    {
        private readonly PatientRepo _patientRepo;
        private Patient _patient;

        public EditPatientForm(Patient patient)
        {
            InitializeComponent();
            _patientRepo = new PatientRepo();
            _patient = patient;
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue600, Primary.Blue700, Primary.Blue200,
                Accent.LightBlue200, TextShade.WHITE);

            SetupUI();
            LoadPatientData();
        }

        private MaterialTextBox txtName, txtEmail, txtPhone, txtAddress, txtAllergies;
        private ComboBox cmbGender;
        private DateTimePicker dtpDOB;
        private MaterialButton btnSave;

        private void SetupUI()
        {
            this.Text = "Update Patient";
            this.Size = new Size(400, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblName = new Label { Text = "Full Name", Location = new Point(20, 20) };
            txtName = new MaterialTextBox { Location = new Point(20, 45), Width = 350 };

            Label lblEmail = new Label { Text = "Email", Location = new Point(20, 90) };
            txtEmail = new MaterialTextBox { Location = new Point(20, 115), Width = 350 };

            Label lblPhone = new Label { Text = "Phone", Location = new Point(20, 160) };
            txtPhone = new MaterialTextBox { Location = new Point(20, 185), Width = 350 };

            Label lblAddress = new Label { Text = "Address", Location = new Point(20, 230) };
            txtAddress = new MaterialTextBox { Location = new Point(20, 255), Width = 350 };

            Label lblGender = new Label { Text = "Gender", Location = new Point(20, 300) };
            cmbGender = new ComboBox
            {
                Location = new Point(20, 325),
                Width = 350,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbGender.Items.AddRange(new string[] { "Male", "Female" });

            Label lblDOB = new Label { Text = "Date of Birth", Location = new Point(20, 370) };
            dtpDOB = new DateTimePicker
            {
                Location = new Point(20, 395),
                Width = 350,
                Format = DateTimePickerFormat.Short
            };

            Label lblAllergies = new Label { Text = "Allergies", Location = new Point(20, 440) };
            txtAllergies = new MaterialTextBox { Location = new Point(20, 465), Width = 350 };

            btnSave = new MaterialButton
            {
                Text = "Save Changes",
                Location = new Point(20, 510),
                Width = 350,
                HighEmphasis = true
            };
            btnSave.Click += BtnSave_Click;

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

        private void LoadPatientData()
        {
            if (_patient == null)
            {
                MessageBox.Show("Patient not found!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            txtName.Text = _patient.Name;
            txtEmail.Text = _patient.Email;
            txtPhone.Text = _patient.Phone;
            txtAddress.Text = _patient.Address;
            cmbGender.SelectedIndex = (int)_patient.gender;
            dtpDOB.Value = _patient.DOB.Date;
            txtAllergies.Text = _patient.Allergies;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Name and Phone are required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _patient.Name = txtName.Text;
            _patient.Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? null : txtEmail.Text;
            _patient.Phone = txtPhone.Text;
            _patient.Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text;
            _patient.gender = (Gender)cmbGender.SelectedIndex;
            _patient.DOB =dtpDOB.Value;
            _patient.Allergies = string.IsNullOrWhiteSpace(txtAllergies.Text) ? null : txtAllergies.Text;

            _patientRepo.Update(_patient);
            MessageBox.Show("Patient updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
