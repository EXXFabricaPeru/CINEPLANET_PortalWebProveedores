namespace EFHBlazzer.Server.WSEntities
{
    public class Recurso
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public float CantidadRequerida { get; set; }
        public float CantidadEntregada { get; set; }
        public string UnidadMedida { get; set; }
        public string TipoRecurso { get; set; }
    }
}
