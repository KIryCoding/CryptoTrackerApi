using CryptoTrackerApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CryptoTrackerApi.Infrastructure.Persistence
{
    public class CryptoDbContext(DbContextOptions<CryptoDbContext> options) : DbContext(options)
    {
        public DbSet<BlockchainData> BlockchainData => Set<BlockchainData>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlockchainData>(entity =>
            {
                entity.ToTable("BlockchainData");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Hash).IsRequired();
                entity.Property(e => e.LatestUrl).IsRequired();
                entity.Property(e => e.RawJson).IsRequired();

                // Index for history searching
                entity.HasIndex(e => new { e.Name, e.CreatedAt });
            });
        }
    }
}

