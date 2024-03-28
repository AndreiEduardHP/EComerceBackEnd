using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Places.Migrations
{
    /// <inheritdoc />
    public partial class CompanyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "UserProfile",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cui = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_CompanyId",
                table: "UserProfile",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_Company_CompanyId",
                table: "UserProfile",
                column: "CompanyId",
                principalTable: "Company",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_Company_CompanyId",
                table: "UserProfile");

            migrationBuilder.DropTable(
                name: "Company");

            migrationBuilder.DropIndex(
                name: "IX_UserProfile_CompanyId",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "UserProfile");
        }
    }
}
