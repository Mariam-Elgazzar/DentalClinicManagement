using DentalClinicManagement.PL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinicManagement.DAL.Models
{
    [PrimaryKey(nameof(DId),nameof(RId))]
    public class DentistManagement
    {
        public  int DId { get; set; }
        [ForeignKey(nameof(DId))]
        public  virtual Dentist dentist { get; set; }


        public  int RId { get; set; }
        [ForeignKey(nameof(RId))]
        public virtual Receptionist receptionist { get; set; }
    }
}
