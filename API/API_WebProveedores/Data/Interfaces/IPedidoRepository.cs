
using System.Collections.Generic;
using WebProov_API.Models;

namespace WebProov_API.Data.Interfaces
{
    public interface IPedidoRepository : IDocumentRepository
    {        public List<Documento> GetListaByRuc(string ruc);

    }
}