using DentalClinicManagement.DAL.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using DentalClinicManagement.PL;

namespace DentalClinicManagement.BL.Repositories
{
    public class ReceptionistRepo
    {
         AppDBContext appDbContext;
        UserRepo validateUser;
        public ReceptionistRepo( )
        {
            appDbContext = new AppDBContext();
            validateUser = new UserRepo();
        }

        
        public List<Receptionist> GetAll()
        {
            return appDbContext.Receptionists.ToList();
        }

        
        public Receptionist Add(Receptionist receptionist)
        {
            if (receptionist == null)
                throw new ArgumentNullException(nameof(receptionist), "Receptionist object is null");

            receptionist.Password = UserRepo.HashPassword(receptionist.Password); // Hash before saving
            appDbContext.Receptionists.Add(receptionist);
            appDbContext.SaveChanges();
            return receptionist;
        }

        public void Update(Receptionist receptionist)
        {
            if (receptionist == null)
                throw new ArgumentNullException(nameof(receptionist), "Receptionist object is null");

            if (!appDbContext.Receptionists.Any(r => r.Id == receptionist.Id))
                throw new KeyNotFoundException("Receptionist ID not found");

            receptionist.Password = UserRepo.HashPassword(receptionist.Password);
            //??????????????????????????????????????????????
            appDbContext.Entry(receptionist).State = EntityState.Modified;
            appDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var receptionist = appDbContext.Receptionists.Find(id);
            if (receptionist == null)
                throw new KeyNotFoundException("Receptionist ID not found");

            appDbContext.Receptionists.Remove(receptionist);
            appDbContext.SaveChanges();
        }
    }
}
