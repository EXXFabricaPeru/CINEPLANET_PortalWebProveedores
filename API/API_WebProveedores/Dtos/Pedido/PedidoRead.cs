using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebProov_API.Models;

namespace WebProov_API.Dtos
{
    public class PedidoRead
    {
        public int DocEntry { get; set; }
        public string DocNum { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string LicTradNum { get; set; }
        public string NumAtCard { get; set; }
        public DateTime? DocDate { get; set; }
        public DateTime? DocDueDate { get; set; }
        public int GroupNum { get; set; }
        //public string Project { get; set; }
        public string DocStatus { get; set; }
        public string ListaPrecio { get; set; }
        public string DireccionDespacho { get; set; }
        public string DireccionFiscal { get; set; }
        public string CondicionPago { get; set; }
        public string IdCotizacion { get; set; }
        public string NumeroCotizacion { get; set; }
        public string SalesPersonCode { get; set; }

        public double DiscSum { get; set; }
        public double DiscountPercent { get; set; }
        public double VatSum { get; set; }
        public double VatSumFC { get; set; }
        public double DocTotal { get; set; }
        public double DocTotalFC { get; set; }
        public string DocCur { get; set; }
        public double DocRate { get; set; }
        public string Comments { get; set; }

        public List<DetallePedido> DetallePedido { get; set; }
    }
}
