using DentalClinicManagement.DAL.Models;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DentalClinicManagement.PL
{
    public partial class EditSessionForm : MaterialForm
    {
        private Session updatedSession;

        public Session GetUpdatedSession()
        {
            return updatedSession;
        }

        private void SetUpdatedSession(Session value)
        {
            updatedSession = value;
        }

        private MaterialTextBox txtPatientName;
        private MaterialTextBox txtDentistName;
        private MaterialTextBox txtReceptionistName;
        private DateTimePicker datePicker;
        private DateTimePicker timePicker;
        private MaterialButton btnSave;
        private MaterialButton btnCancel;

        public EditSessionForm(Session session)
        {
            InitializeComponent();
            SetUpdatedSession(session);
            SetupUI();
            LoadSessionData();
        }

        private void SetupUI()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue600, Primary.Blue700, Primary.Blue200,
                Accent.LightBlue200, TextShade.WHITE);

            this.Text = "Edit Session";
            this.Size = new Size(600, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblPatient = new Label
            {
                Text = "Patient Name:",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(50, 50),
                AutoSize = true
            };
            this.Controls.Add(lblPatient);

            txtPatientName = new MaterialTextBox
            {
                Width = 400,
                Location = new Point(50, 80)
            };
            this.Controls.Add(txtPatientName);

            Label lblDentist = new Label
            {
                Text = "Dentist Name:",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(50, 130),
                AutoSize = true
            };
            this.Controls.Add(lblDentist);

            txtDentistName = new MaterialTextBox
            {
                Width = 400,
                Location = new Point(50, 160)
            };
            this.Controls.Add(txtDentistName);

            Label lblReceptionist = new Label
            {
                Text = "Receptionist Name:",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(50, 210),
                AutoSize = true
            };
            this.Controls.Add(lblReceptionist);

            txtReceptionistName = new MaterialTextBox
            {
                Width = 400,
                Location = new Point(50, 240)
            };
            this.Controls.Add(txtReceptionistName);

            Label lblDate = new Label
            {
                Text = "Appointment Date:",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(50, 290),
                AutoSize = true
            };
            this.Controls.Add(lblDate);

            datePicker = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Width = 400,
                Location = new Point(50, 320)
            };
            this.Controls.Add(datePicker);

            Label lblTime = new Label
            {
                Text = "Appointment Time:",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(50, 370),
                AutoSize = true
            };
            this.Controls.Add(lblTime);

            timePicker = new DateTimePicker
            {
                Format = DateTimePickerFormat.Time,
                Width = 400,
                Location = new Point(50, 400),
                ShowUpDown = true
            };
            this.Controls.Add(timePicker);

            btnSave = new MaterialButton
            {
                Text = "Save",
                Width = 120,
                Location = new Point(150, 450),
                HighEmphasis = true
            };
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);

            btnCancel = new MaterialButton
            {
                Text = "Cancel",
                Width = 120,
                Location = new Point(300, 450),
                HighEmphasis = true
            };
            btnCancel.Click += (sender, e) => this.Close();
            this.Controls.Add(btnCancel);
        }

        private void LoadSessionData()
        {
            txtPatientName.Text = GetUpdatedSession().patient.Name;
            txtDentistName.Text = GetUpdatedSession().dentist.Name;
            txtReceptionistName.Text = GetUpdatedSession().receptionist.Name;
            datePicker.Value = GetUpdatedSession().dateTime.Date;
            timePicker.Value = GetUpdatedSession().dateTime;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            GetUpdatedSession().patient.Name = txtPatientName.Text;
            GetUpdatedSession().dentist.Name = txtDentistName.Text;
            GetUpdatedSession().receptionist.Name = txtReceptionistName.Text;
            GetUpdatedSession().dateTime = datePicker.Value.Date + timePicker.Value.TimeOfDay;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void EditSessionForm_Load(object sender, EventArgs e)
        {

        }
    }
}
