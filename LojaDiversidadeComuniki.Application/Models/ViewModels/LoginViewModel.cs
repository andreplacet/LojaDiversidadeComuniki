using System.ComponentModel.DataAnnotations;

namespace LojaDiversidadeComuniki.Application.Models.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Nome")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Informe o usuario")]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe a senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
