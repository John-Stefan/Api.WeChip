using System.ComponentModel.DataAnnotations;

namespace Api.WeChip.ViewModels
{
    public class CreateStatusViewModel
    {
        [Required]
        public string Codigo { get; set; }
        [Required]
        public string Descricao { get; set; }
        public bool FinalizaCliente { get; set; }
        public bool ContabilizaVenda { get; set; }
    }
}
