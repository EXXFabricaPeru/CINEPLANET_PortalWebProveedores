using System;
using System.Collections.Generic;
using System.Text;

namespace EFHBlazzer.Shared.Entities
{
    public class CtHr_MultiList
    {
        public string NroOrden { get; set; }
        public string Cliente { get; set; }
        public string Medida { get; set; }
        public string Impresion { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public List<GenericEnt> Etapas { get; set; }
        public List<GenericEnt> Maquinas { get; set; }
        public List<GenericEnt> Eventos { get; set; }
        public List<CtrHra_Parada> Paradas { get; set; }
        public List<CtrHra_Detail> Detalles { get; set; }
    }
}
