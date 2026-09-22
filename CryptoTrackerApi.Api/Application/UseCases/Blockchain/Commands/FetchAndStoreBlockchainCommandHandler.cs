using CryptoTrackerApi.Application.Interfaces;

namespace CryptoTrackerApi.Application.UseCases.Blockchain.Commands
{
    public class FetchAndStoreBlockchainCommandHandler(IBlockCypherOrchestrator blockCypherOrchestrator, 
        IBlockchainRepository blockchainRepository)
    {
        public async Task<int> Handle(FetchAndStoreBlockchainCommand request, CancellationToken cancellationToken = default)
        {
            var entities = await blockCypherOrchestrator.GetAllCryptoDataFromUrlsAsync();

            if (entities == null || entities.Count == 0)
                return 0;

            await blockchainRepository.AddBlockchainDataAsync(entities);

            return entities.Count;
        }
    }
}
