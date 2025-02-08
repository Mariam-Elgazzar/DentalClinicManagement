using DentalClinicManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinicManagement.PL
{
    [Table("Receptionists")]
    public class Receptionist
    {
       public int Id { get; set; }
       public string Name { get; set; }
       public string Email { get; set; }
       public string? Phone { get; set; }
       public string Password { get; set; }
       public bool IsAdmin { get; set; }

        public static implicit operator Receptionist(Dentist v)
        {
            throw new NotImplementedException();
        }
    }
}
