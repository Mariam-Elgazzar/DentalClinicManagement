using DentalClinicManagement.DAL.DataBase;
using DentalClinicManagement.PL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinicManagement.BL.Repositories
{
    public class ReceptionistRepo
    {
        AppDBContext appDbContext;

        /* 
         * Receptionist GetAll()
         * 
         */
        public ReceptionistRepo()
        {
            appDbContext = new AppDBContext();
        }

        public List<Receptionist> GetAll()
        {
            return appDbContext.Receptionists.ToList();
        }

        public Receptionist Add(Receptionist receptionist)
        {

            if (receptionist == null)
            {
                throw new Exception("obj is null");
            }
            appDbContext.Add(receptionist);
            appDbContext.SaveChanges();
            return receptionist;
        }

        public void Update(Receptionist receptionist)
        {
            if (!appDbContext.Receptionists.Any(r => r.Id == receptionist.Id))
            {
                throw new Exception("the object is null or id is not found");
            }
            appDbContext.Update(receptionist);
            appDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            if (!appDbContext.Receptionists.Any(r => r.Id == id))
            {
                throw new Exception("the object is null or id is not found");
            }
            appDbContext.Remove(appDbContext.Receptionists.Find(id));
            appDbContext.SaveChanges();
        }
    }
}
