using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Antomi.Data.Migrations
{
    public partial class mig_CreateNewDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSpecific",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "ProductGroups",
                columns: new[] { "GroupId", "GroupImageName", "GroupTitle", "IsDeleted", "ParentId" },
                values: new object[] { 1, "Group.png", "کالای دیجیتال", false, null });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "IsDeleted", "RoleTitle" },
                values: new object[,]
                {
                    { 1, false, "مدیر سایت" },
                    { 2, false, "دستیار مدیر" },
                    { 3, false, "کاربر عادی" }
                });

            migrationBuilder.InsertData(
                table: "WalletTypes",
                columns: new[] { "TypeId", "TypeName" },
                values: new object[,]
                {
                    { 1, "واریز" },
                    { 2, "برداشت" }
                });

            migrationBuilder.InsertData(
                table: "ProductGroups",
                columns: new[] { "GroupId", "GroupImageName", "GroupTitle", "IsDeleted", "ParentId" },
                values: new object[] { 2, "Group.png", "موبایل", false, 1 });

            migrationBuilder.InsertData(
                table: "ProductGroups",
                columns: new[] { "GroupId", "GroupImageName", "GroupTitle", "IsDeleted", "ParentId" },
                values: new object[] { 3, "Group.png", "سامسونگ", false, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductGroups",
                keyColumn: "GroupId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "WalletTypes",
                keyColumn: "TypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WalletTypes",
                keyColumn: "TypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductGroups",
                keyColumn: "GroupId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ProductGroups",
                keyColumn: "GroupId",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "IsSpecific",
                table: "Products");
        }
    }
}
