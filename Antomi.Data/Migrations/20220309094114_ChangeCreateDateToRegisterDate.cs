using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Antomi.Data.Migrations
{
    public partial class ChangeCreateDateToRegisterDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "Users",
                newName: "RegisterDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegisterDate",
                table: "Users",
                newName: "CreateDate");
        }
    }
}
