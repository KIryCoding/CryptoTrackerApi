using CryptoTrackerApi.Domain.Enums;
using System.Text.Json.Serialization;

namespace CryptoTrackerApi.Application.DTOs
{
    [JsonDerivedType(typeof(UtxoBlockchainDataDto), typeDiscriminator: "utxo")]
    [JsonDerivedType(typeof(AccountBlockchainDataDto), typeDiscriminator: "account")]
    public record BaseBlockchainDataDto
    {
        [JsonPropertyName("name")]
        public required string Name { get; init; }

        [JsonPropertyName("height")]
        public int Height { get; init; }

        [JsonPropertyName("hash")]
        public required string Hash { get; init; }

        [JsonPropertyName("time")]
        public DateTimeOffset Time { get; init; }

        [JsonPropertyName("latest_url")]
        public required string LatestUrl { get; init; }

        [JsonPropertyName("previous_hash")]
        public string? PreviousHash { get; init; }

        [JsonPropertyName("previous_url")]
        public string? PreviousUrl { get; init; }

        [JsonPropertyName("peer_count")]
        public int? PeerCount { get; init; }

        [JsonPropertyName("unconfirmed_count")]
        public int? UnconfirmedCount { get; init; }

        [JsonPropertyName("last_fork_height")]
        public int? LastForkHeight { get; init; }

        [JsonPropertyName("last_fork_hash")]
        public string? LastForkHash { get; init; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
