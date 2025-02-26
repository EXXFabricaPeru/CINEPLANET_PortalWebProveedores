namespace WebProov_API.Models
{
    public class ErrMsj
    {
        public ErrMsj(string docEntry, string mensaje)
        {
            this.docEntry = docEntry;
            this.mensaje = mensaje;
        }

        public string docEntry { get; set; }
        public string mensaje { get; set; }

        public static ErrMsj GetErr(string docEntry, string mensaje)
        {
            return new(docEntry, mensaje);
        }
    }
}
