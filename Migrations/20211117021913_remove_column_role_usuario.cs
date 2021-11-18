using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.WeChip.Migrations
{
    public partial class remove_column_role_usuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "Usuarios");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "Usuarios",
                type: "TEXT",
                nullable: true);
        }
    }
}
