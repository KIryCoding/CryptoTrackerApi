using CryptoTrackerApi.Domain.Enums;

namespace CryptoTrackerApi.Domain.Extensions
{
    public static class BlockchainNetworkExtensions
    {
        public static bool IsUtxo(this BlockchainNetworkEnum network) => network switch
        {
            BlockchainNetworkEnum.BtcMain or
            BlockchainNetworkEnum.BtcTest3 or
            BlockchainNetworkEnum.LtcMain or
            BlockchainNetworkEnum.DashMain => true,

            BlockchainNetworkEnum.EthMain => false,
            _ => throw new ArgumentOutOfRangeException(nameof(network))
        };
    }
}
