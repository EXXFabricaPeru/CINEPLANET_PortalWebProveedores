using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WebProov_API.Models
{
    public class SocioDeNegocio
    {
        public string CardCode { get; set; }
        [Required]
        [StringRange(ValoresValidos = new[] { "C", "P" }, MensajeError = "El tipo de cliente no válido. C: Clientes; P: Proveedores")]
        public string CardType { get; set; }
        [StringRange(ValoresValidos = new[] { "100", "102" }, MensajeError = "100: Cliente Nacional; 102: Cliente Extranjero")]
        public int GroupCode { get; set; }
        public string GroupName { get; set; }
        [Required]
        [StringRange(ValoresValidos = new[] { "TPN", "TPJ", "SND" }, MensajeError = "El tipo de persona no válido. TPJ: Jurídica; TPN: Natural; SND: Sujeto No Domiciliado")]

        public string CardName { get; set; }
        [Required]
        public string LicTradNum { get; set; }
        [Required]
        public string Currency { get; set; }
        public int PayTermsGrpCode { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Cellular { get; set; }
        public string EmailAddress { get; set; }
        public string SalesPerson { get; set; }
        public string MainDirection { get; set; }
        public string Contacto { get; set; }
        public string ContactoPhone { get; set; }
        public double CreditLine { get; set; }
        public double SaldoDisponible { get; set; }
        public double DeudaALaFecha { get; set; }
        public string FormaPago { get; set; }
        public string U_EXX_TIPODOCU { get; set; }
        public string U_EXX_ESTCONTR { get; set; }
        public string U_EXX_CNDCONTR { get; set; }
        public string U_EXX_TIPOPERS { get; set; }//Tipo Persona
        public string Nombre { get; set; }
        public string SegundoNombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public List<Direccion> Direcciones { get; set; }
        public List<BranchAssignment> BranchAssignments { get; set; }

        public SocioDeNegocio()
        {
            Direcciones = new List<Direccion>();
            BranchAssignments = new List<BranchAssignment>();
        }
    }

    public class Direccion
    {
        [Required]
        [StringRange(ValoresValidos = new[] { "B", "S" }, MensajeError = "Tipo de dirección no válida. B: Fiscal; S: Envio")]
        public string AdressType { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Departamento { get; set; }
        [Required]
        public string Provincia { get; set; }
        [Required]
        public string Distrito { get; set; }
        [Required]
        public string DireccionDesc { get; set; }
    }
    public class BranchAssignment
    {
        [Required]
        public int BPLID { get; set; }
    }


    public class StringRangeAttribute : ValidationAttribute
    {
        public string[] ValoresValidos { get; set; }
        public string MensajeError { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (ValoresValidos?.Contains(value?.ToString()) == true)
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(MensajeError);
        }
    }

}
