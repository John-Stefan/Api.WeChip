using Api.WeChip.Models;
using System.ComponentModel.DataAnnotations;

namespace Api.WeChip.ViewModels
{
    public class CreateClienteViewModel
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public string Telefone { get; set; }
        public decimal Credito { get; set; }
        public int StatusId { get; set; }
    }
}
