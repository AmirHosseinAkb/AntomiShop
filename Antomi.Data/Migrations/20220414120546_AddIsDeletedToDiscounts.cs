using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Antomi.Data.Migrations
{
    public partial class AddIsDeletedToDiscounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Discounts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Discounts");
        }
    }
}
