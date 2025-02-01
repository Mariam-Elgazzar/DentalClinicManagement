using DentalClinicManagement.DAL.DataBase;
using DentalClinicManagement.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinicManagement.BL.Repositories
{
    public class PatientRepo
    {
        AppDBContext dBContext;

        public PatientRepo()
        {
            dBContext = new AppDBContext();
        }

        public List<Patient> GetAll()
        {

            return dBContext.Patients.ToList();
        }

        public Patient Add(Patient patient) {
            if(patient == null)
            {
                throw new Exception("obj is null");
            }
            dBContext.Add(patient);
            dBContext.SaveChanges();
            return patient;
        }

        public void Update(Patient patient)
        {
            if (!dBContext.Patients.Any(p=>p.Id==patient.Id))
            {
                throw new Exception("obj is null or id not found");
            }
            dBContext.Update(patient);
            dBContext.SaveChanges();
        }

        public void Delete(int id) {
            if (!dBContext.Patients.Any(p => p.Id == id))
            {
                throw new Exception("obj is null or id not found");
            }
            dBContext.Remove(dBContext.Patients.Find(id));
            dBContext.SaveChanges();
        }
    }
}
