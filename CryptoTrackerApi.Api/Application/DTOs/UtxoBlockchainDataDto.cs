using System.Text.Json.Serialization;

namespace CryptoTrackerApi.Application.DTOs
{
    public record UtxoBlockchainDataDto : BaseBlockchainDataDto
    {
        [JsonPropertyName("high_fee_per_kb")]
        public long? HighFeePerKb { get; init; }

        [JsonPropertyName("medium_fee_per_kb")]
        public long? MediumFeePerKb { get; init; }

        [JsonPropertyName("low_fee_per_kb")]
        public long? LowFeePerKb { get; init; }
    }
}
