using Api.WeChip.Models;

namespace Api.WeChip.ViewModels
{
    public class CreateOfertaViewModel
    {
        public decimal ValorTotal { get; set; }
        public int ClienteId { get; set; }
        public Endereco Endereco { get; set; }
        public List<int> ProdutosOfertasId { get; set; }
    }
}
