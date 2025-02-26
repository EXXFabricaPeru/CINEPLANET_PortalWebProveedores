using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFHBlazzer.Server.WSEntities
{
    public class ControlHoraList
    {
        public Class1[] Listado { get; set; }

    }

    public class Class1
    {
        public string Codigo { get; set; }
        public string IdEmpleado { get; set; }
        public int OrdenFabricacion { get; set; }
        public string Etapa { get; set; }
        public string Recurso { get; set; }
        public object Tipo { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; }
        public object Origen { get; set; }
        public string Estado { get; set; }
    }
}
