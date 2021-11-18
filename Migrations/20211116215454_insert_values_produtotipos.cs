using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.WeChip.Migrations
{
    public partial class insert_values_produtotipos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ProdutoTipos",
                columns: new[] { "Id", "Descricao" },
                values: new object[,]
                {
                    { 1, "HARDWARE" },
                    { 2, "SOFTWARE" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
