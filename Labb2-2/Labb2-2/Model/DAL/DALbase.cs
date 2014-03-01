using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Configuration;

namespace Labb2_2.Model.DAL
{
    public abstract class DALbase
    {
        private static string connectionString;

        static DALbase()
        {
            connectionString = WebConfigurationManager.ConnectionStrings["myConnectionString"].ConnectionString;
        }

        protected SqlConnection CreateConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}