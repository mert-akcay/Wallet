using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Wallet.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateWalletNumberSequenceLast : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "WalletNumber",
                table: "Wallets",
                type: "integer",
                nullable: false,
                defaultValueSql: "nextval('wallet_number_sequence')",
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_WalletNumber",
                table: "Wallets",
                column: "WalletNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Wallets_WalletNumber",
                table: "Wallets");

            migrationBuilder.AlterColumn<int>(
                name: "WalletNumber",
                table: "Wallets",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldDefaultValueSql: "nextval('wallet_number_sequence')");
        }
    }
}
