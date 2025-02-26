namespace EFHBlazzer.Server.WSEntities
{

    public class MaterialesResult
    {
        public Materiales[] Materiales { get; set; }
    }
    public class Materiales
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }   
    }
}
