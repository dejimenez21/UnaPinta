using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class RequestNavigationPropertie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_UserNavId",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "UserNavId",
                table: "Requests",
                newName: "RequesterNavId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_UserNavId",
                table: "Requests",
                newName: "IX_Requests_RequesterNavId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_RequesterNavId",
                table: "Requests",
                column: "RequesterNavId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_RequesterNavId",
                table: "Requests");

            migrationBuilder.RenameColumn(
                name: "RequesterNavId",
                table: "Requests",
                newName: "UserNavId");

            migrationBuilder.RenameIndex(
                name: "IX_Requests_RequesterNavId",
                table: "Requests",
                newName: "IX_Requests_UserNavId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_UserNavId",
                table: "Requests",
                column: "UserNavId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
