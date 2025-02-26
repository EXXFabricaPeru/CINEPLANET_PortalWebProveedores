namespace EFHBlazzer.Server.WSEntities
{

    public class UnidadesAlternativasResult
    {
        public Unidades[] UnidadesAlternativas { get; set; }
    }
    public class Unidades
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }   
    }
}
