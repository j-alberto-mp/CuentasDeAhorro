using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuentasAhorro.Identity.Models
{
    public class Usuario : IdentityUser
    {
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaCreacion { get; set; }

        [NotMapped]
        public string NombreCompleto { get { return $"{Nombre} {ApellidoPaterno} {ApellidoMaterno}"; } }
    }
}