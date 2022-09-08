﻿using CuentasAhorro.Identity.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuentasAhorro.Data.Models
{
    [Table("Clientes", Schema = "ahorro")]
    public class Cliente
    {
        public Cliente()
        {
            Cuentas = new HashSet<Cuenta>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClienteID { get; set; }
        [MaxLength(30)]
        public string Nombre { get; set; }
        [MaxLength(70)]
        public string ApellidoPaterno { get; set; }
        [MaxLength(70)]
        public string ApellidoMaterno { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string UsuarioAltaId { get; set; }

        [ForeignKey("UsuarioAltaId")]
        public Usuario UsuarioAlta { get; set; }

        public ICollection<Cuenta> Cuentas { get; set; }
    }
}