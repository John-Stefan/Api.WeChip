using System.ComponentModel.DataAnnotations;

namespace Api.WeChip.ViewModels
{
    public class CreateUsuarioViewModel
    {        
        [Required(ErrorMessage = "O campo usuario é obrigatorio")]
        public string Username { get; set; }  
        
        [Required(ErrorMessage = "O campo senha é obrigatorio")]
        public string Password { get; set; }
    }
}
