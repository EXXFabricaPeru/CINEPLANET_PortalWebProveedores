using SAPbobsCOM;
using WebProov_API.Data.Interfaces;
using WebProov_API.Models;
using WebProov_API.Util;

namespace WebProov_API.Data
{
    public class LoginDIAPI : ILoginRepository
    {
        private Company _company;
        Documents oPor, oOrd;
        public LoginDIAPI()
        {
            _company = DIAPIConexion.GetDIAPIConexion();
        }

        public User GetLogin(User user)
        {
            Recordset oRS = _company.GetBusinessObject(BoObjectTypes.BoRecordset);
            oRS.DoQuery(Queries.ValidarUser(user.Mail,user.Password));
            if (oRS.RecordCount == 0) return null;
            user.Code= oRS.Fields.Item("CardCode").Value;
            user.Name= oRS.Fields.Item("CardName").Value;
            user.Ruc= oRS.Fields.Item("LicTradNum").Value;
            user.CreditLine= oRS.Fields.Item("CreditLine").Value.ToString();
            return user;
        }

        public bool ChangePassword(User user)
        {
            BusinessPartners oBp;
            oBp = _company.GetBusinessObject(BoObjectTypes.oBusinessPartners);
            if (!oBp.GetByKey(user.Code))
                throw new System.Exception("Error: El socio con código: " + user.Code + " no existe en la sociedad.");

            oBp.Password = user.Password;
            if (oBp.Update() != 0)
                throw new System.Exception(_company.GetLastErrorDescription());

            return true;
        }
    }
}