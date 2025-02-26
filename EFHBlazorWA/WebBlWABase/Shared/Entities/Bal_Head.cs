using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFHBlazzer.Shared.Entities
{
    public class Bal_Head
    {
        public string Empleado { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Ingrese valor numerico")]
        public string Serie { get; set; }
        public string DocNum { get; set; }
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
        public string MedidaMan { get; set; }
        public string EspesorMan { get; set; }
        public string RutaDestino { get; set; }
        public string Cliente { get; set; }
        public string Medida { get; set; }
        public string Impresion { get; set; }
        public string FinalItemCode { get; set; }
        public string FinalItemName { get; set; }
        public string Lote { get; set; }
        public string TipoMaterial { get; set; }
    }
}
