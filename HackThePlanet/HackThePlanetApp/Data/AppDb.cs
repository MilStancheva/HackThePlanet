using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace HackThePlanetApp.Data
{
    public class AppDb : IDisposable, IAppDb
    {
        private MySqlConnection connection;
        public AppDb()
        {
            this.connection = new MySqlConnection(AppConfig.Config["Data:ConnectionString"]);
        }
        public IDbConnection DbConnection => this.connection;

        public void Dispose()
        {
            this.connection.Close();
        }
    }
}