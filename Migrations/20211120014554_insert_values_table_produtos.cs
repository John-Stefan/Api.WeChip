using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.WeChip.Migrations
{
    public partial class insert_values_table_produtos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Produtos", 
                columns: new[]
                {
                    "Id", "Codigo", "Descricao", "Preco", "TipoId"
                },
                values: new object[,]
                {
                    { 1, "00015", "Mouse", 20.00, 1 },
                    { 2, "00106", "Teclado", 30.00, 1 },
                    { 3, "00200", "Monitor 17’", 350.00, 1 },
                    { 4, "00211", "Pen Drive 8 GB", 30.00, 1 },
                    { 5, "00314", "Pen Drive 16 GB", 50.00, 1 },
                    { 6, "00459", "AVAST", 199.99, 2 },
                    { 7, "01104", "Pacote Office", 499.00, 2 },
                    { 8, "01108", "Spotify (3 meses)", 45.50, 2 },
                    { 9, "01107", "Netflix (1 mês)", 19.90, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
