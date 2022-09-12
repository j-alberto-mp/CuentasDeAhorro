namespace CuentasAhorro.Application.ViewModels
{
    public class ClienteViewModel
    {
        public int ClienteID { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public DateTime FechaRegistro { get; set; }

        public string NombreCompleto { get { return $"{Nombre} {ApellidoPaterno} {ApellidoMaterno}"; } }
    }
}
