using AutoMapper;
using CryptoTrackerApi.Application.DTOs;
using CryptoTrackerApi.Application.Interfaces;
using CryptoTrackerApi.Domain.Entities;
using CryptoTrackerApi.Domain.Enums;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CryptoTrackerApi.Infrastructure.ExternalServices
{
    public class BlockCypherClient<TDto>(
    HttpClient httpClient,
    IMapper mapper,
    ILogger<BlockCypherClient<TDto>> logger)
    : IBlockchainDataProvider where TDto : BaseBlockchainDataDto
    {
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
        };

        public async Task<BlockchainData?> FetchAndMapAsync(string url, BlockchainNetworkEnum network, CancellationToken cancellationToken)
        {
            try
            {
                var raw = await httpClient.GetStringAsync(url, cancellationToken);
                var dto = JsonSerializer.Deserialize<TDto>(raw, _jsonOptions);

                if (dto == null)
                {
                    logger.LogWarning("Provider returned null DTO for network {Network} at {Url}", network, url);
                    return null;
                }

                var entity = mapper.Map<BlockchainData>(dto);
                entity.RawJson = raw;
                entity.Network = network;
                return entity;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to fetch data for network {Network} from {Url}", network, url);
                return null;
            }
        }
    }
}