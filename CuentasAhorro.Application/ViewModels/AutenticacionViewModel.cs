using System.ComponentModel.DataAnnotations;

namespace CuentasAhorro.Application.ViewModels
{
    public class AutenticacionViewModel
    {
        [Required]
        [Display(Name = "Usuario")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
    }
}