namespace CuentasAhorro.Application.ViewModels
{
    public class TransaccionViewModel
    {
        public string TransaccionID { get; set; }
        public int TipoTransaccionID { get; set; }
        public string TipoTransaccion { get; set; }
        public int CuentaID { get; set; }
        public DateTime FechaOperacion { get; set; }
        public decimal Monto { get; set; }
        public string UsuarioRealizoId { get; set; }
    }
}