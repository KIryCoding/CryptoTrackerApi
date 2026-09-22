using System.Text.Json.Serialization;

namespace CryptoTrackerApi.Application.DTOs
{
    public record AccountBlockchainDataDto : BaseBlockchainDataDto
    {
        [JsonPropertyName("high_gas_price")]
        public long? HighGasPrice { get; init; }

        [JsonPropertyName("medium_gas_price")]
        public long? MediumGasPrice { get; init; }

        [JsonPropertyName("low_gas_price")]
        public long? LowGasPrice { get; init; }

        [JsonPropertyName("high_priority_fee")]
        public long? HighPriorityFee { get; init; }

        [JsonPropertyName("medium_priority_fee")]
        public long? MediumPriorityFee { get; init; }

        [JsonPropertyName("low_priority_fee")]
        public long? LowPriorityFee { get; init; }

        [JsonPropertyName("base_fee")]
        public long? BaseFee { get; init; }
    }
}
