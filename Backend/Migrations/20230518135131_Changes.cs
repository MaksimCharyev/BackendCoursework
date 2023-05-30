using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
