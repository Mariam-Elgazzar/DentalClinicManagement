using DentalClinicManagement.DAL.DataBase;
using DentalClinicManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinicManagement.BL.Repositories
{
    public class TreatmentRepo
    {
        AppDBContext dBContext;

        public TreatmentRepo()
        {
            dBContext = new AppDBContext();
        }

        public List<Treatment> GetAll()
        {
            return dBContext.Treatments.ToList();
        }

        public Treatment Add(Treatment treatment)
        {
            if (treatment == null)
            {
                throw new Exception("obj is null");
            }

            dBContext.Treatments.Add(treatment);
            dBContext.SaveChanges();
            return treatment;
        }

        public void Update(Treatment treatment)
        {

            if (!dBContext.Treatments.Any(i => i.Id == treatment.Id))
            {
                throw new Exception("obj is null or id is not found ");
            }

            dBContext.Treatments.Update(treatment);
            dBContext.SaveChanges();
        }

        public void Delete(int id)
        {

            if (!dBContext.Treatments.Any(i => i.Id == id))
            {
                throw new Exception("obj is null or id is not found");
            }

            dBContext.Treatments.Remove(dBContext.Treatments.Find(id));

            dBContext.SaveChanges();
        }
    }
}
