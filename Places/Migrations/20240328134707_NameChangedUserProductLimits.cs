using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Places.Migrations
{
    /// <inheritdoc />
    public partial class NameChangedUserProductLimits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProductLimit_Products_ProductId",
                table: "UserProductLimit");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProductLimit_UserProfile_UserId",
                table: "UserProductLimit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProductLimit",
                table: "UserProductLimit");

            migrationBuilder.RenameTable(
                name: "UserProductLimit",
                newName: "UserProductLimits");

            migrationBuilder.RenameIndex(
                name: "IX_UserProductLimit_UserId",
                table: "UserProductLimits",
                newName: "IX_UserProductLimits_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProductLimit_ProductId",
                table: "UserProductLimits",
                newName: "IX_UserProductLimits_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProductLimits",
                table: "UserProductLimits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProductLimits_Products_ProductId",
                table: "UserProductLimits",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProductLimits_UserProfile_UserId",
                table: "UserProductLimits",
                column: "UserId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProductLimits_Products_ProductId",
                table: "UserProductLimits");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProductLimits_UserProfile_UserId",
                table: "UserProductLimits");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProductLimits",
                table: "UserProductLimits");

            migrationBuilder.RenameTable(
                name: "UserProductLimits",
                newName: "UserProductLimit");

            migrationBuilder.RenameIndex(
                name: "IX_UserProductLimits_UserId",
                table: "UserProductLimit",
                newName: "IX_UserProductLimit_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserProductLimits_ProductId",
                table: "UserProductLimit",
                newName: "IX_UserProductLimit_ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProductLimit",
                table: "UserProductLimit",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProductLimit_Products_ProductId",
                table: "UserProductLimit",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProductLimit_UserProfile_UserId",
                table: "UserProductLimit",
                column: "UserId",
                principalTable: "UserProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
