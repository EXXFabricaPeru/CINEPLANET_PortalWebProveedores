using System.Text;

namespace WebProov_API.Util
{
    public class Queries
    {

        #region _Attributes_

        private static StringBuilder m_sSQL = new StringBuilder();

        #endregion

        #region _Functions_
        public static string ListarProyectos()
        {
            m_sSQL.Length = 0;
            m_sSQL.Append("SELECT \"PrjCode\"  FROM OPRJ");
            return m_sSQL.ToString();
        }

        public static string ValidarUser(string mail, string password)
        {
            m_sSQL.Length = 0;
            m_sSQL.Append("SELECT TOP 1 \"CardCode\",\"CardName\",\"E_Mail\",\"LicTradNum\",\"CreditLine\" ");
            m_sSQL.Append("FROM OCRD ");
            m_sSQL.AppendFormat("WHERE \"E_Mail\"='{0}' and \"Password\"='{1}' AND \"CardType\"='S' ",mail, password);
            return m_sSQL.ToString();
        }

        public static string GetOPQTDocNum(string docEntry)
        {
            m_sSQL.Length = 0;
            m_sSQL.Append("SELECT \"DocNum\" \"Value\" FROM OPQT ");
            m_sSQL.AppendFormat("WHERE \"DocEntry\"='{0}'", docEntry);
            return m_sSQL.ToString();
        }

        public static string GetStockXAlmacen(string whsCode, string itemCode)
        {
            m_sSQL.Length = 0;
            m_sSQL.Append("SELECT \"OnHand\"-\"IsCommited\" \"Value\" FROM OITW ");
            m_sSQL.AppendFormat("WHERE \"ItemCode\"='{0}' and \"WhsCode\"='{1}'", itemCode, whsCode);
            return m_sSQL.ToString();
        }
        public static string GetDescCondicionPago(int entry)
        {
            m_sSQL.Length = 0;
            m_sSQL.Append("SELECT \"PymntGroup\"  \"Value\" FROM OCTG ");
            m_sSQL.AppendFormat("WHERE \"GroupNum\"='{0}'", entry);
            return m_sSQL.ToString();
        }
        public static string GetNombreEmpleado(string entry)
        {
            m_sSQL.Length = 0;
            m_sSQL.Append("SELECT \"SlpName\"  \"Value\" FROM OSLP ");
            m_sSQL.AppendFormat("WHERE \"SlpCode\"='{0}'", entry);
            return m_sSQL.ToString();
        }
        public static string GetNombreAlmacen(string entry)
        {
            m_sSQL.Length = 0;
            m_sSQL.Append("SELECT \"WhsName\"  \"Value\" FROM OWHS ");
            m_sSQL.AppendFormat("WHERE \"WhsCode\"='{0}'", entry);
            return m_sSQL.ToString();
        }

        public static string ConvertAccount(string account, bool fromFormatCode)
        {
            m_sSQL.Length = 0;
            m_sSQL.AppendFormat("SELECT \"{0}\" \"Value\" ", fromFormatCode ? "AcctCode" : "FormatCode");
            m_sSQL.Append("FROM OACT ");
            m_sSQL.AppendFormat("WHERE \"{0}\" = '{1}'", !fromFormatCode ? "AcctCode" : "FormatCode", account);

            return m_sSQL.ToString();
        }

        public static string GetPedidosByRUC(string ruc)
        {
            m_sSQL.Length = 0;
            m_sSQL.Append("SELECT distinct FA.\"DocEntry\",FA.\"NumAtCard\",FA.\"DocNum\",FA.\"DocDate\",FA.\"DocDueDate\",FA.\"DocCur\",FA.\"DocTotal\",FA.\"DocTotalFC\", FA.\"Comments\", CASE WHEN FA.\"DocStatus\"='O' THEN 'Abierto' else 'Cerrado' END \"Estado\", CASE WHEN IFNULL(FA.\"SlpCode\",-1)<>-1 THEN VE.\"SlpName\" else '-' END \"SlpName\" ");
            m_sSQL.Append("FROM OPOR FA ");
            m_sSQL.Append("LEFT JOIN OSLP VE ON FA.\"SlpCode\"=VE.\"SlpCode\" ");
            m_sSQL.AppendFormat("WHERE FA.\"LicTradNum\"='{0}'", ruc);
            return m_sSQL.ToString();
        }
        public static string GetFacturasOrdeByDateByRUC(string ruc)
        {
            m_sSQL.Length = 0;
            m_sSQL.Append("SELECT distinct I0.\"DocEntry\",I0.\"DocNum\",FP.\"PymntGroup\",  ");
            m_sSQL.Append("case I0.\"DocSubType\" WHEN 'IB' THEN 'Boleta' WHEN 'DN' THEN 'Nota Debito'  WHEN 'RI' THEN 'Reserva'ELSE 'Factura' END \"DocSubType\", ");
            m_sSQL.Append("ifnull(I0.\"FolioPref\",'') \"FolioPref\", ifnull(I0.\"FolioNum\",0) \"FolioNum\", ");
            m_sSQL.Append("I0.\"DocDate\",I0.\"DocDueDate\", I0.\"DocCur\", CASE WHEN FA.\"DocStatus\"='O' THEN 'Abierto' else 'Cerrado' END \"Estado\", ");
            m_sSQL.Append("I0.\"DocTotal\", I0.\"DocTotalFC\", I0.\"PaidToDate\", I0.\"PaidFC\", ");
            m_sSQL.Append("I0.\"DocTotal\"-I0.\"PaidToDate\" \"Saldo\", I0.\"DocTotalFC\"- I0.\"PaidFC\" \"SaldoFC\" ");
            m_sSQL.Append("FROM OPCH I0 ");
            m_sSQL.Append("LEFT JOIN OCTG FP ON I0.\"GroupNum\"=FP.\"GroupNum\" ");
            m_sSQL.AppendFormat("WHERE I0.\"LicTradNum\"='{0}'", ruc);
            return m_sSQL.ToString();
        }

        public static string GetBPbyRUCRazSoc(string input)
        {
            m_sSQL.Length = 0;
            m_sSQL.Append("SELECT TOP 1  C1.\"CardCode\",C1.\"CardName\",C1.\"LicTradNum\",FP.\"PymntGroup\" \"FormaPago\", ");
            m_sSQL.Append("CASE WHEN C1.\"CardType\"='C' THEN 'CLIENTE' ELSE 'LEAD' END \"CardType\",  ");
            m_sSQL.Append("GR.\"GroupName\" \"GroupCode\", C1.\"Phone1\",C1.\"Phone2\",C1.\"Cellular\", ");
            m_sSQL.Append("C1.\"E_Mail\" \"EmailAddress\", CASE WHEN C1.\"Currency\"='##' THEN 'MULTI' ELSE  C1.\"Currency\"END  \"Currency\", ");
            m_sSQL.Append("SL.\"SlpName\" \"SalesPerson\", ");
            m_sSQL.Append("IFNULL(DB.\"Street\"||' '||DB.\"City\",DS.\"Street\"||' '||DS.\"City\") \"MainDirection\", IFNULL(CP.\"FirstName\"||' '||CP.\"MiddleName\"||' '||CP.\"LastName\",'')  ");
            m_sSQL.Append("\"Contacto\",IFNULL(CP.\"Tel1\",'') \"ContactoPhone\",  ");
            m_sSQL.Append("C1.\"CreditLine\", C1.\"Balance\" \"DeudaALaFecha\", C1.\"CreditLine\"-C1.\"Balance\"  \"SaldoDisponible\" ");
            m_sSQL.Append("FROM OCRD C1 ");
            m_sSQL.Append("LEFT JOIN OCRG GR ON C1.\"GroupCode\"=GR.\"GroupCode\" ");
            m_sSQL.Append("LEFT JOIN OCTG FP ON C1.\"GroupNum\"=FP.\"GroupNum\" ");
            m_sSQL.Append("LEFT JOIN OSLP SL ON C1.\"SlpCode\"=SL.\"SlpCode\"   ");
            m_sSQL.Append("LEFT JOIN CRD1 DS ON C1.\"CardCode\"=DS.\"CardCode\" and DS.\"AdresType\"='S' and C1.\"ShipToDef\"=DS.\"Address\" ");
            m_sSQL.Append("LEFT JOIN CRD1 DB ON C1.\"CardCode\"=DB.\"CardCode\" and DB.\"AdresType\"='B' ");
            m_sSQL.Append("LEFT JOIN OCPR CP ON C1.\"CardCode\"=CP.\"CardCode\" and C1.\"CntctPrsn\"=CP.\"Name\" ");
            m_sSQL.AppendFormat("WHERE C1.\"CardType\" !='S' AND (C1.\"CardName\"LIKE '%{0}%' OR C1.\"LicTradNum\"='{0}' ) ", input);
            return m_sSQL.ToString();
        }
        public static string GetBPbyCardCode(string input)
        {
            m_sSQL.Length = 0;
            m_sSQL.Append("SELECT TOP 1  C1.\"CardCode\",C1.\"CardName\",C1.\"LicTradNum\",FP.\"PymntGroup\" \"FormaPago\", ");
            m_sSQL.Append("CASE WHEN C1.\"CardType\"='C' THEN 'CLIENTE' ELSE 'LEAD' END \"CardType\",  ");
            m_sSQL.Append("GR.\"GroupName\" \"GroupCode\", C1.\"Phone1\",C1.\"Phone2\",C1.\"Cellular\", ");
            m_sSQL.Append("C1.\"E_Mail\" \"EmailAddress\", CASE WHEN C1.\"Currency\"='##' THEN 'MULTI' ELSE  C1.\"Currency\"END  \"Currency\", ");
            m_sSQL.Append("SL.\"SlpName\" \"SalesPerson\", ");
            m_sSQL.Append("IFNULL(DB.\"Street\"||' '||DB.\"City\",DS.\"Street\"||' '||DS.\"City\") \"MainDirection\", IFNULL(CP.\"FirstName\"||' '||CP.\"MiddleName\"||' '||CP.\"LastName\",'')  ");
            m_sSQL.Append("\"Contacto\",IFNULL(CP.\"Tel1\",'') \"ContactoPhone\",  ");
            m_sSQL.Append("C1.\"CreditLine\", C1.\"Balance\" \"DeudaALaFecha\", C1.\"CreditLine\"-C1.\"Balance\"  \"SaldoDisponible\" ");
            m_sSQL.Append("FROM OCRD C1 ");
            m_sSQL.Append("LEFT JOIN OCRG GR ON C1.\"GroupCode\"=GR.\"GroupCode\" ");
            m_sSQL.Append("LEFT JOIN OCTG FP ON C1.\"GroupNum\"=FP.\"GroupNum\" ");
            m_sSQL.Append("LEFT JOIN OSLP SL ON C1.\"SlpCode\"=SL.\"SlpCode\"   ");
            m_sSQL.Append("LEFT JOIN CRD1 DS ON C1.\"CardCode\"=DS.\"CardCode\" and DS.\"AdresType\"='S' and C1.\"ShipToDef\"=DS.\"Address\" ");
            m_sSQL.Append("LEFT JOIN CRD1 DB ON C1.\"CardCode\"=DB.\"CardCode\" and DB.\"AdresType\"='B' ");
            m_sSQL.Append("LEFT JOIN OCPR CP ON C1.\"CardCode\"=CP.\"CardCode\" and C1.\"CntctPrsn\"=CP.\"Name\" ");
            m_sSQL.AppendFormat("WHERE   C1.\"CardCode\"='{0}' ", input);
            return m_sSQL.ToString();
        }
        #endregion
    }
}
