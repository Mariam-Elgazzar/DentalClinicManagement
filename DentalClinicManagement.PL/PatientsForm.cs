using DentalClinicManagement.BL.Repositories;
using DentalClinicManagement.DAL.Models;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DentalClinicManagement.PL
{
    public partial class PatientsForm : MaterialForm
    {
        DataGridView dataGrid;
        PatientRepo _patientRepo;
        List<Patient> _patients;
        Receptionist loggedReceptionist;

        public PatientsForm(Receptionist receptionist)
        {
            InitializeComponent();
            _patientRepo = new PatientRepo();
            _patients = _patientRepo.GetAll().ToList();
            loggedReceptionist = receptionist;

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue600, Primary.Blue700, Primary.Blue200,
                Accent.LightBlue200, TextShade.WHITE);

            SetupUI();
            LoadPatients();
        }

        private void SetupUI()
        {
            this.Text = "Patients Management";
            this.Size = new Size(1000, 1000);
            this.StartPosition = FormStartPosition.CenterScreen;

            // الشريط الجانبي
            Panel sidebar = new Panel
            {
                BackColor = Color.FromArgb(33, 150, 243),
                Dock = DockStyle.Left,
                Width = 220
            };
            this.Controls.Add(sidebar);

            PictureBox logo = new PictureBox
            {
                Image = Image.FromFile("icons8-dentist-chair-64.png"),
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 100,
                Height = 100,
                Top = 10,
                Left = (sidebar.Width - 100) / 2
            };
            sidebar.Controls.Add(logo);

            MaterialButton dashboardButton = new MaterialButton
            {
                Text = "Dashboard",
                Dock = DockStyle.Top,
                Width = sidebar.Width - 20,
                Left = 10,
                Height = 50,
                Margin = new Padding(10, 5, 10, 5),
                HighEmphasis = false
            };
            dashboardButton.Click += (sender, e) => OpenForm(new DashboardForm());
            sidebar.Controls.Add(dashboardButton);

            MaterialButton patientsButton = new MaterialButton
            {
                Text = "Patients",
                Dock = DockStyle.Top,
                Width = sidebar.Width - 20,
                Left = 10,
                Height = 50,
                Margin = new Padding(10, 5, 10, 5),
                HighEmphasis = false
            };
            sidebar.Controls.Add(patientsButton);

            MaterialButton dentistsButton = new MaterialButton
            {
                Text = "Dentists",
                Dock = DockStyle.Top,
                Width = sidebar.Width - 20,
                Left = 10,
                Height = 50,
                Margin = new Padding(10, 5, 10, 5),
                HighEmphasis = false
            };
            dentistsButton.Click += (sender, e) => OpenForm(new DentistsForm(loggedReceptionist));
            sidebar.Controls.Add(dentistsButton);

            Panel topPanel = new Panel
            {
                BackColor = Color.White,
                Dock = DockStyle.Top,
                Height = 120
            };
            this.Controls.Add(topPanel);
            topPanel.Controls.Add(logo);

            Label titleLabel = new Label
            {
                Text = "Patients",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Location = new Point(240, 15)
            };
            topPanel.Controls.Add(titleLabel);

            MaterialTextBox searchBox = new MaterialTextBox
            {
                Width = 180,
                Location = new Point(400, 15)
            };
            topPanel.Controls.Add(searchBox);

            MaterialButton searchButton = new MaterialButton
            {
                Text = "Search",
                Width = 80,
                Location = new Point(600, 15),
                HighEmphasis = true
            };
            topPanel.Controls.Add(searchButton);
            searchButton.Click += (sender, e) => SearchPatients(searchBox.Text);

            // زرار إضافة مريض جديد
            MaterialButton addButton = new MaterialButton
            {
                Text = "Add",
                Width = 200,
                Location = new Point(700, 15),
                HighEmphasis = true
            };
            topPanel.Controls.Add(addButton);
            addButton.Click += addButton_Click;

            // زرار تعديل بيانات المريض
            MaterialButton updateButton = new MaterialButton
            {
                Text = "Edit",
                Width = 200,
                Location = new Point(800, 15),
                HighEmphasis = true
            };
            topPanel.Controls.Add(updateButton);
            updateButton.Click += updateButton_Click;

            // زرار حذف المريض
            MaterialButton deleteButton = new MaterialButton
            {
                Text = "Delete",
                Width = 200,
                Location = new Point(900, 15),
                HighEmphasis = true
            };
            topPanel.Controls.Add(deleteButton);
            deleteButton.Click += deleteButton_Click;

            // جدول عرض المرضى
            dataGrid = new DataGridView
            {
                Location = new Point(240, 190),
                Width = 800,
                Height = 400,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                BackgroundColor = Color.FromArgb(243, 243, 243),
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true
            };

            dataGrid.Columns.Add("Id", "ID");
            dataGrid.Columns.Add("Name", "Patient Name");
            dataGrid.Columns.Add("Phone", "Phone Number");
            dataGrid.Columns.Add("Gender", "Gender");

            this.Controls.Add(dataGrid);
        }

        private void LoadPatients()
        {
            dataGrid.Rows.Clear();
            foreach (var patient in _patients)
            {
                dataGrid.Rows.Add(patient.Id, patient.Name, patient.Phone, patient.gender);
            }
        }

        private void OpenForm(Form form)
        {
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void SearchPatients(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                LoadPatients();
                return;
            }

            var filteredPatients = _patientRepo.GetAll()
                .Where(p => p.Name.ToLower().Contains(query.ToLower()) ||
                            p.Phone.ToLower().Contains(query.ToLower()) ||
                            p.Email.ToLower().Contains(query.ToLower()))
                .ToList();

            dataGrid.Rows.Clear();
            foreach (var patient in filteredPatients)
            {
                dataGrid.Rows.Add(patient.Id, patient.Name, patient.Phone, patient.gender);
            }
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            AddPatientForm addForm = new AddPatientForm();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadPatients(); 
            }
        }

        private void updateButton_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGrid.SelectedRows[0].Cells["Id"].Value);

                Patient selectedPatient = _patientRepo.GetById(id);

                if (selectedPatient != null)
                {
                    EditPatientForm editForm = new EditPatientForm(selectedPatient);
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadPatients();
                    }
                }
                else
                {
                    MessageBox.Show("Patient not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a patient to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGrid.SelectedRows[0].Cells["Id"].Value);
                _patientRepo.Delete(id);
                LoadPatients();
            }
        }
    }
}
