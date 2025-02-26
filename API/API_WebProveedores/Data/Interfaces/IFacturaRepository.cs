
using System.Collections.Generic;
using WebProov_API.Models;

namespace WebProov_API.Data.Interfaces
{
    public interface IFacturaRepository : IDocumentRepository
    {        public List<Documento> GetListaFechaPagoByRuc(string ruc);

    }
}