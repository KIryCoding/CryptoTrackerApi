using CryptoTrackerApi.Domain.Entities;

namespace CryptoTrackerApi.Application.Interfaces
{
    public interface IBlockchainRepository
    {
        Task AddBlockchainDataAsync(IEnumerable<BlockchainData> blockchainDataList);
        Task<List<BlockchainData>> GetBlockchainHistoryAsync();
        Task<List<BlockchainData>> GetBlockchainHistoryByUrlAsync(string networkName);
    }
}