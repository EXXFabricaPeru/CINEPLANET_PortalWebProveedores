using System;
using System.Collections.Generic;
using System.Text;

namespace EFHBlazzer.Shared.Entities
{
    public class CtrHra_Listado
    {
        public string Empleado { get; set; }
        public string NroOrden { get; set; }
        public string Etapa { get; set; }
        public string Maquina { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }
    }
}
