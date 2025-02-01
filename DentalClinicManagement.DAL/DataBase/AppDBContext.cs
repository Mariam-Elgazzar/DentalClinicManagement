using DentalClinicManagement.DAL.Models;
using DentalClinicManagement.PL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinicManagement.DAL.DataBase
{
    public class AppDBContext : DbContext
    {
        public DbSet<Receptionist> Receptionists { get; set; }
        public DbSet<Dentist> Dentists { get; set; }
        public DbSet<DentistManagement> DentistManagements  { get; set; }
        public DbSet<PatientManagement> PatientManagements  { get; set; }
        public DbSet<Patient> Patients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=DentalClinicManagement;Integrated Security=True;Trust Server Certificate=True");
        }
    }
}
