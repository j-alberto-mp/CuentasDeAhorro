using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuentasAhorro.Data.Models
{
    [Table("TipoTransacciones", Schema = "ahorro")]
    public class TipoTransaccion
    {
        public TipoTransaccion()
        {
            Transacciones = new HashSet<Transaccion>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TipoTransaccionID { get; set; }
        public string Tipo { get; set; }

        public virtual ICollection<Transaccion> Transacciones { get; set; }
    }
}