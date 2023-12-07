using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeneakerKop.Migrations
{
    public partial class adeddAvailableQtyColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvailableStock",
                table: "CartItem",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableStock",
                table: "CartItem");
        }
    }
}
