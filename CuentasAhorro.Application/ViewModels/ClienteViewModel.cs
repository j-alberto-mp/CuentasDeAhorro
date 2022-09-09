namespace CuentasAhorro.Application.ViewModels
{
    public class ClienteViewModel
    {
        public int ClienteID { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string UsuarioAltaId { get; set; }

        public string NombreCompeto { get { return $"{Nombre} {ApellidoPaterno} {ApellidoMaterno}"; } }
    }
}
