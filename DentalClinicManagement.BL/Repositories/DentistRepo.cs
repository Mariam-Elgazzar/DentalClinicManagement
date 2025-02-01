using DentalClinicManagement.DAL.DataBase;
using DentalClinicManagement.DAL.Models;
using DentalClinicManagement.PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinicManagement.BL.Repositories
{
    public class DentistRepo
    {
        AppDBContext dBContext;

        public DentistRepo()
        {
            dBContext = new AppDBContext();
        }

        public List<Dentist> GetAll()
        {
            return dBContext.Dentists.ToList();
        }
        public Dentist Add(Dentist dentist)
        {
            if (dentist == null)
            {
                throw new Exception("obj is null");
            }

            dBContext.Dentists.Add(dentist);
            dBContext.SaveChanges();
            return dentist;
        }

        public void Update(Dentist dentist) {

            if (!dBContext.Dentists.Any(i=>i.Id==dentist.Id))
            {
                throw new Exception("obj is null or id is not found ");
            }

            dBContext.Dentists.Update(dentist);
            dBContext.SaveChanges();
        }
        public void Delete(int id) {

            if (!dBContext.Dentists.Any(i => i.Id == id))
            {
                throw new Exception("obj is null or id is not found");
            }

            dBContext.Dentists.Remove(dBContext.Dentists.Find(id));

            dBContext.SaveChanges();
        }
    }
}
