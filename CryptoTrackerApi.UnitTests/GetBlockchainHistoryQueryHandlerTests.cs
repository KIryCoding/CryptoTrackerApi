using AutoMapper;
using CryptoTrackerApi.Application.DTOs;
using CryptoTrackerApi.Application.Interfaces;
using CryptoTrackerApi.Application.UseCases.Blockchain.Queries;
using CryptoTrackerApi.Domain.Entities;
using NSubstitute;

namespace CryptoTrackerApi.UnitTests;

public class GetBlockchainHistoryQueryHandlerTests
{
    private readonly IBlockchainRepository _repositoryMock;
    private readonly IMapper _mapperMock;
    private readonly GetBlockchainHistoryQueryHandler _handler;

    public GetBlockchainHistoryQueryHandlerTests()
    {
        _repositoryMock = Substitute.For<IBlockchainRepository>();
        _mapperMock = Substitute.For<IMapper>();
        _handler = new GetBlockchainHistoryQueryHandler(_repositoryMock, _mapperMock);
    }

    [Fact]
    public async Task Handle_WhenRepositoryHasRecords_ReturnsMappedList()
    {
        // Arrange
        var mockEntities = new List<BlockchainData> { Substitute.For<BlockchainData>() };
        _repositoryMock.GetBlockchainHistoryAsync().Returns(mockEntities);
        _mapperMock.Map<List<BaseBlockchainDataDto>>(mockEntities)
                   .Returns(new List<BaseBlockchainDataDto> { Substitute.For<BaseBlockchainDataDto>() });

        var query = new GetBlockchainHistoryQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Single(result);
        await _repositoryMock.Received(1).GetBlockchainHistoryAsync();
    }

    [Fact]
    public async Task Handle_WhenRepositoryHasNoRecords_ReturnsEmptyList()
    {
        // Arrange
        _repositoryMock.GetBlockchainHistoryAsync().Returns(new List<BlockchainData>());
        _mapperMock.Map<List<BaseBlockchainDataDto>>(Arg.Any<List<BlockchainData>>())
                   .Returns(new List<BaseBlockchainDataDto>());

        var query = new GetBlockchainHistoryQuery();

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.Empty(result);
        await _repositoryMock.Received(1).GetBlockchainHistoryAsync();
    }
}
