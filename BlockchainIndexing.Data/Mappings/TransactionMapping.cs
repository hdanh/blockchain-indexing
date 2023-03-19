using BlockchainIndexing.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainIndexing.Data.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(x => x.TransactionId);

            builder.Property(x => x.TransactionId)
                .HasColumnName("transactionID")
                .ValueGeneratedOnAdd()
                .HasPrecision(20);

            builder.Property(x => x.BlockId)
                .HasColumnName("blockID")
                .HasPrecision(20);

            builder.Property(x => x.Hash)
                .HasColumnName("hash")
                .HasMaxLength(66);

            builder.Property(x => x.From)
                .HasColumnName("from")
                .HasMaxLength(42);

            builder.Property(x => x.To)
                .HasColumnName("to")
                .HasMaxLength(42);

            builder.Property(x => x.Value)
                .HasColumnName("value")
                .HasPrecision(50, 0);

            builder.Property(x => x.Gas)
                .HasColumnName("gas")
                .HasPrecision(50, 0);

            builder.Property(x => x.GasPrice)
                .HasColumnName("gasPrice")
                .HasPrecision(50, 0);

            builder.Property(x => x.TransactionIndex)
                .HasColumnName("transactionIndex")
                .HasPrecision(20);

            builder.HasOne(x => x.Block).WithMany(x => x.Transactions);
        }
    }
}