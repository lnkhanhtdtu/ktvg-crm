using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ktvg.Crm.Migrations
{
    /// <inheritdoc />
    public partial class Add_Column_Result : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Result",
                schema: "dbo",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Result",
                schema: "dbo",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Result",
                schema: "dbo",
                table: "ContactPurposes");

            migrationBuilder.DropColumn(
                name: "Result",
                schema: "dbo",
                table: "ContactProjects");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Result",
                schema: "dbo",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Result",
                schema: "dbo",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Result",
                schema: "dbo",
                table: "ContactPurposes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Result",
                schema: "dbo",
                table: "ContactProjects",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
