using DentalClinicManagement.BL.Repositories;
using DentalClinicManagement.DAL.Models;
using MaterialSkin;
using MaterialSkin.Controls;
using Microsoft.Identity.Client;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DentalClinicManagement.PL
{
    public partial class Welcome : MaterialForm
    {
        DataGridView dataGrid;
        SessionRepo _SessionRepo;
        List<Session> _sessions;
        private readonly Receptionist loggedResptionist;

        public Welcome(Receptionist receptionist)
        {
            InitializeComponent();
            _SessionRepo = new SessionRepo();
            _sessions = _SessionRepo.GetAll().ToList(); // Fetch sessions without BindingList
            loggedResptionist = receptionist;
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue600, Primary.Blue700, Primary.Blue200,
                Accent.LightBlue200, TextShade.WHITE);

            SetupUI();
            LoadSessions();
        }

        private void SetupUI()
        {
            this.Text = "Dental Clinic";
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
                Image = Image.FromFile("icons8-dentist-time-64.png"),
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 100,
                Height = 100,
                Top = 10,
                Left = (sidebar.Width - 100) / 2
            };


            int yOffset = 20;
            // إضافة زرار Dashboard
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
                Text = "Dentists",
                Dock = DockStyle.Top,
                Width = sidebar.Width - 20,
                Left = 10,
                Height = 50,
                Margin = new Padding(10, 5, 10, 5),
                HighEmphasis = false
            };
            dentistsButton.Click += (sender, e) => OpenForm(new DentistsForm(loggedResptionist));
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
                Text = "Sessions",
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

            searchButton.Click += (sender, e) => SearchSessions(searchBox.Text);


            // 🟢 التاريخ
            //DateTimePicker datePicker = new DateTimePicker
            //{
            //    Format = DateTimePickerFormat.Long,
            //    Width = 160,
            //    Location = new Point(600, 15)
            //};
            //topPanel.Controls.Add(datePicker);

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

            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            dataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);

            dataGrid.DefaultCellStyle.Font = new Font("Arial", 10);

            dataGrid.Columns.Clear();
            dataGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Patient Name",
                Width = 150
            });
            dataGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Dentist Name",
                Width = 150
            });
            dataGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Receptionist Name",
                Width = 150
            });
            dataGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Appointment Date",
                Width = 180,
                DefaultCellStyle = { Format = "g" } // To format the date and time in short format
            });
            dataGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Appointment Time",
                Width = 180,
                DefaultCellStyle = { Format = "g" } // To format the date and time in short format
            });
            dataGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "PId", Visible = false });
            dataGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "DId", Visible = false });
            dataGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "RId", Visible = false });
            dataGrid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", Visible = false });


            this.Controls.Add(dataGrid);
            dataGrid.EnableHeadersVisualStyles = false;
            dataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            dataGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGrid.RowHeadersVisible = true;
            dataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        }



        public void LoadSessions()
        {
            // Clear existing rows
            dataGrid.Rows.Clear();

            // Add each session as a row manually
            foreach (var session in _sessions)
            {
                dataGrid.Rows.Add(
                    session.patient.Name,
                    session.dentist.Name,
                    session.receptionist.Name,
                    session.dateTime.ToShortDateString(),
                    session.dateTime.ToShortTimeString(),
                    session.PId,
                    session.DId,
                    session.RId,
                    session.Id
                    );
            };
        }
        private void OpenForm(Form form)
        {
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
            this.Close();
        }

        private void SearchSessions(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                LoadSessions();
                return;
            }

            var filteredSessions = _SessionRepo.Filter(query);

            dataGrid.Rows.Clear();
            foreach (var session in filteredSessions)
            {
                dataGrid.Rows.Add(
                    session.patient.Name,
                    session.dentist.Name,
                    session.receptionist.Name,
                    session.dateTime.ToShortDateString(),
                    session.dateTime.ToShortTimeString(),
                    session.PId,
                    session.DId,
                    session.RId,
                    session.Id
                 );
            }
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedRows.Count > 0)
            {
                int Pid = Convert.ToInt32(dataGrid.SelectedRows[0].Cells["PId"].Value);
                int Did = Convert.ToInt32(dataGrid.SelectedRows[0].Cells["DId"].Value);
                int Rid = Convert.ToInt32(dataGrid.SelectedRows[0].Cells["RId"].Value);
                int id = Convert.ToInt32(dataGrid.SelectedRows[0].Cells["Id"].Value);

                DialogResult result = MessageBox.Show("Are you sure you want to delete this session?",
                                                      "Confirm Deletion",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    _SessionRepo.Remove(id);
                    LoadSessions();
                }
            }
            else
            {
                MessageBox.Show("Please select a session to delete.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void updateButton_Click(object sender, EventArgs e)
        {
            if (dataGrid.SelectedRows.Count > 0)
            {
                int Did = Convert.ToInt32(dataGrid.SelectedRows[0].Cells["DId"].Value);
                int Rid = Convert.ToInt32(dataGrid.SelectedRows[0].Cells["RId"].Value);
                int Pid = Convert.ToInt32(dataGrid.SelectedRows[0].Cells["PId"].Value);
                int id = Convert.ToInt32(dataGrid.SelectedRows[0].Cells["Id"].Value);
                Session selectedSession = _SessionRepo.GetSessionByIds(Did, Rid, Pid,id);

                if (selectedSession != null)
                {
                    EditSessionForm editForm = new EditSessionForm(selectedSession);
                    DialogResult result = editForm.ShowDialog();

                    if (result == DialogResult.OK)
                    {
                        _SessionRepo.Update(editForm.GetUpdatedSession());
                        LoadSessions();
                    }
                }
                else
                {
                    MessageBox.Show("The selected session was not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a session to update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void addButton_Click(object sender, EventArgs e)
        {
            AddSessionForm addForm = new AddSessionForm(loggedResptionist);
            DialogResult result = addForm.ShowDialog();

            if (result == DialogResult.OK)
            {
                _SessionRepo.Add(addForm.getNewSession);
                LoadSessions(); 
            }
        }

        private void Welcome_Load(object sender, EventArgs e)
        {
            LoadSessions();
        }
    }
}
