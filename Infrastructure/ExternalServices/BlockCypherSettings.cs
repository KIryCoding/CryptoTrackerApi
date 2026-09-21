namespace CryptoTrackerApi.Infrastructure.ExternalServices
{
    public class BlockCypherSettings
    {
        public const string SectionName = "BlockCypherSettings";

        public Dictionary<string, string> Endpoints { get; set; } = [];
    }
}
