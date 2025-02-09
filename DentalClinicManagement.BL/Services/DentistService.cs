using DentalClinicManagement.DAL.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DentalClinicManagement.DAL.Models;
namespace DentalClinicManagement.BL.Services
{
    public class DentistService
    {
        private readonly HelperClass _dbHelper = new HelperClass();

        public List<Patient> GetDoctorPatients(int doctorId)
        {
            using (var connection = _dbHelper.GetConnection())
            {
                string query = @"SELECT Id, Name, Email, Phone, Address, gender, DOB, Allergies FROM Patients WHERE Id = @DoctorId";
                var patients = connection.Query<Patient>(query, new { DoctorId = doctorId }).ToList();
                return patients;
            }
        }
    }



}
