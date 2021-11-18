using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.WeChip.Migrations
{
    public partial class insert_values_status : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "Id", "Codigo", "Descricao", "FinalizaCliente", "ContabilizaVenda" },
                values: new object[,]
                {
                    { 1, "0001", "Nome Livre", false, false },
                    { 2, "0007", "Não deseja ser contatado", true, false },
                    { 3, "0009", "Cliente Aceitou Oferta", true, true },
                    { 4, "0015", "Caiu a ligação", false, false },
                    { 5, "0019", "Viajou", false, false },
                    { 6, "0021", "Falecido", true, false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
