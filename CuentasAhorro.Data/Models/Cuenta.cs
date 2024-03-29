﻿using CuentasAhorro.Identity.Models;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuentasAhorro.Data.Models
{
    [Table("Cuentas", Schema = "ahorro")]
    public class Cuenta
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CuentaID { get; set; }
        public int ClienteID { get; set; }
        [MaxLength(16)]
        public string NumeroCuenta { get; set; }
        public DateTime FechaApertura { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Saldo { get; set; }
        public string UsuarioAltaId { get; set; }

        [ForeignKey("ClienteID")]
        public virtual Cliente Cliente { get; set; }

        [ForeignKey("UsuarioAltaId")]
        public virtual Usuario UsuarioAlta { get; set; }

        public virtual ICollection<Transaccion> Transacciones { get; set; }
    }
}
