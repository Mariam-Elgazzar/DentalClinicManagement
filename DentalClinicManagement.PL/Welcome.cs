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
    public partial class Welcome : MaterialForm
    {
         DataGridView dataGrid;
         SessionRepo _SessionRepo;
         List<Session> _sessions;

        public Welcome()
        {
            InitializeComponent();
            _SessionRepo = new SessionRepo();
            _sessions = _SessionRepo.GetAll().ToList(); // Fetch sessions without BindingList

            // Initialize MaterialSkin
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Blue600, Primary.Blue700, Primary.Blue200,
                Accent.LightBlue200, TextShade.WHITE);

            // Build the UI
            SetupUI();
            LoadSessions();
        }

        private void SetupUI()
        {
            // 🟢 إعدادات النافذة
            this.Text = "Dental Clinic";
            this.Size = new Size(1000, 1000);
            this.StartPosition = FormStartPosition.CenterScreen;

            // 🟢 الشريط الجانبي
            // 🟢 الشريط الجانبي
            Panel sidebar = new Panel
            {
                BackColor = Color.FromArgb(33, 150, 243),
                Dock = DockStyle.Left,
                Width = 220
            };
            this.Controls.Add(sidebar);

            // 🟢 Logo (تم تعديل موقعه ليكون فوق الأزرار مباشرةً)
            PictureBox logo = new PictureBox
            {
                Image = Image.FromFile("icons8-dentist-time-64.png"),
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 100,
                Height = 100,
                Top = 10, // جعله فوق الأزرار
                Left = (sidebar.Width - 100) / 2 // توسيطه داخل الـ sidebar
            };
           

            // 🟢 الأزرار الجانبية
            Dictionary<string, Form> menuItems = new Dictionary<string, Form>
{
    { "Dashboard", new DashboardForm() },
    // { "Patients", new PatientsForm() },
    // { "Dentists", new DentistsForm() },
    // { "Appointments", new AppointmentsForm() },
    // { "Sessions", new SessionsForm() },
    // { "Settings", new SettingsForm() }
};

            int yOffset =  20; // وضع الأزرار بعد الصورة مباشرة
            foreach (var item in menuItems)
            {
                MaterialButton btn = new MaterialButton
                {
                    Text = item.Key,
                    Dock = DockStyle.Top,
                    Width = sidebar.Width - 20,
                    Left = 10,
                    Height = 50,
                    Margin = new Padding(10, 5, 10, 5),
                    HighEmphasis = false
                };

                btn.Click += (sender, e) => OpenForm(item.Value);

                btn.Top = yOffset;
                sidebar.Controls.Add(btn);
                yOffset += 50;
            }

            // 🟢 الترويسة العلوية
            Panel topPanel = new Panel
            {
                BackColor = Color.White,
                Dock = DockStyle.Top,
                Height = 120
            };
            this.Controls.Add(topPanel);
            topPanel.Controls.Add(logo);

            // 🟢 عنوان الصفحة
            Label titleLabel = new Label
            {
                Text = "Sessions",
                Font = new Font("Arial", 14, FontStyle.Bold),
                Location = new Point(240, 15)
            };
            topPanel.Controls.Add(titleLabel);

            // 🟢 مربع البحث
            MaterialTextBox searchBox = new MaterialTextBox
            {
                Hint = "Search...",
                Width = 180,
                Location = new Point(400, 15)
            };
            topPanel.Controls.Add(searchBox);
            // 🟢 زر البحث
            MaterialButton searchButton = new MaterialButton
            {
                Text = "Search",
                Width = 80,
                Location = new Point(590, 15),
                HighEmphasis = true
            };
            topPanel.Controls.Add(searchButton);

            // ربط الزر بوظيفة البحث
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
                Text = "Manage Sessions",
                Width = 200,
                Location = new Point(720, 15),
                HighEmphasis = true
            };
            topPanel.Controls.Add(addButton);

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

            // تغيير إعدادات الأعمدة
            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            // تغيير حجم الخط في الهيدر
            dataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12, FontStyle.Bold);

            // تغيير حجم الخط في الخلايا
            dataGrid.DefaultCellStyle.Font = new Font("Arial", 10);

            // تعيين الخصائص للأعمدة
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

            this.Controls.Add(dataGrid);
            dataGrid.EnableHeadersVisualStyles = false;
            dataGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.LightGray;
            dataGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 10, FontStyle.Bold);
            dataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGrid.RowHeadersVisible = true;
            dataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        }

        private void LoadSessions()
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
                    session.dateTime.ToShortTimeString());
                    };
        }
        private void OpenForm(Form form)
        {    
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
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
                    session.dateTime.ToShortTimeString()
                    );
            }
        }

        private void Welcome_Load(object sender, EventArgs e)
        {
            // Load event if needed
        }
    }
}
