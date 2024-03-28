using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Places.Migrations
{
    /// <inheritdoc />
    public partial class CodBsrMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CodBSR",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodBSR",
                table: "Orders");
        }
    }
}
