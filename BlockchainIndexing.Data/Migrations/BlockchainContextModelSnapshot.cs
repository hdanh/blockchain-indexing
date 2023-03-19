﻿// <auto-generated />
using BlockchainIndexing.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BlockchainIndexing.Data.Migrations
{
    [DbContext(typeof(BlockchainContext))]
    partial class BlockchainContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BlockchainIndexing.Domain.Models.Block", b =>
                {
                    b.Property<int>("BlockId")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(20)
                        .HasColumnType("int")
                        .HasColumnName("blockID");

                    b.Property<int>("BlockNumber")
                        .HasPrecision(20)
                        .HasColumnType("int")
                        .HasColumnName("blockNumber");

                    b.Property<decimal>("BlockReward")
                        .HasPrecision(50)
                        .HasColumnType("decimal(50,0)")
                        .HasColumnName("blockReward");

                    b.Property<decimal>("GasLimit")
                        .HasPrecision(50)
                        .HasColumnType("decimal(50,0)")
                        .HasColumnName("gasLimit");

                    b.Property<decimal>("GasUsed")
                        .HasPrecision(50)
                        .HasColumnType("decimal(50,0)")
                        .HasColumnName("gasUsed");

                    b.Property<string>("Hash")
                        .HasMaxLength(66)
                        .HasColumnType("varchar(66)")
                        .HasColumnName("hash");

                    b.Property<string>("Miner")
                        .HasMaxLength(42)
                        .HasColumnType("varchar(42)")
                        .HasColumnName("miner");

                    b.Property<string>("ParentHash")
                        .HasMaxLength(66)
                        .HasColumnType("varchar(66)")
                        .HasColumnName("parentHash");

                    b.HasKey("BlockId");

                    b.ToTable("blocks", (string)null);
                });

            modelBuilder.Entity("BlockchainIndexing.Domain.Models.Transaction", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasPrecision(20)
                        .HasColumnType("int")
                        .HasColumnName("transactionID");

                    b.Property<int>("BlockId")
                        .HasPrecision(20)
                        .HasColumnType("int")
                        .HasColumnName("blockID");

                    b.Property<string>("From")
                        .HasMaxLength(42)
                        .HasColumnType("varchar(42)")
                        .HasColumnName("from");

                    b.Property<decimal>("Gas")
                        .HasPrecision(50)
                        .HasColumnType("decimal(50,0)")
                        .HasColumnName("gas");

                    b.Property<decimal>("GasPrice")
                        .HasPrecision(50)
                        .HasColumnType("decimal(50,0)")
                        .HasColumnName("gasPrice");

                    b.Property<string>("Hash")
                        .HasMaxLength(66)
                        .HasColumnType("varchar(66)")
                        .HasColumnName("hash");

                    b.Property<string>("To")
                        .HasMaxLength(42)
                        .HasColumnType("varchar(42)")
                        .HasColumnName("to");

                    b.Property<int>("TransactionIndex")
                        .HasPrecision(20)
                        .HasColumnType("int")
                        .HasColumnName("transactionIndex");

                    b.Property<decimal>("Value")
                        .HasPrecision(50)
                        .HasColumnType("decimal(50,0)")
                        .HasColumnName("value");

                    b.HasKey("TransactionId");

                    b.HasIndex("BlockId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("BlockchainIndexing.Domain.Models.Transaction", b =>
                {
                    b.HasOne("BlockchainIndexing.Domain.Models.Block", "Block")
                        .WithMany("Transactions")
                        .HasForeignKey("BlockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Block");
                });

            modelBuilder.Entity("BlockchainIndexing.Domain.Models.Block", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
