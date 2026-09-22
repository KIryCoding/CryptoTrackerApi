using CryptoTrackerApi.Domain.Entities;

namespace CryptoTrackerApi.Application.Interfaces
{
    public interface IBlockCypherOrchestrator
    {
        Task<List<BlockchainData>> GetAllCryptoDataFromUrlsAsync(CancellationToken cancellationToken = default);
    }
}