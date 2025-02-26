namespace EFHBlazzer.Server.WSEntities
{
    public class AuthUser
    {

        public string IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Token { get; set; }
        public string Rol { get; set; }
        public string AUTH_B_Peso { get; set; }
        public string AUTH_B_Del { get; set; }
        public string AUTH_B_DelPE { get; set; }
        public string AUTH_B_Part { get; set; }
        public string AUTH_B_Cali { get; set; }
        public string AUTH_B_ESap { get; set; }
        public string AUTH_C_ESap { get; set; }

        public string NombreApellido { get { return Nombre + " " + Apellido; } set { } }

    }
}
