using System;
using System.Collections.Generic;
using System.Text;

namespace EFHBlazzer.Shared.Entities
{
    public class DetallePedido
    {
        public int lineNum { get; set; }
        public string itemCode { get; set; }
        public string unitMsr { get; set; }

        public double quantity { get; set; }
        public string dscription { get; set; }
        public double discountPercent { get; set; }
        public double price { get; set; }

        public string whsCode { get; set; }
        public string taxCode { get; set; }
        public double priceAfVAT { get; set; }
        public double lineTotal { get; set; }
        public string project { get; set; }
        public double pendQuantity { get; set; }
        public DateTime shipDate { get; set; }
        public double stock { get; set; }
    }
}
