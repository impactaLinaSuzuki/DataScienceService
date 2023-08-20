using System;

namespace BitcoinProject.Models
{
    public class Ohlcv
    {
        public string time_period_start { get; set; }
        public string time_period_end { get; set; }
        public string time_open { get; set; }
        public string time_close { get; set; }
        public double price_open { get; set; }
        public double price_high { get; set; }
        public double price_low { get; set; }
        public double price_close { get; set; }
        public double volume_traded { get; set; }
        public int trades_count { get; set; }
        public DateTime LastUpdate { get; set; }

    }
}
