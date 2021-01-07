using Microsoft.EntityFrameworkCore.Migrations;

namespace Api.Migrations
{
    public partial class DescriptionInSpanishFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "UserTypes",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "BloodTypes",
                newName: "Description");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "UserTypes",
                newName: "Descripcion");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "BloodTypes",
                newName: "Descripcion");
        }
    }
}
