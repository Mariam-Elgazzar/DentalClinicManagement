using DentalClinicManagement.PL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinicManagement.DAL.Models
{

    public class Session
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key ]
        public int Id { get; set; }
        public int DId { get; set; }
        [ForeignKey(nameof(DId))]
        public virtual Dentist dentist { get; set; }
        public int PId { get; set; }
        [ForeignKey(nameof(PId))]
        public virtual Patient patient { get; set; }


        public int RId { get; set; }
        [ForeignKey(nameof(RId))]
        public virtual Receptionist receptionist { get; set; }

        public DateTime dateTime { get; set; }
    }
}
