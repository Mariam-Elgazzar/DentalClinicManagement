using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DentalClinicManagement.DAL.DataBase
{
    public class HelperClass
    {
        public SqlConnection GetConnection()
        {
            return new SqlConnection("Data Source=(localdb)\\ProjectModels;Initial Catalog=DentalClinicManagement;Integrated Security=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
       
    }
}
