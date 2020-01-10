using Dapper;
using System;
using System.Configuration;
using System.Data;
using System.Data.SQLite;


namespace Abstractions
{
    public class SQLiteDataAccess
    {
        public static SQLiteConnection _dbConnection;

        public static string LoadConectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

          
        public static void OpenDb()
        {
            _dbConnection = new SQLiteConnection(LoadConectionString());

            if (_dbConnection == null)
            {
                throw new NullReferenceException("Please provide a connection");
            }

            // Ensure that the connection state is Open
            if (_dbConnection.State != ConnectionState.Open)
            {
                _dbConnection.Open();
            }

        }

    }
}
