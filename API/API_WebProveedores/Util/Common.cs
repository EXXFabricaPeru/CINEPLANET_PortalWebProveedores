using WebProov_API.Data;

namespace WebProov_API.Util
{
    public class Common
    {
        public static string GetAccountFromFormat(string acc)
        {
            string val = DIAPIConexion.GenericQuery(Queries.ConvertAccount(acc.Replace("-", ""), true));
            return string.IsNullOrEmpty(val) ? acc : val;
        }

        public static string GetAccountFromCode(string acc)
        {
            string val = DIAPIConexion.GenericQuery(Queries.ConvertAccount(acc, false));
            return string.IsNullOrEmpty(val) ? acc : val;
        }


    }
}
