namespace CuentasAhorro.Application.ViewModels
{
    public class CuentaViewModel
    {
        public int CuentaID { get; set; }
        public int ClienteID { get; set; }
        public string NumeroCuenta { get; set; }
        public DateTime FechaApertura { get; set; }
        public decimal Saldo { get; set; }
        public string UsuarioAltaId { get; set; }
    }
}