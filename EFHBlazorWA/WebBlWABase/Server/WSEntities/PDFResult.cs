using System;


namespace EFHBlazzer.Server.WSEntities
{
    public class PDFResult
    {
        public DateTime FechaProduccion { get; set; }
        public string Hora { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public string CodigoInterno { get; set; }
        public string Tipo { get; set; }
        public string Pedido { get; set; }
        public string OT { get; set; }
        public string OC { get; set; }
        public string Cliente { get; set; }
        public string Producto { get; set; }
        public string Impresión { get; set; }
        public string MedidaFinal { get; set; }
        public string Destino { get; set; }
        public float PesoBruto { get; set; }
        public float Tara { get; set; }
        public string Unidades { get; set; }
        public float PesoNeto { get; set; }
        public string Operario { get; set; }
        public string CodProd { get; set; }
        public string Maquina { get; set; }
        public float Cantidad { get; set; }
    }
}



