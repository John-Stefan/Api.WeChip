using System.Text.Json.Serialization;

namespace Api.WeChip.Models
{
    public class OfertaProduto
    {
        public int Id { get; set; }        
        public string? Descricao { get; set; }
        public int ProdutoId { get; set; }
        public int OfertaId { get; set; }

        [JsonIgnore]
        public Produto Produto { get; set; }

        [JsonIgnore]
        public Oferta Oferta { get; set; }
    }
}
