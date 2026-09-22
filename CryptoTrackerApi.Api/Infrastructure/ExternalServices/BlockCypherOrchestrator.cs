using CryptoTrackerApi.Application.DTOs;
using CryptoTrackerApi.Application.Interfaces;
using CryptoTrackerApi.Domain.Entities;
using CryptoTrackerApi.Domain.Enums;
using CryptoTrackerApi.Domain.Extensions;
using Microsoft.Extensions.Options;

namespace CryptoTrackerApi.Infrastructure.ExternalServices
{
    public class BlockCypherOrchestrator(
        BlockCypherClient<UtxoBlockchainDataDto> utxoProvider,
        BlockCypherClient<AccountBlockchainDataDto> accountProvider,
        IOptions<BlockCypherSettings> settings,
        ILogger<BlockCypherOrchestrator> logger) : IBlockCypherOrchestrator
    {
        public async Task<List<BlockchainData>> GetAllCryptoDataFromUrlsAsync(CancellationToken cancellationToken = default)
        {
            var endpoints = settings.Value.Endpoints;
            if (endpoints == null || endpoints.Count == 0) return [];

            var tasks = endpoints.Select(async kvp =>
            {
                if (!Enum.TryParse<BlockchainNetworkEnum>(kvp.Key, true, out var network))
                {
                    logger.LogWarning("Unknown network key in settings: {Key}", kvp.Key);
                    return null;
                }

                // UTXO or Account
                return network.IsUtxo()
                    ? await utxoProvider.FetchAndMapAsync(kvp.Value, network, cancellationToken)
                    : await accountProvider.FetchAndMapAsync(kvp.Value, network, cancellationToken);
            });

            var results = await Task.WhenAll(tasks);
            return results.Where(x => x != null).ToList()!;
        }
    }
}