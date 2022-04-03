using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Antomi.Data.Migrations
{
    public partial class AddRelationBetweenColorAndOrderDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ColorId",
                table: "OrderDetails",
                column: "ColorId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_ProductColors_ColorId",
                table: "OrderDetails",
                column: "ColorId",
                principalTable: "ProductColors",
                principalColumn: "ColorId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_ProductColors_ColorId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_ColorId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "OrderDetails");
        }
    }
}
