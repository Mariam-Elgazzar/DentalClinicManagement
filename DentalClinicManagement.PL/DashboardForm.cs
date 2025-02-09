using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MaterialSkin;
using MaterialSkin.Controls;
using Dapper;
using Microsoft.Data.SqlClient;
using DentalClinicManagement.DAL.DataBase;
using DentalClinicManagement.BL.Services;

namespace DentalClinicManagement.PL
{
    public partial class DashboardForm : MaterialForm
    {
        private  DashboardService _dashboardService = new DashboardService();
        private Chart chart;
        private MaterialLabel lblTodayAppointments;

        public DashboardForm()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue600, Primary.Blue700, Primary.Blue200, Accent.LightBlue200, TextShade.WHITE);
            SetUp();
            InitializeChart();
            LoadDashboardData();
        }
        #region Design
        private void SetUp()
        {
            lblTodayAppointments = new MaterialLabel
            {
                Text = "Loading...",
                Location = new System.Drawing.Point(50, 100),
                Size = new System.Drawing.Size(250, 40),
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold)
            };
            Controls.Add(lblTodayAppointments);
        }

        private void InitializeChart()
        {
            chart = new Chart
            {
                Size = new System.Drawing.Size(400, 280),
                Location = new System.Drawing.Point(250, 100),
                BackColor = System.Drawing.Color.WhiteSmoke
            };

            ChartArea chartArea = new ChartArea
            {
                BackColor = System.Drawing.Color.AliceBlue
            };
            chart.ChartAreas.Add(chartArea);

            Series series = new Series
            {
                ChartType = SeriesChartType.Pie,
                Name = "Appointments",
                IsValueShownAsLabel = true,
                LabelForeColor = System.Drawing.Color.Black,
                Font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold)
            };
            chart.Series.Add(series);

            Controls.Add(chart);
        }
        #endregion
        private void LoadDashboardData()
        {
            int todayAppointments = _dashboardService.GetTodayAppointments();
            int totalPatients = _dashboardService.GetTotalPatients();
            lblTodayAppointments.Text = $"Today's Appointments: {todayAppointments}";
            chart.Series["Appointments"].Points.Clear();
            chart.Series["Appointments"].Points.AddXY("Today Appointments", todayAppointments);
            chart.Series["Appointments"].Points.AddXY("Other Patients", Math.Max(totalPatients - todayAppointments, 0));
            chart.Series["Appointments"].Palette = ChartColorPalette.BrightPastel;
        }
    }
}
