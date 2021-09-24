using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace CareerCloud.ADODataAccessLayer
{
    public class Initializer
    {
        protected readonly SqlConnection _connection;
        protected readonly string _connectionString;
        
        public Initializer()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connectionString = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
            _connection = new SqlConnection(_connectionString);
        }
    }
}
