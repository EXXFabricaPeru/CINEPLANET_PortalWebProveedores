using WebProov_API.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebProov_API.Dtos
{
    public class SocioDeNegocioCreate
    {
        //public string Codigo { get; set; }
        [Required]
        [StringRange(ValoresValidos = new[] { "C", "P" }, MensajeError = "Tipo de cliente no válido. C: Clientes; P: Proveedores")]
        public string CardType { get; set; }
        [StringRange(ValoresValidos = new[] { "100", "102" }, MensajeError = "100: Cliente Nacional; 102: Cliente Extranjero")]
        public int GroupCode { get; set; }
        public string U_EXX_TIPOPERS { get; set; }
        public string Nombre { get; set; }
        public string SegundoNombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string CardName { get; set; }
        [StringRange(ValoresValidos = new[] { "0", "1", "4","6", "7", "A" }, MensajeError = "Tipo de documento no válido. 0: Otros; 1: DNI; 4: C.E.; 6: RUC ; 7: Pasaporte ; A: Cédula diplomática")]
        public string U_EXX_TIPODOCU { get; set; }
        public string LicTradNum { get; set; }
        public string Currency { get; set; }
        public int PayTermsGrpCode { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Cellular { get; set; }
        public string U_EXX_ESTCONTR { get; set; }
        public string U_EXX_CNDCONTR { get; set; }
        public int U_SCO_IDCLIENTE { get; set; }

        public string EmailAddress { get; set; }

        public IEnumerable<Direccion> Direcciones { get; set; }
        public IEnumerable<BranchAssignment> BranchAssignments { get; set; }
    }
}
