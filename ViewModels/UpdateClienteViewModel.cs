using System.ComponentModel.DataAnnotations;

namespace Api.WeChip.ViewModels
{
    public class UpdateClienteViewModel
    {       
        public string Nome { get; set; }       
        public string CPF { get; set; }        
        public string Telefone { get; set; }
    }
}
