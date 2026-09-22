using CryptoTrackerApi.Application.Interfaces;
using CryptoTrackerApi.Domain.Entities;
using CryptoTrackerApi.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CryptoTrackerApi.Infrastructure.Persistence
{
    public class BlockchainRepository(CryptoDbContext cryptoDbContext) : IBlockchainRepository
    {
        public async Task AddBlockchainDataAsync(IEnumerable<BlockchainData> blockchainDataList)
        {
            await cryptoDbContext.BlockchainData.AddRangeAsync(blockchainDataList);
            await cryptoDbContext.SaveChangesAsync();
        }

        public async Task<List<BlockchainData>> GetBlockchainHistoryAsync()
        {
            return await cryptoDbContext.BlockchainData
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<BlockchainData>> GetBlockchainHistoryByUrlAsync(string networkName)
        {
            if (!Enum.TryParse<BlockchainNetworkEnum>(networkName, true, out var parsedNetwork))
            {
                return []; 
            }

            return await cryptoDbContext.BlockchainData
                .Where(x => x.Network == parsedNetwork)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}
