using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations
{
    public partial class initialTodo2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Catalog");

            migrationBuilder.RenameTable(
                name: "Products",
                schema: "Application",
                newName: "Products",
                newSchema: "Catalog");

            migrationBuilder.RenameTable(
                name: "Brands",
                schema: "Application",
                newName: "Brands",
                newSchema: "Catalog");

            migrationBuilder.AddColumn<string>(
                name: "TenantId",
                schema: "Application",
                table: "Tokens",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TenantId",
                schema: "Application",
                table: "Tokens");

            migrationBuilder.RenameTable(
                name: "Products",
                schema: "Catalog",
                newName: "Products",
                newSchema: "Application");

            migrationBuilder.RenameTable(
                name: "Brands",
                schema: "Catalog",
                newName: "Brands",
                newSchema: "Application");
        }
    }
}
