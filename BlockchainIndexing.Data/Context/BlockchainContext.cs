using BlockchainIndexing.Data.Mappings;
using BlockchainIndexing.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BlockchainIndexing.Data.Context
{
    public class BlockchainContext : DbContext
    {
        public BlockchainContext(DbContextOptions<BlockchainContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BlockMapping());
            modelBuilder.ApplyConfiguration(new TransactionMapping());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Block> Blocks { get; set; }

        public DbSet<Transaction> Transactions { get; set; }
    }
}