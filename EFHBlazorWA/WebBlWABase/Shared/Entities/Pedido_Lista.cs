using System;
using System.Collections.Generic;
using System.Text;

namespace EFHBlazzer.Shared.Entities
{
    public class Pedido_Lista
    {
        public int docEntry { get; set; }
        public string docNum { get; set; }
        //public string CardCode { get; set; }
        //public string CardName { get; set; }
        //public string licTradNum { get; set; }
        public string numAtCard { get; set; }
        public DateTime docDate { get; set; }
        public DateTime docDueDate { get; set; }
        public double docTotal { get; set; }
        public double docTotalFC { get; set; }
        public string docCur { get; set; }
        public string comments { get; set; }
        public string docStatus { get; set; }
        public string SalesPersonCode { get; set; }


    }
}
