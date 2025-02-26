using System;

namespace EFHBlazzer.Shared.Entities
{
    public class Factura_Lista
    {
        public int docEntry { get; set; }
        public string docNum { get; set; }
        public string condicionPago { get; set; }
        public string tipoDocumento { get; set; }
        public string folioPref { get; set; }
        public int folioNum { get; set; }
        public DateTime docDate { get; set; }
        public DateTime docDueDate { get; set; }

        public double docTotal { get; set; }
        public double docTotalFC { get; set; }
        public double paidToDate { get; set; }
        public double paidToDateFC { get; set; }

        public double saldo { get; set; }
        public double saldoFC { get; set; }

        //public string Comments { get; set; }
        public int atraso { get; set; }
        public string docCur { get; set; }

    }
}
