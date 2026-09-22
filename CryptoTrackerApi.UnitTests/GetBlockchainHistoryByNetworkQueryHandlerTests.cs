using AutoMapper;
using CryptoTrackerApi.Application.DTOs;
using CryptoTrackerApi.Application.Interfaces;
using CryptoTrackerApi.Application.UseCases.Blockchain.Queries;
using CryptoTrackerApi.Domain.Entities;
using CryptoTrackerApi.Domain.Enums;
using NSubstitute;

namespace CryptoTrackerApi.UnitTests;

public class GetBlockchainHistoryByNetworkQueryHandlerTests
{
    private readonly IBlockchainRepository _repositoryMock;
    private readonly IMapper _mapperMock;
    private readonly GetBlockchainHistoryByNetworkQueryHandler _handler;

    public GetBlockchainHistoryByNetworkQueryHandlerTests()
    {
        _repositoryMock = Substitute.For<IBlockchainRepository>();
        _mapperMock = Substitute.For<IMapper>();
        _handler = new GetBlockchainHistoryByNetworkQueryHandler(_repositoryMock, _mapperMock);
    }

    [Fact]
    public async Task Handle_WithValidNetwork_ReturnsFilteredMappedList()
    {
        string networkName = "BtcMain";
        var networkEnum = BlockchainNetworkEnum.BtcMain;

        var mockEntities = new List<BlockchainData>
        {
            new BlockchainData
            {
                Name = "Bitcoin",
                Hash = "00000000000000000000",
                LatestUrl = "https://blockcypher.com",
                RawJson = "{}",
                Network = networkEnum
            }
        };

        _repositoryMock.GetBlockchainHistoryByUrlAsync(networkName).Returns(mockEntities);
        _mapperMock.Map<List<BaseBlockchainDataDto>>(mockEntities)
                   .Returns(new List<BaseBlockchainDataDto> { Substitute.For<BaseBlockchainDataDto>() });

        var query = new GetBlockchainHistoryByNetworkQuery(networkName);

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.Single(result);
        await _repositoryMock.Received(1).GetBlockchainHistoryByUrlAsync(networkName);
    }

    [Fact]
    public async Task Handle_WhenRepositoryHasNoRecords_ReturnsEmptyList()
    {
        string networkName = "BtcMain";
        _repositoryMock.GetBlockchainHistoryByUrlAsync(networkName).Returns(new List<BlockchainData>());
        _mapperMock.Map<List<BaseBlockchainDataDto>>(Arg.Any<List<BlockchainData>>()).Returns(new List<BaseBlockchainDataDto>());

        var query = new GetBlockchainHistoryByNetworkQuery(networkName);

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.Empty(result);
        await _repositoryMock.Received(1).GetBlockchainHistoryByUrlAsync(networkName);
    }
}
