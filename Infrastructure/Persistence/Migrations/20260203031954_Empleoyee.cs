using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Employee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DisplayName",
                table: "employees");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "employees");

            migrationBuilder.RenameColumn(
                name: "Active",
                table: "employees",
                newName: "IsActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "employees",
                newName: "Active");

            migrationBuilder.AddColumn<string>(
                name: "DisplayName",
                table: "employees",
                type: "character varying(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "employees",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
