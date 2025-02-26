using SAPbobsCOM;


namespace WebProov_API.Data
{
    public static class DIAPIConexion
    {
        public static Company oComm;
        public static Company GetDIAPIConexion()
        {
            try
            {

                if (oComm != null && oComm.Connected) { return oComm; };
                Company oCompany = new Company
                {
                    Server = Startup.StaConfig["DIAPI:server"],
                    DbServerType = BoDataServerTypes.dst_HANADB,
                    DbUserName = Startup.StaConfig["DIAPI:DbUserName"],
                    DbPassword = Startup.StaConfig["DIAPI:DbPassword"],
                    UserName = Startup.StaConfig["DIAPI:UserName"],
                    Password = Startup.StaConfig["DIAPI:Password"],
                    CompanyDB = Startup.StaConfig["DIAPI:CompanyDB"]
                };

                if (oCompany.Connect() != 0)
                    throw new System.Exception(oCompany.GetLastErrorDescription());

                oComm = oCompany;
                return oCompany;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        internal static string GenericQuery(string query)
        {
            Recordset oRS = oComm.GetBusinessObject(BoObjectTypes.BoRecordset);
            string valor = "";
            try
            {
                oRS.DoQuery(query);
                if (oRS.RecordCount > 0)
                {
                    valor = oRS.Fields.Item("Value").Value.ToString().Trim();
                }
            }
            catch (System.Exception ex)
            {

            }
            finally { System.Runtime.InteropServices.Marshal.ReleaseComObject(oRS); }

            return valor;
        }
    }
}
