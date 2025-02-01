using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinicManagement.DAL.Models
{
    [PrimaryKey(nameof(PId), nameof(TId), nameof(DId))]

    public class Prescription
    {
        public int DId { get; set; }
        [ForeignKey(nameof(DId))]
        public virtual Dentist dentist { get; set; }
        public int PId { get; set; }
        [ForeignKey(nameof(PId))]
        public virtual Patient patient { get; set; }
        public int TId { get; set; }
        [ForeignKey(nameof(TId))]
        public virtual Treatment treatment { get; set; }
    }
}
