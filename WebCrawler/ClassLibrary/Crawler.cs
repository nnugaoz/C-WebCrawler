using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace ClassLibrary
{
    public class Crawler
    {
        public Boolean SendRequest(String pURI, ref String pResponse)
        {
            Boolean lRet = false;

            using (HttpClient lHttpClient = new HttpClient())
            {
                Task<String> t = lHttpClient.GetStringAsync(pURI);
                pResponse = t.Result;
            }
            return lRet;
        }

        public Boolean Matches(string pText, string pExpr, ref List<Match> pResult)
        {
            Boolean lRet = false;

            MatchCollection mc = Regex.Matches(pText, pExpr,RegexOptions.Singleline);

            foreach (Match m in mc)
            {
                pResult.Add(m);
            }
            return lRet;
        }
    }
}
