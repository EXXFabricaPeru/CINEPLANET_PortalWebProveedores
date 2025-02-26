using SAPbobsCOM;
using WebProov_API.Data.Interfaces;
using WebProov_API.Models;
using System;
using System.Collections.Generic;
using WebProov_API.Util;

namespace WebProov_API.Data
{
    public class PedidoDIAPI : IPedidoRepository
    {
        private Company _company;
        Documents oPor;
        public PedidoDIAPI()
        {
            _company = DIAPIConexion.GetDIAPIConexion();
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
                        estado = "Cerrado";
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
                pedido.CondicionPago = GetDescCondicionPago(oPor.PaymentGroupCode);//TODO
                pedido.SalesPersonCode = GetSLPName(oPor.SalesPersonCode.ToString()) ;
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
                    det.WhsCode = GetWHSName(oPor.Lines.WarehouseCode);//
                    det.PendQuantity = oPor.Lines.RequiredQuantity;
                    det.ShipDate = oPor.Lines.ShipDate;
                    det.Stock = oPor.Lines.Quantity;//oPor.Lines.WarehouseCode,oPor.Lines.ItemCode;
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

        public List<Documento> GetListaByRuc(string ruc)
        {
            try
            {
                Documento ped = new Documento();
                List<Documento> listPed = new List<Documento>();
                Recordset oRS = _company.GetBusinessObject(BoObjectTypes.BoRecordset);
                oRS.DoQuery(Queries.GetPedidosByRUC(ruc));
                if (oRS.RecordCount == 0)
                    return null;
                for (int i = 0; i < oRS.RecordCount; i++)
                {
                    ped = new Documento();
                    ped.DocEntry = oRS.Fields.Item("DocEntry").Value;
                    ped.DocNum = oRS.Fields.Item("DocNum").Value.ToString().Trim();
                    ped.NumAtCard = oRS.Fields.Item("NumAtCard").Value.ToString().Trim();
                    ped.DocDate = DateTime.Parse(oRS.Fields.Item("DocDate").Value.ToString().Trim());
                    ped.DocDueDate = DateTime.Parse(oRS.Fields.Item("DocDueDate").Value.ToString().Trim());
                    ped.SalesPersonCode = oRS.Fields.Item("SlpName").Value.ToString().Trim();
                    ped.DocCur = oRS.Fields.Item("DocCur").Value.ToString().Trim();
                    ped.DocTotal = oRS.Fields.Item("DocTotal").Value;
                    ped.DocTotalFC = oRS.Fields.Item("DocTotalFC").Value;
                    ped.Comments = oRS.Fields.Item("Comments").Value.ToString().Trim();
                    ped.DocStatus = oRS.Fields.Item("Estado").Value.ToString().Trim();
                    oRS.MoveNext();
                    listPed.Add(ped);
                }
                return listPed;
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

        private string GetSLPName(string code)
        {
            string val = "-";
            if (code != "-1")
                val = DIAPIConexion.GenericQuery(Queries.GetNombreEmpleado(code));
  
            return val;
        }
        private string GetWHSName(string code)
        {
            string val = code;
            if (code != "-1")
                val = DIAPIConexion.GenericQuery(Queries.GetNombreAlmacen(code));

            return val;
        }

        private double GetStock(string whsCode, string itemCode)
        {
            double val = 0;
      
                val = double.Parse(DIAPIConexion.GenericQuery(Queries.GetStockXAlmacen(whsCode, itemCode)));

            return val;
        }

        public bool ActualizarDocumento(Documento ordCompra)
        {
            try
            {
                oPor = _company.GetBusinessObject(BoObjectTypes.oPurchaseOrders);
                if (!oPor.GetByKey(ordCompra.DocEntry))
                    throw new System.Exception("La oferta con id: " + ordCompra.DocEntry + " no existe en la sociedad.");

                if (oPor.DocumentStatus != BoStatus.bost_Open)
                    throw new System.Exception("La oferta con id: " + ordCompra.DocEntry + " se encuentra cerrada.");
                   OPOR(ordCompra);
                int i = 0;
                foreach (DetalleOferta det in ordCompra.DetalleOferta)
                {
                    if (oPor.Lines.Count <= det.LineNum)
                        oPor.Lines.Add();

                    oPor.Lines.SetCurrentLine(det.LineNum);
                    if (oPor.Lines.LineStatus == BoStatus.bost_Open)
                        POR1(det);
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
                OPOR(oferta);
                foreach (DetalleOferta det in oferta.DetalleOferta)
                {
                    POR1(det);
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

        private void OPOR(Documento oferta)
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
        private void POR1(DetalleOferta detOfer)
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
    }
}