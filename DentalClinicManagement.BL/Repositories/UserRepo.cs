using DentalClinicManagement.DAL.DataBase;
using DentalClinicManagement.DAL.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DentalClinicManagement.BL.Repositories
    {
        public class UserRepo
        {
            private readonly AppDBContext appDbContext;

            public UserRepo(AppDBContext dbContext)
            {
                appDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            }

        public UserRepo()
        {
        }

        // ✅ Validate user and return their role (Dentist or Receptionist)
        public object ValidateUser(string username, string password, out string userType)
            {
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    userType = null;
                    return null;
                }

                // 🔹 Check in Receptionists Table
                var receptionist = appDbContext.Receptionists.FirstOrDefault(r => r.Name == username);
                if (receptionist != null && VerifyPassword(password, receptionist.Password))
                {
                    userType = "Receptionist";
                    return receptionist;
                }

                // 🔹 Check in Dentists Table
                var dentist = appDbContext.Dentists.FirstOrDefault(d => d.Name == username);
                if (dentist != null && VerifyPassword(password, dentist.Password))
                {
                    userType = "Dentist";
                    return dentist;
                }

                // 🔹 No match found
                userType = null;
                return null;
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



