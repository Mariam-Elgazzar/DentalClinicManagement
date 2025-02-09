using Dapper;
using DentalClinicManagement.DAL.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinicManagement.BL.Services
{
    public class DashboardService
    {
        private readonly HelperClass _dbHelper = new HelperClass();

        public int GetTodayAppointments()
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = @"SELECT COUNT(*) FROM Sessions WHERE CAST(dateTime AS DATE) = CAST(GETDATE() AS DATE)";
                return connection.ExecuteScalar<int>(query);
            }
        }

        public int GetTotalPatients()
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = @"SELECT COUNT(*) FROM Patients";
                return connection.ExecuteScalar<int>(query);
            }
        }
    }
}
