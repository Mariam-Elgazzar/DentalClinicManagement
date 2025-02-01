using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinicManagement.DAL.Models
{
    public enum Gender
    {
        Male,
        Female
    }
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
        public string Phone { get; set; }
        public string? Address {  get; set; }
        public Gender gender { get; set; }
        public DateOnly DOB {  get; set; }
        public string? Allergies { get; set; }

    }
}
