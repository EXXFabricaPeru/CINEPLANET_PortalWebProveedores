using System;
using System.Collections.Generic;
using System.Text;

namespace EFHBlazzer.Shared.Entities
{
    public class Bal_MultiList
    {
       public string NroOrden { get; set; }
       public string Cliente { get; set; }
       public string Medida { get; set; }
       public string Impresion { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public List<GenericEnt> Etapas { get; set; }
        public List<GenericEnt> Maquinas { get; set; }
        public List<GenericEnt> Articulos { get; set; }
        public List<Bal_Det> Detalles { get; set; }
    }
}
