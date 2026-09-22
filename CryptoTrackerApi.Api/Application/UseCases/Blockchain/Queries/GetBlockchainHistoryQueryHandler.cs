using AutoMapper;
using CryptoTrackerApi.Application.DTOs;
using CryptoTrackerApi.Application.Interfaces;
using CryptoTrackerApi.Domain.Entities;
using CryptoTrackerApi.Domain.Extensions;

namespace CryptoTrackerApi.Application.UseCases.Blockchain.Queries
{
    public class GetBlockchainHistoryQueryHandler(IBlockchainRepository blockchainRepository,
        IMapper mapper)
    {
        public async Task<List<BaseBlockchainDataDto>> Handle(GetBlockchainHistoryQuery request, CancellationToken cancellationToken = default)
        {
            var records = await blockchainRepository.GetBlockchainHistoryAsync();

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
