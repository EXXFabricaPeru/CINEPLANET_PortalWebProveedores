using System;
using System.ComponentModel.DataAnnotations;

namespace EFHBlazzer.Shared.Entities
{
    public class Bal_Det
    {

        public string Empleado { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Ingrese valor numerico")]
        public string NroOrden { get; set; }
        public string Etapa { get; set; }
        public string RecursoMaquina { get; set; }
        public string DescRecurso { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Unit { get; set; }
        public string AltUnit { get; set; }
        public decimal Tara { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PesoBruto { get; set; }
        public decimal PesoNeto { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime Hora { get; set; }
        public string codigoInterno { get; set; }
        public string Numero { get; set; }
        public string tipo { get; set; }
        public string material { get; set; }
        public string destinoMan { get; set; }
        public string espesorMan { get; set; }
        public string medidaMan { get; set; }
        public string lote { get; set; }
        public string reciboProduccion { get; set; }
        public string parteEntrega { get; set; }
        public string status { get; set; }
        public string aproCalidad { get; set; }
    }
}
