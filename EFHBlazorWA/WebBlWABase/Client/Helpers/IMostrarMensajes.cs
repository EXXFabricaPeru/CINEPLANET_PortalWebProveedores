using System.Threading.Tasks;

namespace EFHBlazzer.Client.Helpers
{
    public interface IMostrarMensajes
    {
        Task MostrarMensajeError(string mensaje);
        Task MostrarMensajeExitoso(string mensaje);
        Task MostrarMensajeWarning(string mensaje);
    }
}
