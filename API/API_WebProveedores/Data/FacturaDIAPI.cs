using SAPbobsCOM;
using WebProov_API.Data.Interfaces;
using WebProov_API.Models;
using System;
using System.Collections.Generic;
using WebProov_API.Util;

namespace WebProov_API.Data
{
    public class FacturaDIAPI : IFacturaRepository
    {
        private Company _company;
        Documents oPor, oOrd;
        public FacturaDIAPI()
        {
            _company = DIAPIConexion.GetDIAPIConexion();
        }


        public List<Documento> GetListaFechaPagoByRuc(string ruc)
        {
            try
            {
                Documento oFact = new Documento();
                List<Documento> listFact = new List<Documento>();
                Recordset oRS = _company.GetBusinessObject(BoObjectTypes.BoRecordset);
                oRS.DoQuery(Queries.GetFacturasOrdeByDateByRUC(ruc));
                if (oRS.RecordCount == 0)
                    return null;
                for (int i = 0; i < oRS.RecordCount; i++)
                {
                    oFact = new Documento();
                    oFact.DocEntry = oRS.Fields.Item("DocEntry").Value;
                    oFact.DocNum = oRS.Fields.Item("DocNum").Value.ToString().Trim();
                    oFact.DocDate = DateTime.Parse(oRS.Fields.Item("DocDate").Value.ToString().Trim());
                    oFact.DocDueDate = DateTime.Parse(oRS.Fields.Item("DocDueDate").Value.ToString().Trim());
                    oFact.DocCur = oRS.Fields.Item("DocCur").Value.ToString().Trim();
                    oFact.TipoDocumento = oRS.Fields.Item("DocSubType").Value;
                    oFact.CondicionPago = oRS.Fields.Item("PymntGroup").Value;
                    oFact.FolioPref = oRS.Fields.Item("FolioPref").Value;
                    oFact.FolioNum = oRS.Fields.Item("FolioNum").Value;
                    oFact.DocTotal = oRS.Fields.Item("DocTotal").Value;
                    oFact.DocTotalFC = oRS.Fields.Item("DocTotalFC").Value;
                    oFact.PaidToDate = oRS.Fields.Item("PaidToDate").Value;
                    oFact.PaidToDateFC = oRS.Fields.Item("PaidFC").Value;
                    oFact.Saldo = oFact.DocTotal - oFact.PaidToDate;
                    oFact.SaldoFC = oFact.DocTotalFC - oFact.PaidToDateFC;
                    oFact.DocStatus = (oFact.Saldo <= 0) ? "Pagado" : oRS.Fields.Item("Estado").Value;
                    oFact.Atraso = (oFact.Saldo > 0 && oFact.DocDueDate < DateTime.Now) ? (int)(DateTime.Now - (DateTime)oFact.DocDueDate).TotalDays : 0;
                    oRS.MoveNext();
                    listFact.Add(oFact);

                }
                return listFact;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public static string GetOPQTDocNumByEntry(string entry)
        {
            string val = DIAPIConexion.GenericQuery(Queries.GetOPQTDocNum(entry));
            return val;
        }
        public static string GetDescCondicionPago(int code)
        {
            string val = DIAPIConexion.GenericQuery(Queries.GetDescCondicionPago(code));
            return val;
        }

        public Documento GetDocumentoById(int id)
        {
            try
            {
                Documento pedido = new Documento();

                oPor = _company.GetBusinessObject(BoObjectTypes.oPurchaseOrders);
                if (!oPor.GetByKey(id))
                    return null;

                pedido.DocEntry = oPor.DocEntry;
                pedido.DocNum = oPor.DocNum.ToString();
                pedido.CardCode = oPor.CardCode;
                pedido.CardName = oPor.CardName;
                pedido.LicTradNum = oPor.FederalTaxID;
                pedido.NumAtCard = oPor.NumAtCard;
                pedido.DocDate = oPor.DocDate;
                pedido.DocDueDate = oPor.DocDueDate;
                pedido.GroupNum = oPor.GroupNumber;
                string estado = "";
                switch (oPor.DocumentStatus)
                {
                    case BoStatus.bost_Open:
                        estado = "Abierto";
                        break;
                    case BoStatus.bost_Close:
                        estado = "Cerradp";
                        break;
                    case BoStatus.bost_Paid:
                        estado = "Pagado";
                        break;
                    case BoStatus.bost_Delivered:
                        estado = "Entregado";
                        break;
                    default:
                        break;
                }
                pedido.DocStatus = estado;
                pedido.ListaPrecio = "";//??
                pedido.DireccionDespacho = oPor.Address2;
                pedido.DireccionFiscal = oPor.Address;
                pedido.CondicionPago = GetDescCondicionPago(oPor.GroupNumber);//TODO
                pedido.SalesPersonCode = oPor.SalesPersonCode.ToString(); ;
                pedido.DiscountPercent = oPor.DiscountPercent;
                pedido.DiscSum = oPor.TotalDiscount;
                pedido.VatSum = oPor.VatSum;
                pedido.VatSumFC = oPor.VatSumFc;
                pedido.DocTotal = oPor.DocTotal;
                pedido.DocTotalFC = oPor.DocTotalFc;
                pedido.DocRate = oPor.DocRate;
                pedido.Comments = oPor.Comments;
                pedido.DocCur = oPor.DocCurrency;

                List<DetallePedido> detalle = new List<DetallePedido>();
                for (int i = 0; i < oPor.Lines.Count; i++)
                {
                    oPor.Lines.SetCurrentLine(i);
                    DetallePedido det = new DetallePedido();
                    det.LineNum = oPor.Lines.LineNum;
                    det.ItemCode = oPor.Lines.ItemCode;
                    det.UnitMsr = oPor.Lines.MeasureUnit;
                    det.Quantity = oPor.Lines.Quantity;
                    det.Dscription = oPor.Lines.ItemDescription;
                    det.DiscountPercent = oPor.Lines.DiscountPercent;
                    det.Price = oPor.Lines.Price;
                    det.TaxCode = oPor.Lines.TaxCode;
                    det.PriceAfVAT = oPor.Lines.PriceAfterVAT;
                    det.LineTotal = oPor.Lines.LineTotal;
                    det.WhsCode = oPor.Lines.WarehouseCode;
                    detalle.Add(det);
                    pedido.DetallePedido = detalle;
                    if (oPor.Lines.BaseType == 540000006)
                    {
                        pedido.IdCotizacion = oPor.Lines.BaseEntry.ToString();
                        pedido.NumeroCotizacion = GetOPQTDocNumByEntry(oPor.Lines.BaseEntry.ToString());
                    }
                }
                return pedido;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ActualizarDocumento(Documento oferta)
        {
            try
            {
                oPor = _company.GetBusinessObject(BoObjectTypes.oQuotations);
                if (!oPor.GetByKey(oferta.DocEntry))
                    throw new System.Exception("La oferta con id: " + oferta.DocEntry + " no existe en la sociedad.");

                if (oPor.DocumentStatus != BoStatus.bost_Open)
                    throw new System.Exception("La oferta con id: " + oferta.DocEntry + " se encuentra cerrada.");
                //if (oQuot.UserFields.Fields.Item("U_EXS_APROB").Value == "A" && oferta.U_SCO_ESTADO == "-")
                //    throw new System.Exception("La oferta no puede retroceder luego de aprobada.");
                //switch (oferta.U_SCO_ESTADO)
                //{
                //    case "R":
                //        if (oQuot.Close() != 0)
                //            throw new System.Exception(_company.GetLastErrorDescription());

                //        return true;
                //    case "-":
                //        for (int a = 0; a < oQuot.Lines.Count; a++)
                //        {
                //            oQuot.Lines.SetCurrentLine(a);
                //            oQuot.Lines.Delete();
                //        }
                //        break;
                //}
                OQUT(oferta);
                int i = 0;
                foreach (DetalleOferta det in oferta.DetalleOferta)
                {
                    if (oPor.Lines.Count <= det.LineNum)
                        oPor.Lines.Add();

                    oPor.Lines.SetCurrentLine(det.LineNum);
                    if (oPor.Lines.LineStatus == BoStatus.bost_Open)
                        QUT1(det);
                    i++;
                }
                if (oPor.Update() != 0)
                    throw new System.Exception(_company.GetLastErrorDescription());

                //if (!string.IsNullOrEmpty(oferta.Project) && oferta.U_SCO_ESTADO == "A")
                //    OfertaToOrder(oferta);

                return true;
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oPor);
            }
        }

        public int CrearDocument(Documento oferta)
        {
            try
            {
                oPor = _company.GetBusinessObject(BoObjectTypes.oQuotations);
                OQUT(oferta);
                foreach (DetalleOferta det in oferta.DetalleOferta)
                {
                    QUT1(det);
                    oPor.Lines.Add();
                }
                if (oPor.Add() != 0)
                    throw new System.Exception(_company.GetLastErrorDescription());

                string offerKey = "";
                _company.GetNewObjectCode(out offerKey);
                oferta.DocEntry = int.Parse(offerKey);

                //if (!string.IsNullOrEmpty(oferta.Project) && oferta.U_SCO_ESTADO == "A")
                //{
                //    oQuot.GetByKey(oferta.DocEntry);
                //    OfertaToOrder(oferta);
                //}

                return oferta.DocEntry;
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oPor);

            }
        }

        private void OfertaToOrder(Documento oferta)
        {
            try
            {
                int count = 0;
                oOrd = _company.GetBusinessObject(BoObjectTypes.oOrders);
                ORDR(oferta);
                int i = 0;
                foreach (DetalleOferta det in oferta.DetalleOferta)
                {
                    oPor.Lines.SetCurrentLine(det.LineNum);
                    if (!string.IsNullOrEmpty(det.Project) && oPor.Lines.LineStatus == BoStatus.bost_Open)
                    {
                        RDR1(det);
                        oOrd.Lines.BaseEntry = oferta.DocEntry;
                        oOrd.Lines.BaseLine = oPor.Lines.LineNum;
                        oOrd.Lines.BaseType = (int)BoObjectTypes.oQuotations;

                        oOrd.Lines.Add();
                        count++;
                    }
                    i++;
                }
                if (count > 0)
                    if (oOrd.Add() != 0)
                        throw new Exception(_company.GetLastErrorDescription());
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oOrd);
            }
        }
        private void OQUT(Documento oferta)
        {
            oPor.CardCode = oferta.CardCode;
            oPor.DocDate = (DateTime)oferta.DocDate;
            oPor.DocDueDate = (DateTime)oferta.DocDueDate;
            oPor.GroupNumber = oferta.GroupNum;
            oPor.Project = oferta.Project;
            oPor.DiscountPercent = oferta.DiscountPercent;
            oPor.DocTotal = oferta.DocTotal;
            oPor.DocCurrency = oferta.DocCur;
            oPor.Comments = oferta.Comments;

        }
        private void QUT1(DetalleOferta detOfer)
        {
            oPor.Lines.ItemCode = detOfer.ItemCode;

            oPor.Lines.WarehouseCode = detOfer.WhsCode;
            oPor.Lines.Quantity = detOfer.Quantity;
            oPor.Lines.TaxCode = detOfer.TaxCode;
            oPor.Lines.DiscountPercent = detOfer.DiscountPercent;
            //oQuot.Lines.Price = detOfer.Price;
            //oQuot.Lines.PriceAfterVAT = detOfer.PriceAfVAT;
            oPor.Lines.ProjectCode = detOfer.Project;
            oPor.Lines.LineTotal = detOfer.LineTotal;
            oPor.Lines.CostingCode = detOfer.OcrCode;
            oPor.Lines.CostingCode2 = detOfer.OcrCode2;
            oPor.Lines.CostingCode3 = detOfer.OcrCode3;
            oPor.Lines.CostingCode4 = detOfer.OcrCode4;
            oPor.Lines.CostingCode5 = detOfer.OcrCode5;
            if (!string.IsNullOrEmpty(detOfer.U_EXX_GRUPODET)) oPor.Lines.UserFields.Fields.Item("U_EXX_GRUPODET").Value = detOfer.U_EXX_GRUPODET;

        }

        private void ORDR(Documento oferta)
        {
            oOrd.CardCode = oferta.CardCode;
            oOrd.DocDate = (DateTime)oferta.DocDate;
            oOrd.DocDueDate = (DateTime)oferta.DocDueDate;

            //oQuot.GroupNumber = oferta.GroupNum;
            //oQuot.Project = oferta.Project;
            //oQuot.DiscountPercent = oferta.DiscountPercent;
            //oOrd.DocTotal = oferta.DocTotal;
            //oQuot.DocCurrency = oferta.DocCur;
            //oQuot.Comments = oferta.Comments;
            //if (!string.IsNullOrEmpty(oferta.U_SCO_NOPERACION)) oQuot.UserFields.Fields.Item("U_SCO_NOPERACION").Value = oferta.U_SCO_NOPERACION;
            //if (!string.IsNullOrEmpty(oferta.U_SCO_ESTADO)) oQuot.UserFields.Fields.Item("U_SCO_ESTADO").Value = oferta.U_SCO_ESTADO;
            //if (!string.IsNullOrEmpty(oferta.U_SCO_MARGEN)) oQuot.UserFields.Fields.Item("U_SCO_MARGEN").Value = oferta.U_SCO_MARGEN;
            //if (!string.IsNullOrEmpty(oferta.U_SCO_NUMEROCOTIZACION)) oQuot.UserFields.Fields.Item("U_SCO_NUMEROCOTIZACION").Value = oferta.U_SCO_NUMEROCOTIZACION;
            //if (oferta.U_SCO_FECHACREACION != null) oQuot.UserFields.Fields.Item("U_SCO_FECHACREACION").Value = oferta.U_SCO_FECHACREACION;
            //if (oferta.U_SCO_FECHAACTUALIZACION != null) oQuot.UserFields.Fields.Item("U_SCO_FECHAACTUALIZACION").Value = oferta.U_SCO_FECHAACTUALIZACION;
            //if (!string.IsNullOrEmpty(oferta.U_SCO_USUARIOCREA)) oQuot.UserFields.Fields.Item("U_SCO_USUARIOCREA").Value = oferta.U_SCO_USUARIOCREA;
            //if (!string.IsNullOrEmpty(oferta.U_SCO_USUARIOACT)) oQuot.UserFields.Fields.Item("U_SCO_USUARIOACT").Value = oferta.U_SCO_USUARIOACT;
            //if (!string.IsNullOrEmpty(oferta.U_SCO_RUBRO1)) oQuot.UserFields.Fields.Item("U_SCO_RUBRO1").Value = oferta.U_SCO_RUBRO1;
            //if (!string.IsNullOrEmpty(oferta.U_SCO_RUCCLIENTE2)) oQuot.UserFields.Fields.Item("U_SCO_RUCCLIENTE2").Value = oferta.U_SCO_RUCCLIENTE2;
            //if (!string.IsNullOrEmpty(oferta.U_SCO_RSCLIENTE2)) oQuot.UserFields.Fields.Item("U_SCO_RSCLIENTE2").Value = oferta.U_SCO_RSCLIENTE2;
            //if (!string.IsNullOrEmpty(oferta.U_SCO_RUBROCF)) oQuot.UserFields.Fields.Item("U_SCO_RUBROCF").Value = oferta.U_SCO_RUBROCF;
            //if (!string.IsNullOrEmpty(oferta.U_SCO_DESCRIPCION)) oQuot.UserFields.Fields.Item("U_SCO_DESCRIPCION").Value = oferta.U_SCO_DESCRIPCION;
            //if (!string.IsNullOrEmpty(oferta.U_SCO_CAMPO1)) oQuot.UserFields.Fields.Item("U_SCO_CAMPO1").Value = oferta.U_SCO_CAMPO1;
            //if (!string.IsNullOrEmpty(oferta.U_SCO_CAMPO2)) oQuot.UserFields.Fields.Item("U_SCO_CAMPO2").Value = oferta.U_SCO_CAMPO2;
            //if (!string.IsNullOrEmpty(oferta.U_SCO_CAMPO3)) oQuot.UserFields.Fields.Item("U_SCO_CAMPO3").Value = oferta.U_SCO_CAMPO3;
            //if (!string.IsNullOrEmpty(oferta.U_SCO_CAMPO4)) oQuot.UserFields.Fields.Item("U_SCO_CAMPO4").Value = oferta.U_SCO_CAMPO4;
            //if (!string.IsNullOrEmpty(oferta.U_SCO_CAMPO5)) oQuot.UserFields.Fields.Item("U_SCO_CAMPO5").Value = oferta.U_SCO_CAMPO5;
        }
        private void RDR1(DetalleOferta detOfer)
        {
            oOrd.Lines.ItemCode = detOfer.ItemCode;
            oOrd.Lines.UserFields.Fields.Item("U_EXX_GRUPOPER").Value = "0000";
            oOrd.Lines.LineTotal = detOfer.LineTotal;
            //oQuot.Lines.FreeText = detOfer.Text;
            //oQuot.Lines.WarehouseCode = detOfer.WhsCode;
            //oQuot.Lines.Quantity = detOfer.Quantity;
            //oQuot.Lines.TaxCode = detOfer.TaxCode;
            //oQuot.Lines.Price = detOfer.Price;
            //oQuot.Lines.PriceAfterVAT = detOfer.PriceAfVAT;
            //oQuot.Lines.DiscountPercent = detOfer.DISCPRCNT;
            //oQuot.Lines.CostingCode = detOfer.OcrCode;
            //oQuot.Lines.CostingCode3 = detOfer.OcrCode3;
            //if (!string.IsNullOrEmpty(detOfer.U_EXX_GRUPODET)) oQuot.Lines.UserFields.Fields.Item("U_EXX_GRUPODET").Value = detOfer.U_EXX_GRUPODET;
            //if (!string.IsNullOrEmpty(detOfer.U_SCO_NUMEROCOTIZACION)) oQuot.Lines.UserFields.Fields.Item("U_SCO_NUMEROCOTIZACION").Value = detOfer.U_SCO_NUMEROCOTIZACION;
            //if (!string.IsNullOrEmpty(detOfer.U_SCO_ORIGEN)) oQuot.Lines.UserFields.Fields.Item("U_SCO_ORIGEN").Value = detOfer.U_SCO_ORIGEN;
            //if (!string.IsNullOrEmpty(detOfer.U_SCO_DESTINO)) oQuot.Lines.UserFields.Fields.Item("U_SCO_DESTINO").Value = detOfer.U_SCO_DESTINO;
            //if (!string.IsNullOrEmpty(detOfer.U_SCO_CAMPO1)) oQuot.Lines.UserFields.Fields.Item("U_SCO_CAMPO1").Value = detOfer.U_SCO_CAMPO1;
            //if (!string.IsNullOrEmpty(detOfer.U_SCO_CAMPO2)) oQuot.Lines.UserFields.Fields.Item("U_SCO_CAMPO2").Value = detOfer.U_SCO_CAMPO2;
            //if (!string.IsNullOrEmpty(detOfer.U_SCO_CAMPO3)) oQuot.Lines.UserFields.Fields.Item("U_SCO_CAMPO3").Value = detOfer.U_SCO_CAMPO3;
            //if (!string.IsNullOrEmpty(detOfer.U_SCO_CAMPO4)) oQuot.Lines.UserFields.Fields.Item("U_SCO_CAMPO4").Value = detOfer.U_SCO_CAMPO4;
            //if (!string.IsNullOrEmpty(detOfer.U_SCO_CAMPO5)) oQuot.Lines.UserFields.Fields.Item("U_SCO_CAMPO5").Value = detOfer.U_SCO_CAMPO5;
        }


    }
}