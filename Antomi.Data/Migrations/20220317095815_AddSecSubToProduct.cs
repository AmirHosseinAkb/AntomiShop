using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Antomi.Data.Migrations
{
    public partial class AddSecSubToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SecSubId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Products_SecSubId",
                table: "Products",
                column: "SecSubId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductGroups_SecSubId",
                table: "Products",
                column: "SecSubId",
                principalTable: "ProductGroups",
                principalColumn: "GroupId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductGroups_SecSubId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SecSubId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SecSubId",
                table: "Products");
        }
    }
}
