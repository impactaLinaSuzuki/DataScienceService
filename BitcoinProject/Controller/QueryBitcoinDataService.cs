using BitcoinProject.Interfaces;
using BitcoinProject.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace BitcoinProject.Controller
{
    public class QueryBitcoinDataService : IQueryBitcoinDataService
    {

        private readonly IBitcoinOptions _BitcoinOptions;
        private readonly IConnectDatabaseService _ConnectDatabase;

        public QueryBitcoinDataService(
            IBitcoinOptions bitcoinOptions,
            IConnectDatabaseService connectDatabase)
        {
            _BitcoinOptions = bitcoinOptions;
            _ConnectDatabase = connectDatabase;
        }

        public void ExecuteQueryData()
        {
            var bitcoinDatas = _SelectBictoinData();
            _SaveDataInDatabase(bitcoinDatas);
        }

        private List<Ohlcv> _SelectBictoinData()
        {
            var ohlcvList = new List<Ohlcv>();
            var apiUrl = $"v1/ohlcv/{_BitcoinOptions.Symbol}/history?period_id={_BitcoinOptions.PeriodId}&limit={_BitcoinOptions.Limit}";
            var client2 = new RestClient(_BitcoinOptions.ApiBaseUrl);
            var request = new RestRequest(apiUrl, Method.Get);

            request.AddHeader("X-CoinAPI-Key", _BitcoinOptions.ApiKey);

            try
            {
                var collection = _ConnectDatabase.ConnectDatabase();

                var lastRecord = collection.Find(Builders<BsonDocument>.Filter.Empty)
                               .SortByDescending(d => d["time_period_end"])
                               .Limit(1)
                               .FirstOrDefault();

                var lastRecordDate = lastRecord != null ? lastRecord["time_period_end"].ToString() : null;

                // Calculate the start date for the API request
                var startDate = lastRecordDate != null
                                ? DateTime.Parse(lastRecordDate).AddDays(1).ToString("yyyy-MM-ddTHH:mm:ss")
                                : DateTime.UtcNow.AddYears(-5).ToString("yyyy-MM-ddTHH:mm:ss");

                RestResponse response = client2.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var content = response.Content;
                    ohlcvList = JsonConvert.DeserializeObject<List<Ohlcv>>(content);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return ohlcvList;
        }

        private void _SaveDataInDatabase(List<Ohlcv> ohlcvsList)
        {
            IMongoCollection<BsonDocument> collection = _ConnectDatabase.ConnectDatabase();

            foreach (var ohlcv in ohlcvsList)
            {
                TimeZoneInfo tzBrasil = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
                DateTime lastUpdate = TimeZoneInfo.ConvertTime(DateTime.UtcNow, tzBrasil);

                ohlcv.LastUpdate = lastUpdate;

                FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Eq("time_period_start", ohlcv.time_period_start);

                UpdateOptions updateOptions = new UpdateOptions
                {
                    // Se o documento não existir, ele será inserido
                    IsUpsert = true
                };
                UpdateDefinition<BsonDocument> update = Builders<BsonDocument>.Update
                    .Set("time_period_start", ohlcv.time_period_start)
                    .Set("time_period_end", ohlcv.time_period_end)
                    .Set("time_open", ohlcv.time_open)
                    .Set("time_close", ohlcv.time_close)
                    .Set("price_open", ohlcv.price_open)
                    .Set("price_high", ohlcv.price_high)
                    .Set("price_low", ohlcv.price_low)
                    .Set("price_close", ohlcv.price_close)
                    .Set("volume_traded", ohlcv.volume_traded)
                    .Set("trades_count", ohlcv.trades_count)
                    .Set("LastUpdate", lastUpdate);

                collection.UpdateOne(filter, update, updateOptions);
            }

            Console.WriteLine("Bitcoin data has been inserted into MongoDB.");
        }
        
    }
}
