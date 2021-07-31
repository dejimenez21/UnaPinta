using Microsoft.EntityFrameworkCore.Migrations;

namespace UnaPinta.Api.Migrations
{
    public partial class FixBloodTypeRequestfield : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BloodTypes_Requests_RequestId",
                table: "BloodTypes");

            migrationBuilder.DropIndex(
                name: "IX_BloodTypes_RequestId",
                table: "BloodTypes");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "BloodTypes");

            migrationBuilder.CreateTable(
                name: "RequestPossibleBloodTypes",
                columns: table => new
                {
                    RequestId = table.Column<long>(type: "bigint", nullable: false),
                    BloodTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestPossibleBloodTypes", x => new { x.RequestId, x.BloodTypeId });
                    table.ForeignKey(
                        name: "FK_RequestPossibleBloodTypes_BloodTypes_BloodTypeId",
                        column: x => x.BloodTypeId,
                        principalTable: "BloodTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestPossibleBloodTypes_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RequestPossibleBloodTypes_BloodTypeId",
                table: "RequestPossibleBloodTypes",
                column: "BloodTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestPossibleBloodTypes");

            migrationBuilder.AddColumn<long>(
                name: "RequestId",
                table: "BloodTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BloodTypes_RequestId",
                table: "BloodTypes",
                column: "RequestId");

            migrationBuilder.AddForeignKey(
                name: "FK_BloodTypes_Requests_RequestId",
                table: "BloodTypes",
                column: "RequestId",
                principalTable: "Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
