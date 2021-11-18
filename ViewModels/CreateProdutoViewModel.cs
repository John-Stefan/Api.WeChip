using Api.WeChip.Models;
using System.ComponentModel.DataAnnotations;

namespace Api.WeChip.ViewModels
{
    public class CreateProdutoViewModel
    {
        [Required]
        public string Codigo { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public decimal Preco { get; set; }
        [Required]
        public int Tipo { get; set; }
    }
}
