using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.WeChip.Migrations
{
    public partial class alter_table_clientes_column_status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Status_StatusId",
                table: "Clientes");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Status_StatusId",
                table: "Clientes",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Status_StatusId",
                table: "Clientes");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Status_StatusId",
                table: "Clientes",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
