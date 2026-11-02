using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skovbyprojekt.Technical_Services
{
    public class DatabaseConnection
    {
        private string connectionString = @"Data Source=10.56.8.36;Initial Catalog=DB52;Persist Security Info=True;User ID=STUDENT52;Password=OPENDB_52";
        private SqlConnection connection;
        private bool isOpen;
        
        public DatabaseConnection()
        {
            this.connection = new SqlConnection(connectionString);
            this.isOpen = false;
        }
        
        public void Open()
        {
            if (!isOpen)
            {
                connection.Open();
                isOpen = true;
            }
        }

        public void Close()
        {
            if (isOpen)
            {
                connection.Close();
                isOpen = false;
            }
        }
        
        public SqlDataReader ExecuteQuery(string query)
        {
            if (!isOpen)
            {
                throw new InvalidOperationException("The connection is not open.");
            }

            var command = new SqlCommand(query, connection);
            return command.ExecuteReader();
        }

        //UPDATE or DELETE
        public int ExecuteNonQuery(string query)
        {
            if (!isOpen)
            {
                throw new InvalidOperationException("The connection is not open.");
            }

            var command = new SqlCommand(query, connection);
            return command.ExecuteNonQuery();
        }
    }

}


       

