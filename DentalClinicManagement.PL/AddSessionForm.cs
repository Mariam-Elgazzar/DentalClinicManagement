using DentalClinicManagement.BL.Repositories;
using DentalClinicManagement.DAL.Models;
using MaterialSkin.Controls;
using Microsoft.VisualBasic.ApplicationServices;
using System.Windows.Forms;
using  DentalClinicManagement.PL;
namespace DentalClinicManagement.PL
{
    public partial class AddSessionForm : MaterialForm
    {
        private readonly SessionRepo _sessionRepo;
        private readonly DentistRepo _dentistRepo;
        private readonly PatientRepo _patientRepo;
        public Receptionist loggedInUser; // المستخدم الذي قام بتسجيل الدخول
        public Session getNewSession;

        // Declaring controls for the form
        private MaterialComboBox doctorComboBox;
        private MaterialComboBox patientComboBox;
        private MaterialTextBox receptionistTextBox;
        private MaterialButton saveButton;

        public AddSessionForm(Receptionist user)
        {
            InitializeComponent();
            _sessionRepo = new SessionRepo();
            _dentistRepo = new DentistRepo();
            _patientRepo = new PatientRepo();
            loggedInUser = user;



            // Load doctor and patient lists into combo boxes
            doctorComboBox.DataSource = _dentistRepo.GetAll().ToList();
            doctorComboBox.DisplayMember = "Name";
            doctorComboBox.ValueMember = "Id";


            patientComboBox.DataSource = _patientRepo.GetAll().ToList();
            patientComboBox.DisplayMember = "Name";
            patientComboBox.ValueMember = "Id";

            // Set the receptionist (logged-in user)
            receptionistTextBox.Text = loggedInUser.Name;
        }

        private void InitializeComponent()
        {
            doctorComboBox = new MaterialComboBox();
            patientComboBox = new MaterialComboBox();
            receptionistTextBox = new MaterialTextBox();
            saveButton = new MaterialButton();
            dateTimePicker1 = new DateTimePicker();
            SuspendLayout();
            // 
            // doctorComboBox
            // 
            doctorComboBox.AutoResize = false;
            doctorComboBox.BackColor = Color.FromArgb(255, 255, 255);
            doctorComboBox.Depth = 0;
            doctorComboBox.DisplayMember = "Name";
            doctorComboBox.DrawMode = DrawMode.OwnerDrawVariable;
            doctorComboBox.DropDownHeight = 174;
            doctorComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            doctorComboBox.DropDownWidth = 121;
            doctorComboBox.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            doctorComboBox.ForeColor = Color.FromArgb(222, 0, 0, 0);
            doctorComboBox.IntegralHeight = false;
            doctorComboBox.ItemHeight = 43;
            doctorComboBox.Location = new Point(20, 80);
            doctorComboBox.MaxDropDownItems = 4;
            doctorComboBox.MouseState = MaterialSkin.MouseState.OUT;
            doctorComboBox.Name = "doctorComboBox";
            doctorComboBox.Size = new Size(377, 49);
            doctorComboBox.StartIndex = 0;
            doctorComboBox.TabIndex = 0;
            doctorComboBox.ValueMember = "Id";
            // 
            // patientComboBox
            // 
            patientComboBox.AutoResize = false;
            patientComboBox.BackColor = Color.FromArgb(255, 255, 255);
            patientComboBox.Depth = 0;
            patientComboBox.DisplayMember = "Name";
            patientComboBox.DrawMode = DrawMode.OwnerDrawVariable;
            patientComboBox.DropDownHeight = 174;
            patientComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            patientComboBox.DropDownWidth = 121;
            patientComboBox.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Pixel);
            patientComboBox.ForeColor = Color.FromArgb(222, 0, 0, 0);
            patientComboBox.IntegralHeight = false;
            patientComboBox.ItemHeight = 43;
            patientComboBox.Location = new Point(20, 140);
            patientComboBox.MaxDropDownItems = 4;
            patientComboBox.MouseState = MaterialSkin.MouseState.OUT;
            patientComboBox.Name = "patientComboBox";
            patientComboBox.Size = new Size(377, 49);
            patientComboBox.StartIndex = 0;
            patientComboBox.TabIndex = 1;
            patientComboBox.ValueMember = "Id";
            // 
            // receptionistTextBox
            // 
            receptionistTextBox.AnimateReadOnly = false;
            receptionistTextBox.BorderStyle = BorderStyle.None;
            receptionistTextBox.Depth = 0;
            receptionistTextBox.Font = new Font("Roboto", 16F, FontStyle.Regular, GraphicsUnit.Pixel);
            receptionistTextBox.LeadingIcon = null;
            receptionistTextBox.Location = new Point(20, 200);
            receptionistTextBox.MaxLength = 50;
            receptionistTextBox.MouseState = MaterialSkin.MouseState.OUT;
            receptionistTextBox.Multiline = false;
            receptionistTextBox.Name = "receptionistTextBox";
            receptionistTextBox.ReadOnly = true;
            receptionistTextBox.Size = new Size(377, 50);
            receptionistTextBox.TabIndex = 2;
            receptionistTextBox.Text = "";
            receptionistTextBox.TrailingIcon = null;
            // 
            // saveButton
            // 
            saveButton.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            saveButton.Density = MaterialButton.MaterialButtonDensity.Default;
            saveButton.Depth = 0;
            saveButton.HighEmphasis = true;
            saveButton.Icon = null;
            saveButton.Location = new Point(160, 381);
            saveButton.Margin = new Padding(4, 6, 4, 6);
            saveButton.MouseState = MaterialSkin.MouseState.HOVER;
            saveButton.Name = "saveButton";
            saveButton.NoAccentTextColor = Color.Empty;
            saveButton.Size = new Size(64, 36);
            saveButton.TabIndex = 3;
            saveButton.Text = "Save";
            saveButton.Type = MaterialButton.MaterialButtonType.Contained;
            saveButton.UseAccentColor = false;
            saveButton.Click += saveButton_Click;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(20, 265);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(377, 23);
            dateTimePicker1.TabIndex = 4;
            // 
            // AddSessionForm
            // 
            ClientSize = new Size(452, 498);
            Controls.Add(dateTimePicker1);
            Controls.Add(doctorComboBox);
            Controls.Add(patientComboBox);
            Controls.Add(receptionistTextBox);
            Controls.Add(saveButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "AddSessionForm";
            Text = "Add New Session";
            Load += AddSessionForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (doctorComboBox.SelectedValue == null || patientComboBox.SelectedValue == null)
            {
                MessageBox.Show("Please select a doctor and a patient.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create a new session
            getNewSession = new Session
            {
                DId = Convert.ToInt32(doctorComboBox.SelectedValue),
                PId = Convert.ToInt32(patientComboBox.SelectedValue),
                RId = loggedInUser.Id,
                dateTime = DateTime.Now
            };

            DialogResult = DialogResult.OK; // Close form with confirmation
            Close();
        }

        private void AddSessionForm_Load(object sender, EventArgs e)
        {

        }
    }
}
