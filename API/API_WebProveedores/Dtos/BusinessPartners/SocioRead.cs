using WebProov_API.Models;
using System.Collections.Generic;

namespace WebProov_API.Dtos
{
    public class SocioRead
    {
        public string CardCode { get; set; }
        public string CardType { get; set; }

        public string GroupName { get; set; }

        public string CardName { get; set; }
        public string LicTradNum { get; set; }
        public string Currency { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Cellular { get; set; }
        public string EmailAddress { get; set; }
        public string SalesPerson { get; set; }
        public string MainDirection { get; set; }
        public string Contacto { get; set; }
        public string ContactoPhone { get; set; }
        public double CreditLine { get; set; }
        public double SaldoDisponible { get; set; }
        public double DeudaALaFecha { get; set; }
        public string FormaPago { get; set; }


        //public IEnumerable<Direccion> Direcciones { get; set; }
    }
}
