using CryptoTrackerApi.Application.UseCases.Blockchain.Commands;
using CryptoTrackerApi.Application.UseCases.Blockchain.Queries;
using CryptoTrackerApi.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace CryptoTrackerApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlockchainController(FetchAndStoreBlockchainCommandHandler fetchAndStoreBlockchainCommandHandler,
        GetBlockchainHistoryQueryHandler getBlockchainHistoryQueryHandler,
        GetBlockchainHistoryByNetworkQueryHandler getBlockchainHistoryByNetworkQueryHandler) : ControllerBase
    {
        [HttpPost("fetch-store")]
        public async Task<IActionResult> FetchAndStoreBlockchainData(CancellationToken cancellationToken = default)
        {
            var count = await fetchAndStoreBlockchainCommandHandler.Handle(new FetchAndStoreBlockchainCommand(), cancellationToken);
            return Ok(new { Message = $"Blockchain data fetched and stored {count} successfully." });
        }

        [HttpGet("history")]
        public async Task<IActionResult> GetBlockchainHistory(CancellationToken cancellationToken = default)
        {
            var result = await getBlockchainHistoryQueryHandler.Handle(new GetBlockchainHistoryQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("history/{networkName}")]
        public async Task<IActionResult> GetBlockchainHistoryByNetwork(BlockchainNetworkEnum networkName, CancellationToken cancellationToken = default)
        {
            var result = await getBlockchainHistoryByNetworkQueryHandler.Handle(new GetBlockchainHistoryByNetworkQuery(networkName.ToString()), cancellationToken);
            return Ok(result);
        }
    }
}
