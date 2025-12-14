using System;
using Microsoft.Extensions.Configuration;

namespace BITS.Models
{
    public class ConfigDB
    {
        public static string GetMySqlConnectionString()
        {
            string folder = AppContext.BaseDirectory;

            var builder = new ConfigurationBuilder()
                .SetBasePath(folder)
                .AddJsonFile("mySqlSettings.json", optional: false, reloadOnChange: true);

            string connectionString = builder.Build().GetConnectionString("mySql");
            return connectionString;
        }
    }
}
