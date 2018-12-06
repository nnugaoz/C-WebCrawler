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
    }
}
