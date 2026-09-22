using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoTrackerApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlockchainData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Height = table.Column<int>(type: "INTEGER", nullable: false),
                    Hash = table.Column<string>(type: "TEXT", nullable: false),
                    Time = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    LatestUrl = table.Column<string>(type: "TEXT", nullable: false),
                    RawJson = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockchainData", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlockchainData_Name_CreatedAt",
                table: "BlockchainData",
                columns: new[] { "Name", "CreatedAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockchainData");
        }
    }
}
