namespace EFHBlazzer.Server.WSEntities
{
    public class Articulo
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public float CantidadPlanificada { get; set; }
        public float CantidadEntregada { get; set; }
        public string UnidadMedida { get; set; }
        public string TipoArticulo { get; set; }
        public Lote[] Lotes { get; set; }
    }

    public class Lote
    {
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public float Cantidad{ get; set; }

    }


}
