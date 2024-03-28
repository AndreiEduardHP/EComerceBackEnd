using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Places.Migrations
{
    /// <inheritdoc />
    public partial class FixOrderProductMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OderId",
                table: "OrderProducts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OderId",
                table: "OrderProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
