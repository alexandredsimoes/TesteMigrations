using Microsoft.EntityFrameworkCore.Migrations;

namespace TesteMigrations.Infrastructure.Persistence.Migrations.Sqlite
{
    public partial class CampoExtra : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CampoExtra",
                table: "TodoItems",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CampoExtra",
                table: "TodoItems");
        }
    }
}
