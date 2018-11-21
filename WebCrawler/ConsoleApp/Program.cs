using ClassLibrary;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {

            //Crawler lCrawler = new Crawler();
            //List<string> lResult = new List<string> ();
            //lCrawler.Matches("<td width='180px'>AA</td><td>BB</td>", "(<td[^>]*>)(.*?)(</td>)", ref lResult);

            //return;

            //国网电子商务平台
            ecp_sgcc_com_cn lecp_sgcc_com_cn = new ecp_sgcc_com_cn();
            //抓取【物资招标公告】
            lecp_sgcc_com_cn.WZ_Zbgg();
            Console.WriteLine("Done!");
            Console.ReadKey();
        }
    }
}
