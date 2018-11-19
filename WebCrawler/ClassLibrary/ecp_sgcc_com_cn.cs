using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ecp_sgcc_com_cn
    {
        private string m_Url = "http://ecp.sgcc.com.cn/project_list.jsp?site=global&column_code=014001001&project_type=1&company_id=&status=&project_name=&pageNo=1";

        public Boolean WZ_Zbgg()
        {
            Boolean lRet = false;
            String lContent = "";

            //获取招标公告页面
            Crawler lCrawler = new Crawler();
            lCrawler.SendRequest(m_Url, ref lContent);

            WZ_Zbgg_Main(ref lContent);

            return lRet;
        }

        public Boolean WZ_Zbgg_Main(ref String p_Content)
        {
            Boolean lRet = false;
            Crawler lCrawler = new Crawler();
            List<string> lTableList = new List<string>();
            lCrawler.Matches(ref p_Content, "<table>.*</table>", ref lTableList);

            foreach (string lTableContent in lTableList)
            {
                FileHelper.SaveAsFile("Result.txt", lTableContent);
            }

            return lRet;
        }
    }
}
