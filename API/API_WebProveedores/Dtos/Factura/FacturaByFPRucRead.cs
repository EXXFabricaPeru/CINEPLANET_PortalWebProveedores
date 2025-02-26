using System;


namespace WebProov_API.Dtos
{
    public class FacturaByFPRucRead
    {
        public int DocEntry { get; set; }
        public string DocNum { get; set; }
        public string CondicionPago { get; set; }
        public string TipoDocumento { get; set; }
        public string FolioPref { get; set; }
        public int FolioNum { get; set; }
        public string DocStatus { get; set; }
        public DateTime? DocDate { get; set; }
        public DateTime? DocDueDate { get; set; }

        public double DocTotal { get; set; }
        public double DocTotalFC { get; set; }
        public double PaidToDate { get; set; }
        public double PaidToDateFC { get; set; }

        public double Saldo { get; set; }
        public double SaldoFC { get; set; }

        //public string Comments { get; set; }
        public int Atraso { get; set; }
        public string DocCur { get; set; }


    }
}
