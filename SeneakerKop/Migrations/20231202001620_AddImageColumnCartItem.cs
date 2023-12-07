using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeneakerKop.Migrations
{
    public partial class AddImageColumnCartItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "CartItem",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "CartItem");
        }
    }
}
