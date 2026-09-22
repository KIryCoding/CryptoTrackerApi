using CryptoTrackerApi.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace CryptoTrackerApi.Domain.Entities
{
    public class BlockchainData
    {
        public int Id { get; set; }

        [Required]
        public required string Name { get; set; }

        public int Height { get; set; }

        [Required]
        public required string Hash { get; set; }

        public DateTimeOffset Time { get; set; }

        [Url]
        [Required]
        public required string LatestUrl { get; set; }

        public string? PreviousHash { get; set; }
        public string? PreviousUrl { get; set; }

        public int? PeerCount { get; set; }
        public int? UnconfirmedCount { get; set; }

        public long? HighFee { get; set; }
        public long? MediumFee { get; set; }
        public long? LowFee { get; set; }
        public long? HighPriorityFee { get; set; }
        public long? MediumPriorityFee { get; set; }
        public long? LowPriorityFee { get; set; }
        public long? BaseFee { get; set; }

        public int? LastForkHeight { get; set; }
        public string? LastForkHash { get; set; }

        [Required]
        public required string RawJson { get; set; }

        [Required]
        public required BlockchainNetworkEnum Network { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [NotMapped]
        public JsonDocument? ParsedJson => string.IsNullOrWhiteSpace(RawJson) ? null 
                                         : JsonDocument.Parse(RawJson);
    }
}
