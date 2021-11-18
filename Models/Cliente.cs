using System.Text.Json.Serialization;

namespace Api.WeChip.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public decimal Credito { get; set; }
        public int StatusId { get; set; }

        [JsonIgnore]
        public Status Status { get; set; }
    }
}