using DentalClinicManagement.DAL.DataBase;
using DentalClinicManagement.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public Session GetSessionByIds(int dId,int rId ,int pId,int id)
        {
            return db.Sessions.FirstOrDefault(r=>r.Id==id);
        }
        public List<Session> Filter(string query)
        {
            var filteredSessions = db.Sessions
                .Where(s => s.patient.Name.Contains(query) ||
                s.dentist.Name.Contains(query) ||
                s.receptionist.Name.Contains(query))
            .ToList();
            return filteredSessions;
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
            if (obj==null)
            {
                throw new Exception("obj is null or id is not found ");
            }
            
            db.Update(obj);
            db.SaveChanges();
        }
        public void Remove(int id ) {
            if (!db.Sessions.Any(i =>i.Id==id))
            {
                var s = db.Sessions.FirstOrDefault(i => i.Id == id);
                throw new Exception("obj is null or id is not found ");
            }
            var obj =db.Sessions.Where(o => o.Id==id ).FirstOrDefault();
            db.Sessions.Remove(obj);
            db.SaveChanges();
        }
    }
}
