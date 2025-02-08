using DentalClinicManagement.DAL.DataBase;
using DentalClinicManagement.DAL.Models;
using DentalClinicManagement.PL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DentalClinicManagement.BL.Repositories
{
    public class ReceptionistRepo
    {
        private readonly AppDBContext appDbContext;
        private readonly UserRepo<Receptionist> validateUser;

        public ReceptionistRepo()
        {
            appDbContext = new AppDBContext();
            validateUser = new UserRepo<Receptionist>(appDbContext);
        }

        // ✅ استرجاع جميع الاستقباليين
        public List<Receptionist> GetAll()
        {
            return appDbContext.Receptionists.ToList();
        }

       
        // ✅ إضافة استقبال جديد مع تشفير كلمة المرور
        public Receptionist Add(Receptionist receptionist)
        {
            if (receptionist == null)
                throw new ArgumentNullException(nameof(receptionist), "Receptionist object is null");

            receptionist.Password = UserRepo<Receptionist>.HashPassword(receptionist.Password); // Hash before saving
            appDbContext.Receptionists.Add(receptionist);
            appDbContext.SaveChanges();
            return receptionist;
        }

        // ✅ تحديث بيانات استقبال
        public void Update(Receptionist updatedReceptionist)
        {
            if (updatedReceptionist == null)
                throw new ArgumentNullException(nameof(updatedReceptionist), "Receptionist object is null");

            // ✅ تأكد من أن المستخدم موجود في قاعدة البيانات
            var existingReceptionist = appDbContext.Receptionists.Find(updatedReceptionist.Id);
            if (existingReceptionist == null)
                throw new KeyNotFoundException("Receptionist ID not found");

            // تحديث الحقول
            existingReceptionist.Name = updatedReceptionist.Name;
            existingReceptionist.Email = updatedReceptionist.Email;
            existingReceptionist.Phone = updatedReceptionist.Phone;

            // ✅ تحديث كلمة المرور فقط إذا تم تغييرها
            if (!string.IsNullOrWhiteSpace(updatedReceptionist.Password))
            {
                existingReceptionist.Password = UserRepo<Receptionist>.HashPassword(updatedReceptionist.Password);
            }

            appDbContext.SaveChanges();
        }

        // ✅ حذف استقبال
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
