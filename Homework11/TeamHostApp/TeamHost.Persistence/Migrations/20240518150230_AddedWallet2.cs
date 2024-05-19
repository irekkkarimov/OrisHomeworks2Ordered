using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TeamHost.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedWallet2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "GamePurchase",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "GamePurchase");
        }
    }
}
