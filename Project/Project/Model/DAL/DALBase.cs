using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data.SqlClient;

namespace Project.Model.DAL
{
    /// <summary>
    /// Abstrakt klass för anslutning till databasen.
    /// </summary>
    public abstract class DALBase
    {
        /// <summary>
        /// Innehåller connection-sträng från inställningarna i Web.config.
        /// </summary>
        private static string connectionString;

        /// <summary>
        /// Statisk konstruktor. 
        /// </summary>
        static DALBase()
        {
            //hämta connection-strängen från Web.config och placera i connectionString-variabeln.
            connectionString = WebConfigurationManager.ConnectionStrings["BlissKomConnectionString"].ConnectionString;
        }

        /// <summary>
        /// Skapar och returnerar ett SqlConnection-objekt med den connectionstring som anges i inställningarna.
        /// </summary>
        /// <returns>SqlConnection-objekt.</returns>
        /// <exception cref="ApplicationException">Om operationen misslyckas. (kontrollera vpn-inställningar)</exception>
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