using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Antomi.Data.Migrations
{
    public partial class OneToOneRelationBetweenProductAndInventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInventories",
                table: "ProductInventories");

            migrationBuilder.DropIndex(
                name: "IX_ProductInventories_ProductId",
                table: "ProductInventories");

            migrationBuilder.DropColumn(
                name: "InventoryId",
                table: "ProductInventories");

            migrationBuilder.DropColumn(
                name: "ChargeDate",
                table: "ProductInventories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInventories",
                table: "ProductInventories",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInventories",
                table: "ProductInventories");

            migrationBuilder.AddColumn<int>(
                name: "InventoryId",
                table: "ProductInventories",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "ChargeDate",
                table: "ProductInventories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInventories",
                table: "ProductInventories",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventories_ProductId",
                table: "ProductInventories",
                column: "ProductId");
        }
    }
}
