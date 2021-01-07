using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class ForeignKeysSpecified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_RequesterNavId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_RequesterNavId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "RequesterNavId",
                table: "Requests");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequesterId",
                table: "Requests",
                column: "RequesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_RequesterId",
                table: "Requests",
                column: "RequesterId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Users_RequesterId",
                table: "Requests");

            migrationBuilder.DropIndex(
                name: "IX_Requests_RequesterId",
                table: "Requests");

            migrationBuilder.AddColumn<int>(
                name: "RequesterNavId",
                table: "Requests",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_RequesterNavId",
                table: "Requests",
                column: "RequesterNavId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Users_RequesterNavId",
                table: "Requests",
                column: "RequesterNavId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
