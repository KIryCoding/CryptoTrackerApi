using AutoMapper;
using CryptoTrackerApi.Application.DTOs;
using CryptoTrackerApi.Application.Interfaces;
using CryptoTrackerApi.Domain.Entities;
using CryptoTrackerApi.Domain.Extensions;
using System.Linq;

namespace CryptoTrackerApi.Application.UseCases.Blockchain.Queries
{
    public class GetBlockchainHistoryByNetworkQueryHandler(IBlockchainRepository blockchainRepository,
        IMapper mapper)
    {
        public async Task<List<BaseBlockchainDataDto>> Handle(GetBlockchainHistoryByNetworkQuery request, CancellationToken cancellationToken = default)
        {
            var records = await blockchainRepository.GetBlockchainHistoryByUrlAsync(request.NetworkName);

            if (records == null || records.Count == 0)
                return [];

            var dtos = records.Select<BlockchainData, BaseBlockchainDataDto>(record =>
            {
                if (record.Network.IsUtxo())
                {
                    return mapper.Map<UtxoBlockchainDataDto>(record);
                }

                // If not UTXO, return Account type
                return mapper.Map<AccountBlockchainDataDto>(record);

            }).ToList();

            return dtos;

        }
    }
}
