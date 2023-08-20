namespace BitcoinProject.Interfaces
{
    public interface IDatabaseOptions
    {
       string ConnectionString { get; set; }
        string Database { get; set; }
    }
}
