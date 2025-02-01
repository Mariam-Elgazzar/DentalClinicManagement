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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\ProjectModels;Initial Catalog=DentalClinicManagement;Integrated Security=True;Trust Server Certificate=True");
        }
    }
}
