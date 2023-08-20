using BitcoinProject.Interfaces;

namespace BitcoinProject.Services
{
    public class BitcoinProjectService : IBitcoinProjectService
    {
        private readonly IQueryBitcoinDataService _QueryBitcoinService;

        public BitcoinProjectService(
            IQueryBitcoinDataService queryBitcoinService)
        {
            _QueryBitcoinService = queryBitcoinService;
        }

        public void Execute()
        {
            _QueryBitcoinService.ExecuteQueryData();
        }
    }
}
