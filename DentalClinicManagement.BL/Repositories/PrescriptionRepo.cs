using DentalClinicManagement.DAL.DataBase;
using DentalClinicManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinicManagement.BL.Repositories
{
    public class PrescriptionRepo
    {
        AppDBContext db;
        public PrescriptionRepo(){
            db = new AppDBContext();
        }
        public List<Prescription> GetAll()
        {

            var obj = db.Prescriptions.ToList();
            var all = obj.Where(o => o.dentist.Id == o.DId && o.patient.Id == o.PId && o.treatment.Id == o.TId).ToList();
            return all;

        }

        public Prescription Add(Prescription obj)
        {
            if (obj == null)
            {
                throw new Exception("obj is null");
            }
            db.Prescriptions.Add(obj);
            db.SaveChanges();
            return obj;
        }
        public void Update(Prescription obj)
        {
            if (!db.Prescriptions.Any(i => i.PId == obj.PId && i.TId == obj.TId && i.DId == obj.DId))
            {
                throw new Exception("obj is null or id is not found ");
            }
            db.Prescriptions.Update(obj);
            db.SaveChanges();
        }
        public void Remove(int DID, int RID, int PID)
        {
            if (!db.Prescriptions.Any(i => i.PId == PID && i.TId == RID && i.DId == DID))
            {
                throw new Exception("obj is null or id is not found ");
            }
            var obj = db.Prescriptions.Where(o => o.dentist.Id == DID && o.patient.Id == PID && o.treatment.Id == RID).FirstOrDefault();
            db.Prescriptions.Remove(obj);
            db.SaveChanges();
        }
    }
}
