using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace Labb2_2.DAL
{
    public abstract class DALbase
    {
        private static string connectionString;

        static DALbase()
        {

        }

        protected SqlConnection CreateConnection()
        {
            throw new NotImplementedException("inte implementerat");
        }
    }
}