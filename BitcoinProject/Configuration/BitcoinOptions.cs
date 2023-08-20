using BitcoinProject.Interfaces;

namespace BitcoinProject.Configuration
{
    public class BitcoinOptions : IBitcoinOptions
    {
        public string ApiBaseUrl { get; set; }
        public string ApiKey { get; set; }
        public string Symbol { get; set; }
        public string PeriodId { get; set; }
        public int Limit { get; set; }
    }
}
