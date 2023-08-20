namespace BitcoinProject.Interfaces
{
    public interface IBitcoinOptions
    {
        string ApiBaseUrl { get; set; }
        string ApiKey { get; set; }
        string Symbol { get; set; }
        string PeriodId { get; set; }
        int Limit { get; set; }
    }
}