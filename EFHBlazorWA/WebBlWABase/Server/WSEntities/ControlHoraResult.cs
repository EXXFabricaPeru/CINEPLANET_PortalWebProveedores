using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFHBlazzer.Shared.Entities;

namespace EFHBlazzer.Server.WSEntities
{
    public class ControlHoraResult
    {
        public Controlhora2[] ControlHoras { get; set; }
        public Parada2[] Paradas { get; set; }
    }
}



public class Controlhora2
{
    public string Codigo { get; set; }
    public string IdEmpleado { get; set; }
    public int OrdenFabricacion { get; set; }
    public string Etapa { get; set; }
    public string Recurso { get; set; }
    public string Tipo { get; set; }
    public DateTime Fecha { get; set; }
    public string Hora { get; set; }
    public string Origen { get; set; }
    public object Estado { get; set; }
}

public class Parada2
{
    public string IdEmpleado { get; set; }
    public int OrdenFabricacion { get; set; }
    public string Etapa { get; set; }
    public string Recurso { get; set; }
    public string TipoPara { get; set; }
    public string TipoParaDesc { get; set; }
    public string MotivoPara { get; set; }
    public string Tipo { get; set; }
    public DateTime Fecha { get; set; }
    public string Hora { get; set; }
    public object Origen { get; set; }
}
