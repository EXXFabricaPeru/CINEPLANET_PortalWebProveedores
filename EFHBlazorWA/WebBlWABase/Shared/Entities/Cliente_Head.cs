using System;
using System.Collections.Generic;
using System.Text;

namespace EFHBlazzer.Shared.Entities
{
    public class Cliente_Head
    {
        public string busqueda { get; set; }
        public string cardCode { get; set; }
        public string cardType { get; set; }
        public string groupName { get; set; }
        public string cardName { get; set; }
        public string licTradNum { get; set; }
        public string currency { get; set; }
        public string phone1 { get; set; }
        public string phone2 { get; set; }
        public string cellular { get; set; }
        public string emailAddress { get; set; }
        public string salesPerson { get; set; }
        public string mainDirection { get; set; }
        public string contacto { get; set; }
        public string contactoPhone { get; set; }
        public decimal creditLine { get; set; }
        public decimal saldoDisponible { get; set; }
        public decimal deudaALaFecha { get; set; }
        public string formaPago { get; set; }
    }
}
