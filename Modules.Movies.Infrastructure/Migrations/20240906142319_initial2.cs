using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modules.Movies.Infrastructure.Migrations
{
    public partial class initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Movies");

            migrationBuilder.RenameTable(
                name: "Movies",
                schema: "Ticket",
                newName: "Movies",
                newSchema: "Movies");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Ticket");

            migrationBuilder.RenameTable(
                name: "Movies",
                schema: "Movies",
                newName: "Movies",
                newSchema: "Ticket");
        }
    }
}
