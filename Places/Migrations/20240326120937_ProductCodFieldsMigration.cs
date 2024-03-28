using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Places.Migrations
{
    /// <inheritdoc />
    public partial class ProductCodFieldsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InternalClientCod",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductCod",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InternalClientCod",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductCod",
                table: "Products");
        }
    }
}
