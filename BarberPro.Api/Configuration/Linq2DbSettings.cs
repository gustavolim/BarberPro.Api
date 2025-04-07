using LinqToDB.Configuration;

namespace BarberPro.Api.Configuration
{
    public class Linq2DbSettings : ILinqToDBSettings
    {
        private readonly string _connectionString;

        public Linq2DbSettings(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<IDataProviderSettings> DataProviders => new List<IDataProviderSettings>();

        public string DefaultConfiguration => "SQLite";
        public string DefaultDataProvider => "SQLite";

        public IEnumerable<IConnectionStringSettings> ConnectionStrings =>
            new[]
            {
            new ConnectionStringSettings
            {
                Name = "BarberProDB",
                ProviderName = "SQLite",
                ConnectionString = _connectionString
            }
            };
    }
}
