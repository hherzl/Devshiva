using System.Data;
using System.Data.SqlClient;

namespace WideWorldImporters.API.Models
{
    public class AppSettings
    {
        public string ConnectionString { get; set; }
    }

    public static class AppSettingsExtensions
    {
        public static IDbConnection CreateConnection(this AppSettings appSettings)
            => new SqlConnection(appSettings.ConnectionString);
    }
}
