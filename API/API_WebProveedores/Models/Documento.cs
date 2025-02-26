using System;
using System.Collections.Generic;

namespace WebProov_API.Models
{
    public class Documento
    {
        public int DocEntry { get; set; }
        public string DocNum { get; set; }
        public string CardCode { get; set; }
        public string CardName { get; set; }
        public string LicTradNum { get; set; }
        public string NumAtCard { get; set; }
        public DateTime? DocDate { get; set; }
        public DateTime? DocDueDate { get; set; }
        public string FolioPref { get; set; }
        public int FolioNum { get; set; }
        public string Indicator { get; set; }
        public int GroupNum { get; set; }
        public string SalesPersonCode { get; set; }
        public string DocStatus { get; set; }
        public string ListaPrecio { get; set; }
        public string DireccionDespacho { get; set; }
        public string DireccionFiscal { get; set; }
        public string CondicionPago { get; set; }
        public string TipoDocumento { get; set; }

        public string IdCotizacion { get; set; }
        public string NumeroCotizacion { get; set; }

        public string Project { get; set; }
        public double DiscSum { get; set; }
        public double DiscountPercent { get; set; }
        public double VatSum { get; set; }
        public double VatSumFC { get; set; }
        public double DocTotal { get; set; }
        public double DocTotalFC { get; set; }
        public double PaidToDate { get; set; }
        public double PaidToDateFC { get; set; }
        public string DocCur { get; set; }
        public double DocRate { get; set; }
        public int Atraso { get; set; }
        public double Saldo { get; set; }
        public double SaldoFC { get; set; }
        public string Comments { get; set; }

        public string U_EXX_FE_PUNPAR { get; set; }
        public string U_EXX_FE_PUNLLE { get; set; }
        public string U_EXX_LICCONDU { get; set; }
        public string U_EXX_NOMCONDU { get; set; }




        public List<DetalleOferta> DetalleOferta { get; set; }
        public List<DetalleDelivery> DetalleDelivery { get; set; }
        public List<DetallePedido> DetallePedido { get; set; }
    }

    public class DetalleOferta : DetalleDoc
    {
        public string U_EXX_GRUPODET { get; set; }
        public string OcrCode { get; set; }
        public string OcrCode2 { get; set; }
        public string OcrCode3 { get; set; }
        public string OcrCode4 { get; set; }
        public string OcrCode5 { get; set; }
    }

    public class DetalleDelivery : DetalleDoc
    {

    }

    public class DetallePedido : DetalleDoc
    {
        public double PendQuantity { get; set; }
        public DateTime ShipDate { get; set; }
        public double Stock { get; set; }
    }

    public class DetalleDoc
    {
        public int LineNum { get; set; }
        public string ItemCode { get; set; }
        public string UnitMsr { get; set; }
        public double Quantity { get; set; }
        public string Dscription { get; set; }
        public double DiscountPercent { get; set; }
        public double Price { get; set; }
        public string WhsCode { get; set; }
        public string TaxCode { get; set; }
        public double PriceAfVAT { get; set; }
        public double LineTotal { get; set; }
        public string Project { get; set; }


    }
}
