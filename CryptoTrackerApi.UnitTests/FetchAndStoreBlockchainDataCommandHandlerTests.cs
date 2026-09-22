using CryptoTrackerApi.Application.Interfaces;
using CryptoTrackerApi.Application.UseCases.Blockchain.Commands;
using CryptoTrackerApi.Domain.Entities;
using NSubstitute;
using Xunit;

namespace CryptoTrackerApi.UnitTests;

public class FetchAndStoreBlockchainCommandHandlerTests
{
    private readonly IBlockCypherOrchestrator _orchestratorMock;
    private readonly IBlockchainRepository _repositoryMock;
    private readonly FetchAndStoreBlockchainCommandHandler _handler;

    public FetchAndStoreBlockchainCommandHandlerTests()
    {
        _orchestratorMock = Substitute.For<IBlockCypherOrchestrator>();
        _repositoryMock = Substitute.For<IBlockchainRepository>();
        _handler = new FetchAndStoreBlockchainCommandHandler(_orchestratorMock, _repositoryMock);
    }

    [Fact]
    public async Task Handle_WhenOrchestratorReturnsData_SavesToRepository()
    {
        var mockData = new List<BlockchainData> { Substitute.For<BlockchainData>() };
        _orchestratorMock.GetAllCryptoDataFromUrlsAsync().Returns(mockData);
        var command = new FetchAndStoreBlockchainCommand();

        await _handler.Handle(command, CancellationToken.None);

        await _repositoryMock.Received(1).AddBlockchainDataAsync(mockData);
    }

    [Fact]
    public async Task Handle_WhenOrchestratorReturnsNoData_DoesNotSaveToRepository()
    {
        _orchestratorMock.GetAllCryptoDataFromUrlsAsync().Returns(new List<BlockchainData>());
        var command = new FetchAndStoreBlockchainCommand();

        await _handler.Handle(command, CancellationToken.None);

        await _repositoryMock.Received(0).AddBlockchainDataAsync(Arg.Any<IEnumerable<BlockchainData>>());
    }
}
