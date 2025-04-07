using LinqToDB.Configuration;
using LinqToDB.Data;
using LinqToDB;

namespace BarberPro.Api.Configuration
{
    public static class Linq2DbConfig
    {
        public static void Configurar(string connectionString)
        {
            DataConnection.DefaultSettings = new Linq2DbSettings(connectionString);
        }
    }
}
