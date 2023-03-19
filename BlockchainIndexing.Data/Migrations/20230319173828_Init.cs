using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace BlockchainIndexing.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "blocks",
                columns: table => new
                {
                    blockID = table.Column<int>(type: "int", precision: 20, nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    blockNumber = table.Column<int>(type: "int", precision: 20, nullable: false),
                    hash = table.Column<string>(type: "varchar(66)", maxLength: 66, nullable: true),
                    parentHash = table.Column<string>(type: "varchar(66)", maxLength: 66, nullable: true),
                    miner = table.Column<string>(type: "varchar(42)", maxLength: 42, nullable: true),
                    blockReward = table.Column<decimal>(type: "decimal(50,0)", precision: 50, scale: 0, nullable: false),
                    gasLimit = table.Column<decimal>(type: "decimal(50,0)", precision: 50, scale: 0, nullable: false),
                    gasUsed = table.Column<decimal>(type: "decimal(50,0)", precision: 50, scale: 0, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blocks", x => x.blockID);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    transactionID = table.Column<int>(type: "int", precision: 20, nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    blockID = table.Column<int>(type: "int", precision: 20, nullable: false),
                    hash = table.Column<string>(type: "varchar(66)", maxLength: 66, nullable: true),
                    from = table.Column<string>(type: "varchar(42)", maxLength: 42, nullable: true),
                    to = table.Column<string>(type: "varchar(42)", maxLength: 42, nullable: true),
                    value = table.Column<decimal>(type: "decimal(50,0)", precision: 50, scale: 0, nullable: false),
                    gas = table.Column<decimal>(type: "decimal(50,0)", precision: 50, scale: 0, nullable: false),
                    gasPrice = table.Column<decimal>(type: "decimal(50,0)", precision: 50, scale: 0, nullable: false),
                    transactionIndex = table.Column<int>(type: "int", precision: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.transactionID);
                    table.ForeignKey(
                        name: "FK_Transactions_blocks_blockID",
                        column: x => x.blockID,
                        principalTable: "blocks",
                        principalColumn: "blockID",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_blockID",
                table: "Transactions",
                column: "blockID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "blocks");
        }
    }
}
