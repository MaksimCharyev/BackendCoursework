using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class NewInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMarkedProducts_Users_UserId1",
                table: "UserMarkedProducts");

            migrationBuilder.DropIndex(
                name: "IX_UserMarkedProducts_UserId1",
                table: "UserMarkedProducts");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserMarkedProducts");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserMarkedProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_UserMarkedProducts_UserId",
                table: "UserMarkedProducts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMarkedProducts_Users_UserId",
                table: "UserMarkedProducts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "IdUser",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMarkedProducts_Users_UserId",
                table: "UserMarkedProducts");

            migrationBuilder.DropIndex(
                name: "IX_UserMarkedProducts_UserId",
                table: "UserMarkedProducts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserMarkedProducts");

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "UserMarkedProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserMarkedProducts_UserId1",
                table: "UserMarkedProducts",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMarkedProducts_Users_UserId1",
                table: "UserMarkedProducts",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "IdUser");
        }
    }
}
