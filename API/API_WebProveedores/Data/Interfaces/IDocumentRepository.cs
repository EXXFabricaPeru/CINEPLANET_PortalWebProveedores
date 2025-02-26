using WebProov_API.Models;

namespace WebProov_API.Data.Interfaces
{
    public interface IDocumentRepository
    {
        public int CrearDocument(Documento doc);
        public bool ActualizarDocumento(Documento doc);
        public Documento GetDocumentoById(int id);
    }
}