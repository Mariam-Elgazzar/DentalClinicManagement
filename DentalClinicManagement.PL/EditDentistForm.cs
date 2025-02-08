using DentalClinicManagement.DAL.Models;
using DentalClinicManagement.BL.Repositories;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DentalClinicManagement.PL
{
    public partial class EditDentistForm : MaterialForm
    {
        private Dentist _dentist;
        private DentistRepo _dentistRepo;

        // الحقول الخاصة بتعديل الطبيب
        private MaterialTextBox txtName;
        private MaterialTextBox txtSpecialization;
        private MaterialButton btnSave;
        private MaterialButton btnCancel;

        public EditDentistForm(Dentist dentist)
        {
            InitializeComponent();
            _dentist = dentist;
            _dentistRepo = new DentistRepo();
            SetupUI();
            LoadDentistData();
        }

        private void SetupUI()
        {
            this.Text = "Edit Dentist";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue600, Primary.Blue700, Primary.Blue200,
                Accent.LightBlue200, TextShade.WHITE);

            Label lblName = new Label
            {
                Text = "Name:",
                Location = new Point(20, 40),
                AutoSize = true
            };
            this.Controls.Add(lblName);

            txtName = new MaterialTextBox
            {
                Width = 250,
                Location = new Point(100, 35)
            };
            this.Controls.Add(txtName);

            Label lblSpecialization = new Label
            {
                Text = "Specialization:",
                Location = new Point(20, 90),
                AutoSize = true
            };
            this.Controls.Add(lblSpecialization);

            txtSpecialization = new MaterialTextBox
            {
                Width = 250,
                Location = new Point(100, 85)
            };
            this.Controls.Add(txtSpecialization);

            btnSave = new MaterialButton
            {
                Text = "Save",
                Width = 100,
                Location = new Point(70, 150),
                HighEmphasis = true
            };
            btnSave.Click += btnSave_Click;
            this.Controls.Add(btnSave);

            btnCancel = new MaterialButton
            {
                Text = "Cancel",
                Width = 100,
                Location = new Point(200, 150),
                HighEmphasis = true
            };
            btnCancel.Click += (sender, e) => this.Close();
            this.Controls.Add(btnCancel);
        }

        private void LoadDentistData()
        {
            txtName.Text = _dentist.Name;
            txtSpecialization.Text = _dentist.Specialist;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtSpecialization.Text))
            {
                MessageBox.Show("Please fill all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _dentist.Name = txtName.Text;
            _dentist.Specialist = txtSpecialization.Text;

            _dentistRepo.Update(_dentist);

            MessageBox.Show("Dentist updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
