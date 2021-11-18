using System.Text.Json.Serialization;

namespace Api.WeChip.Models
{
    public class OfertaProduto
    {
        public int Id { get; set; }        
        public string? Descricao { get; set; }
        public Produto Produto { get; set; }
    }
}
