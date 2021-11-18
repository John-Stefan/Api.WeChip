using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Api.WeChip.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }  
        public int TipoId { get; set; }

        [JsonIgnore]
        public ProdutoTipo Tipo { get; set; }
    }
}
