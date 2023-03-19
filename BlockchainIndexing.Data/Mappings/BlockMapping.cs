using BlockchainIndexing.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlockchainIndexing.Data.Mappings
{
    public class BlockMapping : IEntityTypeConfiguration<Block>
    {
        public void Configure(EntityTypeBuilder<Block> builder)
        {
            builder.HasKey(x => x.BlockId);

            builder.Property(x => x.BlockId)
                .HasColumnName("blockID")
                .HasPrecision(20);

            builder.Property(x => x.BlockNumber)
                .HasColumnName("blockNumber")
                .HasPrecision(20);

            builder.Property(x => x.Hash)
                .HasColumnName("hash")
                .HasMaxLength(66);

            builder.Property(x => x.ParentHash)
                .HasColumnName("parentHash")
                .HasMaxLength(66);

            builder.Property(x => x.Miner)
                .HasColumnName("miner")
                .HasMaxLength(42);

            builder.Property(x => x.BlockReward)
                .HasColumnName("blockReward")
                .HasPrecision(50, 0);

            builder.Property(x => x.GasLimit)
                .HasColumnName("gasLimit")
                .HasPrecision(50, 0);

            builder.Property(x => x.GasUsed)
                .HasColumnName("gasUsed")
                .HasPrecision(50, 0);

            builder.ToTable("blocks");
        }
    }
}