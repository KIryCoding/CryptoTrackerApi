using CryptoTrackerApi.Domain.Entities;
using CryptoTrackerApi.Domain.Enums;

namespace CryptoTrackerApi.Application.Interfaces
{
    public interface IBlockchainDataProvider
    {
        Task<BlockchainData?> FetchAndMapAsync(string url, BlockchainNetworkEnum network, CancellationToken cancellationToken);
    }
}