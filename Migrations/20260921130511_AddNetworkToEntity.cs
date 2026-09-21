using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CryptoTrackerApi.Migrations
{
    /// <inheritdoc />
    public partial class AddNetworkToEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "BaseFee",
                table: "BlockchainData",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "HighFee",
                table: "BlockchainData",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "HighPriorityFee",
                table: "BlockchainData",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastForkHash",
                table: "BlockchainData",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LastForkHeight",
                table: "BlockchainData",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LowFee",
                table: "BlockchainData",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LowPriorityFee",
                table: "BlockchainData",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MediumFee",
                table: "BlockchainData",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "MediumPriorityFee",
                table: "BlockchainData",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Network",
                table: "BlockchainData",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PeerCount",
                table: "BlockchainData",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviousHash",
                table: "BlockchainData",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PreviousUrl",
                table: "BlockchainData",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnconfirmedCount",
                table: "BlockchainData",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseFee",
                table: "BlockchainData");

            migrationBuilder.DropColumn(
                name: "HighFee",
                table: "BlockchainData");

            migrationBuilder.DropColumn(
                name: "HighPriorityFee",
                table: "BlockchainData");

            migrationBuilder.DropColumn(
                name: "LastForkHash",
                table: "BlockchainData");

            migrationBuilder.DropColumn(
                name: "LastForkHeight",
                table: "BlockchainData");

            migrationBuilder.DropColumn(
                name: "LowFee",
                table: "BlockchainData");

            migrationBuilder.DropColumn(
                name: "LowPriorityFee",
                table: "BlockchainData");

            migrationBuilder.DropColumn(
                name: "MediumFee",
                table: "BlockchainData");

            migrationBuilder.DropColumn(
                name: "MediumPriorityFee",
                table: "BlockchainData");

            migrationBuilder.DropColumn(
                name: "Network",
                table: "BlockchainData");

            migrationBuilder.DropColumn(
                name: "PeerCount",
                table: "BlockchainData");

            migrationBuilder.DropColumn(
                name: "PreviousHash",
                table: "BlockchainData");

            migrationBuilder.DropColumn(
                name: "PreviousUrl",
                table: "BlockchainData");

            migrationBuilder.DropColumn(
                name: "UnconfirmedCount",
                table: "BlockchainData");
        }
    }
}
