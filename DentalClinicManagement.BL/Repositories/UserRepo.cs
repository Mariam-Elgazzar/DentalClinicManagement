using DentalClinicManagement.DAL.DataBase;
using DentalClinicManagement.DAL.Models;
using DentalClinicManagement.PL;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DentalClinicManagement.BL.Repositories
{
    public class UserRepo<T> where T : class
    {
        private readonly AppDBContext appDbContext;

        public UserRepo(AppDBContext dbContext)
        {
            appDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        // ✅ Validate user and return specific type (T) with userType string
        public (T user, string userType) ValidateUser(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return (null, null);
            }

            if (typeof(T) == typeof(Receptionist))
            {
                var receptionist = appDbContext.Receptionists.FirstOrDefault(r => r.Name == username) as T;
                if (receptionist != null && VerifyPassword(password, ((Receptionist)(object)receptionist).Password))
                {
                    return (receptionist, "Receptionist");
                }
            }

            if (typeof(T) == typeof(Dentist))
            {
                var dentist = appDbContext.Dentists.FirstOrDefault(d => d.Name == username) as T;
                if (dentist != null && VerifyPassword(password, ((Dentist)(object)dentist).Password))
                {
                    return (dentist, "Dentist");
                }
            }

            return (null, null);
        }

        // ✅ Hash Password before storing it
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        // ✅ Verify Hashed Password
        private bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            string enteredHashed = HashPassword(enteredPassword);
            return enteredHashed == storedHashedPassword;
        }
    }
}
