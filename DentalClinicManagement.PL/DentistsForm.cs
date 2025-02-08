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
    public partial class DentistsForm : MaterialForm
    {
        DataGridView dataGrid;
        DentistRepo _DentistRepo;
        List<Dentist> _dentists;
        Receptionist loggedResptionist;
        public DentistsForm(Receptionist receptionist )
        {
            InitializeComponent();
            _DentistRepo = new DentistRepo();
            _dentists = _DentistRepo.GetAll().ToList();
            loggedResptionist =receptionist;
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue600, Primary.Blue700, Primary.Blue200,
                Accent.LightBlue200, TextShade.WHITE);

            SetupUI();
            LoadDentists();
        }

        private void SetupUI()
        {
            this.Text = "Dentists Management";
            this.Size = new Size(1000, 1000);
            this.StartPosition = FormStartPosition.CenterScreen;

            Panel sidebar = new Panel
            {
                BackColor = Color.FromArgb(33, 150, 243),
                Dock = DockStyle.Left,
                Width = 220
            };
            this.Controls.Add(sidebar);

            PictureBox logo = new PictureBox
            {
                Image = Image.FromFile("icons8-dentist-48.png"),
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 100,
                Height = 100,
                Top = 10,
                Left = (sidebar.Width - 100) / 2
            };
            sidebar.Controls.Add(logo);
            int yOffset = 20;
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
            patientsButton.Click += (sender, e) => OpenForm(new PatientsForm(loggedResptionist));
            sidebar.Controls.Add(patientsButton);

            
            MaterialButton dentistsButton = new MaterialButton
            {
                Text = "Sessions",
                Dock = DockStyle.Top,
                Width = sidebar.Width - 20,
                Left = 10,
                Height = 50,
                Margin = new Padding(10, 5, 10, 5),
                HighEmphasis = false
            };
            //dentistsButton.Click += (sender, e) => ;
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
                Text = "Dentists",
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

            searchButton.Click += (sender, e) => SearchDentists(searchBox.Text);
            MaterialButton addButton = new MaterialButton
            {
                Text = "Add",
                Width = 200,
                Location = new Point(700, 15),
                HighEmphasis = true
            };
            topPanel.Controls.Add(addButton);
            addButton.Click += addButton_Click;
            MaterialButton updateButton = new MaterialButton
            {
                Text = "Edit",
                Width = 200,
                Location = new Point(800, 15),
                HighEmphasis = true
            };
            topPanel.Controls.Add(updateButton);
            updateButton.Click += updateButton_Click;
            MaterialButton deleteButton = new MaterialButton
            {
                Text = "Delete",
                Width = 200,
                Location = new Point(900, 15),
                HighEmphasis = true
            };
            topPanel.Controls.Add(deleteButton);
            deleteButton.Click += deleteButton_Click;
            dataGrid = new DataGridView
            {
                Location = new Point(240, 190),
                Width = 800,
                Height = 400,
                Padding = DefaultMargin,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,  // الهيدر يتكيف مع المحتوى
                BackgroundColor = Color.FromArgb(243, 243, 243),
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true
            };

            dataGrid.Columns.Add("Id", "ID");
            dataGrid.Columns.Add("Name", "Dentist Name");
            dataGrid.Columns.Add("Specialization", "Specialization");

            this.Controls.Add(dataGrid);
        }

        private void LoadDentists()
        {
            dataGrid.Rows.Clear();
            foreach (var dentist in _dentists)
            {
                dataGrid.Rows.Add(dentist.Id, dentist.Name, dentist.Specialist);
            }
        }
        private void OpenForm(Form form)
        {


            this.Hide();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
            this.Close();
        }

        private void DentistsForm_Load(object sender, EventArgs e)
        {

        }

        private void SearchDentists(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                LoadDentists();
                return;
            }

            var filteredDentists = _DentistRepo.GetAll()
                .Where(d => d.Name.Contains(query) ||
                            d.Phone.Contains(query) ||
                            d.Email.Contains(query) ||
                            d.Specialist.Contains(query))
                .ToList();

            dataGrid.Rows.Clear();
            foreach (var dentist in filteredDentists)
            {
                dataGrid.Rows.Add(dentist.Id, dentist.Name, dentist.Phone, dentist.Specialist);
            }
        }


        private void addButton_Click(object sender, EventArgs e)
        {
            using (AddDentistForm addForm = new AddDentistForm())
            {
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    LoadDentists();
                }
            }
        }


        private void updateButton_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGrid.SelectedRows[0].Cells["Id"].Value);

                Dentist selectedDentist = _DentistRepo.GetById(id);

                if (selectedDentist != null)
                {
                    EditDentistForm editForm = new EditDentistForm(selectedDentist);
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        LoadDentists();
                    }
                }
                else
                {
                    MessageBox.Show("Dentist not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a dentist to edit.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGrid.SelectedRows[0].Cells["Id"].Value);

                DialogResult result = MessageBox.Show("Are you sure you want to delete this dentist?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    _DentistRepo.Delete(id);
                    LoadDentists();
                }
            }
            else
            {
                MessageBox.Show("Please select a dentist to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        }
    }
