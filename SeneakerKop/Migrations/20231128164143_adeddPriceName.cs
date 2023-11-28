using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeneakerKop.Migrations
{
    public partial class adeddPriceName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "CartItem",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "CartItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "CartItem");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "CartItem");
        }
    }
}
