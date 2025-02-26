using System;
using System.Collections.Generic;

namespace EFHBlazzer.Shared.Entities
{
    public class Pedido
    {
        public int docEntry { get; set; }
        public string docNum { get; set; }
        public string cardCode { get; set; }
        public string cardName { get; set; }
        public string licTradNum { get; set; }
        public string numAtCard { get; set; }
        public DateTime docDate { get; set; }
        public DateTime docDueDate { get; set; }
        public int groupNum { get; set; }
        //public string Project { get; set; }
        public string docStatus { get; set; }
        public string listaPrecio { get; set; }
        public string direccionDespacho { get; set; }
        public string direccionFiscal { get; set; }
        public string condicionPago { get; set; }
        public string idCotizacion { get; set; }
        public string numeroCotizacion { get; set; }
        public string salesPersonCode { get; set; }

        public double discSum { get; set; }
        public double discountPercent { get; set; }
        public double vatSum { get; set; }
        public double vatSumFC { get; set; }
        public double docTotal { get; set; }
        public double docTotalFC { get; set; }
        public double subTotal { get; set; }
        public double subTotalFC { get; set; }
        public string docCur { get; set; }
        public double docRate { get; set; }
        public string comments { get; set; }

        public List<DetallePedido> detallePedido { get; set; }

    }
}
