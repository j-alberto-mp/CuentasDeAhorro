using CuentasAhorro.Identity.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuentasAhorro.Data.Models
{
    [Table("Transacciones", Schema = "ahorro")]
    public class Transaccion
    {
        public Transaccion()
        {
            TransaccionID = Guid.NewGuid().ToString();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string TransaccionID { get; set; }
        public int TipoTransaccionID { get; set; }
        public int CuentaID { get; set; }
        public DateTime FechaOperacion { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Monto { get; set; }
        public string UsuarioRealizoId { get; set; }


        [ForeignKey("TipoTransaccionID")]
        public virtual TipoTransaccion TipoTransaccion { get; set; }

        [ForeignKey("CuentaID")]
        public virtual Cuenta Cuenta { get; set; }

        [ForeignKey("UsuarioRealizoId")]
        public virtual Usuario UsuarioRealizo { get; set; }
    }
}
