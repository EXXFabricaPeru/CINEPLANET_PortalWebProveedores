using SAPbobsCOM;
using WebProov_API.Data.Interfaces;
using WebProov_API.Models;
using WebProov_API.Util;

namespace WebProov_API.Data
{
    public class BusinessPartnerDIAPI : IBusinessPartnerRepository
    {
        private Company _company;
        BusinessPartners oBp;
        public BusinessPartnerDIAPI()
        {
            _company = DIAPIConexion.GetDIAPIConexion();
        }
        public SocioDeNegocio GetSocioByCardCode(string id)
        {
            try
            {
                SocioDeNegocio socio = new SocioDeNegocio();
                Recordset oRS = _company.GetBusinessObject(BoObjectTypes.BoRecordset);
                oRS.DoQuery(Queries.GetBPbyCardCode(id));
                if (oRS.RecordCount == 0)
                    return null;

                socio.CardCode = oRS.Fields.Item("CardCode").Value.ToString().Trim();
                socio.CardName = oRS.Fields.Item("CardName").Value.ToString().Trim();
                socio.CardType = oRS.Fields.Item("CardType").Value.ToString().Trim();
                socio.LicTradNum = oRS.Fields.Item("LicTradNum").Value.ToString().Trim();
                socio.GroupName = oRS.Fields.Item("GroupCode").Value.ToString().Trim();
                socio.Phone1 = oRS.Fields.Item("Phone1").Value.ToString().Trim();
                socio.Cellular = oRS.Fields.Item("Cellular").Value.ToString().Trim();
                socio.EmailAddress = oRS.Fields.Item("EmailAddress").Value.ToString().Trim();
                socio.Currency = oRS.Fields.Item("Currency").Value.ToString().Trim();
                socio.SalesPerson = oRS.Fields.Item("SalesPerson").Value.ToString().Trim();
                socio.MainDirection = oRS.Fields.Item("MainDirection").Value.ToString().Trim();
                socio.Contacto = oRS.Fields.Item("Contacto").Value.ToString().Trim();
                socio.ContactoPhone = oRS.Fields.Item("ContactoPhone").Value.ToString().Trim();
                socio.CreditLine = double.Parse(oRS.Fields.Item("CreditLine").Value.ToString());
                socio.SaldoDisponible = double.Parse(oRS.Fields.Item("SaldoDisponible").Value.ToString());
                socio.DeudaALaFecha = double.Parse(oRS.Fields.Item("DeudaALaFecha").Value.ToString());
                socio.FormaPago = oRS.Fields.Item("FormaPago").Value.ToString();

                return socio;
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public SocioDeNegocio GetSocioByRUCRaz(string id)
        {
            try
            {
                SocioDeNegocio socio = new SocioDeNegocio();
                Recordset oRS = _company.GetBusinessObject(BoObjectTypes.BoRecordset);
                oRS.DoQuery(Queries.GetBPbyRUCRazSoc(id));
                if (oRS.RecordCount == 0)
                    return null;
                
                socio.CardCode = oRS.Fields.Item("CardCode").Value.ToString().Trim();
                socio.CardName = oRS.Fields.Item("CardName").Value.ToString().Trim();
                socio.CardType = oRS.Fields.Item("CardType").Value.ToString().Trim();
                socio.LicTradNum = oRS.Fields.Item("LicTradNum").Value.ToString().Trim();
                 socio.GroupName = oRS.Fields.Item("GroupCode").Value.ToString().Trim();
                socio.Phone1 = oRS.Fields.Item("Phone1").Value.ToString().Trim();
                socio.Cellular = oRS.Fields.Item("Cellular").Value.ToString().Trim();
                socio.EmailAddress = oRS.Fields.Item("EmailAddress").Value.ToString().Trim();
                socio.Currency = oRS.Fields.Item("Currency").Value.ToString().Trim();
                socio.SalesPerson = oRS.Fields.Item("SalesPerson").Value.ToString().Trim();
                socio.MainDirection = oRS.Fields.Item("MainDirection").Value.ToString().Trim();
                socio.Contacto = oRS.Fields.Item("Contacto").Value.ToString().Trim();
                socio.ContactoPhone = oRS.Fields.Item("ContactoPhone").Value.ToString().Trim();
                socio.CreditLine = double.Parse(oRS.Fields.Item("CreditLine").Value.ToString());
                socio.SaldoDisponible = double.Parse(oRS.Fields.Item("SaldoDisponible").Value.ToString());
                socio.DeudaALaFecha = double.Parse(oRS.Fields.Item("DeudaALaFecha").Value.ToString());
                socio.FormaPago = oRS.Fields.Item("FormaPago").Value.ToString();

                return socio;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public bool ActualizarSocio(SocioDeNegocio socio)
        {
            try
            {
                oBp = _company.GetBusinessObject(BoObjectTypes.oBusinessPartners);
                if (!oBp.GetByKey(socio.CardCode))
                    throw new System.Exception("El socio con código: " + socio.CardCode + " no existe en la sociedad.");
                OCRD(socio, oBp);
                int i = 0;
                foreach (Direccion direccion in socio.Direcciones)
                {
                    oBp.Addresses.SetCurrentLine(i);
                    if (oBp.Addresses.AddressName == direccion.Address)
                    {
                        CRD1(direccion, oBp);
                    }
                    else
                    {
                        oBp.Addresses.AddressName = direccion.Address;
                        oBp.Addresses.AddressType = direccion.AdressType == "B" ? BoAddressType.bo_BillTo : BoAddressType.bo_ShipTo;
                        CRD1(direccion, oBp);
                        oBp.Addresses.Add();
                    }
                    i++;
                }
                if (oBp.Update() != 0)
                    throw new System.Exception(_company.GetLastErrorDescription());

                return true;
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oBp);
            }
        }

        public bool CrearSocio(SocioDeNegocio socio)
        {
            try
            {
                oBp = _company.GetBusinessObject(BoObjectTypes.oBusinessPartners);
                oBp.CardCode = socio.CardCode;
                OCRD(socio, oBp);
                foreach (Direccion direccion in socio.Direcciones)
                {
                    oBp.Addresses.AddressName = direccion.Address;
                    oBp.Addresses.AddressType = direccion.AdressType == "B" ? BoAddressType.bo_BillTo : BoAddressType.bo_ShipTo;
                    CRD1(direccion, oBp);
                    oBp.Addresses.Add();
                }
                foreach (BranchAssignment sucursal in socio.BranchAssignments)
                {
                    oBp.BPBranchAssignment.BPLID = sucursal.BPLID;
                    oBp.BPBranchAssignment.Add();
                }
                if (oBp.Add() != 0)
                    throw new System.Exception(_company.GetLastErrorDescription());

                return true;
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oBp);
            }
        }

                private void OCRD(SocioDeNegocio socio, BusinessPartners oBp)
        {
            if (socio.U_EXX_TIPOPERS == "TPJ")
                oBp.CardName = socio.CardName;
            else
            {
                oBp.CardName = string.Join(" ", socio.ApellidoPaterno, socio.ApellidoMaterno, socio.Nombre, socio.SegundoNombre);
                oBp.UserFields.Fields.Item("U_EXX_PRIMERNO").Value = socio.Nombre;
                oBp.UserFields.Fields.Item("U_EXX_SEGUNDNO").Value = socio.SegundoNombre;
                oBp.UserFields.Fields.Item("U_EXX_APELLPAT").Value = socio.ApellidoPaterno;
                oBp.UserFields.Fields.Item("U_EXX_APELLMAT").Value = socio.ApellidoMaterno;
            }
            oBp.GroupCode = socio.GroupCode;
            oBp.FederalTaxID = socio.LicTradNum;
            oBp.Currency = socio.Currency;
            oBp.PayTermsGrpCode = socio.PayTermsGrpCode;
            oBp.Phone1 = socio.Phone1;
            oBp.Phone2 = socio.Phone2;
            oBp.Cellular = socio.Cellular;
            oBp.EmailAddress = socio.EmailAddress;

            oBp.UserFields.Fields.Item("U_EXX_TIPOPERS").Value = socio.U_EXX_TIPOPERS;
            oBp.UserFields.Fields.Item("U_EXX_TIPODOCU").Value = socio.U_EXX_TIPODOCU;
            if (!string.IsNullOrEmpty(socio.U_EXX_ESTCONTR)) oBp.UserFields.Fields.Item("U_EXX_ESTCONTR").Value = socio.U_EXX_ESTCONTR;
            if (!string.IsNullOrEmpty(socio.U_EXX_CNDCONTR)) oBp.UserFields.Fields.Item("U_EXX_CNDCONTR").Value = socio.U_EXX_CNDCONTR;
            //if ((socio.U_SCO_IDCLIENTE) != null) oBp.UserFields.Fields.Item("U_SCO_IDCLIENTE").Value = socio.U_SCO_IDCLIENTE;
        }
        private void CRD1(Direccion direccion, BusinessPartners oBp)
        {
            oBp.Addresses.Country = "PE";
            oBp.Addresses.State = direccion.Departamento;
            oBp.Addresses.County = direccion.Provincia;
            oBp.Addresses.ZipCode = direccion.Distrito;
            oBp.Addresses.Street = direccion.DireccionDesc;
        }
    }
}