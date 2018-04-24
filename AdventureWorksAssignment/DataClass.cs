using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace salesOrderIDLookUp
{
    class DataClass
    {   // AdvWorks2012 WITH PASSWORD = '1234'
        //connetionString = "Data Source=ServerName;Initial Catalog=DatabaseName;User ID=UserName;Password=Password"
        const string ConnString = @"Server=DTPLAPTOP02;Database=AdventureWorks2012;Trusted_Connection=True;User ID=AdvWorks2012;Password=1234 ";
        SqlConnection sqlConnection;

        private bool dataConnect(string connectionString = "")
        {
            bool returnValue;

            if (connectionString.Length == 0)
                connectionString = ConnString;

            try
            {
                sqlConnection = new SqlConnection(connectionString);
                sqlConnection.Open();
               
                returnValue = true;
            }

            catch (Exception ex)
            {
                returnValue = false;
                throw ex;
            }
            finally
            {
                if (sqlConnection != null)
                {
                    sqlConnection.Close();
                }
                
            }

            return returnValue;
        }




    }
}
