using System.ComponentModel.DataAnnotations;

namespace Aplicacao.Models
{
    public class UsuarioViewModel
    {
        public int? Codigo { get; set; }
        [Required(ErrorMessage = "Informe o seu Nome!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Informe o seu Email!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Informe a sua Senha!")]
        public string Senha { get; set; }
    }
}
