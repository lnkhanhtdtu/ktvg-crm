using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ktvg.Crm.Migrations
{
    /// <inheritdoc />
    public partial class Add_Column_For_ContactHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                schema: "dbo",
                table: "ContactHistories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactHistories_CustomerId",
                schema: "dbo",
                table: "ContactHistories",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactHistories_Customers_CustomerId",
                schema: "dbo",
                table: "ContactHistories",
                column: "CustomerId",
                principalSchema: "dbo",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactHistories_Customers_CustomerId",
                schema: "dbo",
                table: "ContactHistories");

            migrationBuilder.DropIndex(
                name: "IX_ContactHistories_CustomerId",
                schema: "dbo",
                table: "ContactHistories");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "dbo",
                table: "ContactHistories");
        }
    }
}
