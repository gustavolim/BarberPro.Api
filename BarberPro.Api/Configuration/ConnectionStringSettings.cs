using LinqToDB.Configuration;

namespace BarberPro.Api.Configuration
{
    public class ConnectionStringSettings : IConnectionStringSettings
    {
        public string Name { get; set; }
        public string ProviderName { get; set; }
        public string ConnectionString { get; set; }
        public bool IsGlobal => false;
    }
}
