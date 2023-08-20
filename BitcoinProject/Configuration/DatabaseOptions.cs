using BitcoinProject.Interfaces;

namespace BitcoinProject.Configuration
{
    public class DatabaseOptions :IDatabaseOptions
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
    }
}
