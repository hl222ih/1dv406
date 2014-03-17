using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace Project.Model.DAL
{
    public abstract class DALBase
    {
        private static string connectionString;

        static DALBase()
        {
            connectionString = WebConfigurationManager.ConnectionStrings["BlissKomConnectionString"].ConnectionString;
        }

        protected SqlConnection CreateConnection()
        {
            try
            {
                return new SqlConnection(connectionString);
            }
            catch
            {
                throw new ApplicationException("Misslyckades med att ansluta till databasen.");
            }
        }
    }
}