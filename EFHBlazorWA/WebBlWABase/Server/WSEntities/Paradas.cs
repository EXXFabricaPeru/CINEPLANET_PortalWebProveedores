namespace EFHBlazzer.Server.WSEntities
{

    public class TipoParadasResult
    {
        public Paradas[] TipoParadas { get; set; }
    }
    public class Paradas
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }   
        public bool ParadaConsiderada { get; set; }
    }
}