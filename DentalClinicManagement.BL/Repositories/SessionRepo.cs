using DentalClinicManagement.DAL.DataBase;
using DentalClinicManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinicManagement.BL.Repositories
{
    public class SessionRepo
    {
        AppDBContext db;
        public SessionRepo()
        {
            db = new AppDBContext();
        }
        public List<Session> GetAll() { 

            var obj = db.Sessions.ToList();
            var all = obj.Where(o =>o.dentist.Id==o.DId && o.patient.Id==o.PId && o.receptionist.Id==o.RId).ToList();
            return all;

        }

       public Session Add(Session obj)
        {
            if (obj == null)
            {
                throw new Exception("obj is null");
            }
            db.Sessions.Add(obj);
            db.SaveChanges();
            return obj;
        }
        public void Update(Session obj)
        {
            if (!db.Sessions.Any(i => i.PId == obj.PId && i.RId==obj.RId && i.DId==obj.DId))
            {
                throw new Exception("obj is null or id is not found ");
            }
            db.Sessions.Update(obj);
            db.SaveChanges();
        }
        public void Remove(int DID ,int RID,int PID ) {
            if (!db.Sessions.Any(i => i.PId == PID && i.RId == RID && i.DId == DID))
            {
                throw new Exception("obj is null or id is not found ");
            }
            var obj =db.Sessions.Where(o => o.dentist.Id == DID && o.patient.Id == PID && o.receptionist.Id == RID).FirstOrDefault();
            db.Sessions.Remove(obj);
            db.SaveChanges();
        }
    }
}
