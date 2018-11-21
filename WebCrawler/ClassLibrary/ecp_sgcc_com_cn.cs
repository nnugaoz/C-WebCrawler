using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassLibrary
{
    //国网电子商务平台招投标信息抓取类
    public class ecp_sgcc_com_cn
    {
        //【物资招标公告网址】
        private string m_Url = "http://ecp.sgcc.com.cn/project_list.jsp?site=global&column_code=014001001&project_type=1&company_id=&status=&project_name=&pageNo=1";

        //抓取物资招标公告
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

        //物资招标公告内容处理
        //
        private Boolean WZ_Zbgg_Main(ref String p_Content)
        {
            Boolean lRet = false;

            Crawler lCrawler = new Crawler();
            List<Match> lTableList = new List<Match>();

            lCrawler.Matches(p_Content, "<table.*?</table>", ref lTableList);

            if (lTableList.Count > 0)
            {
                DataTable lDT = null;
                WZ_Zbgg_Import(lTableList[0].Value, ref lDT);
            }

            return lRet;
        }

        private Boolean WZ_Zbgg_Import(String p_Zbgg_Content, ref DataTable pDT)
        {
            Boolean lRet = false;

            Crawler lCrawler = new Crawler();
            List<Match> lTableRowList = new List<Match>();
            List<Match> lTableCellList = new List<Match>();

            lCrawler.Matches(p_Zbgg_Content, "<tr.*?</tr>", ref lTableRowList);
            pDT = new DataTable();
            for (int i = 0; i < lTableRowList.Count; i++)
            {
                String lTableRowStr = lTableRowList[i].Value;
                lTableCellList.Clear();
                lCrawler.Matches(lTableRowStr, "(<td[^>]*>)(.*?)(</td>)", ref lTableCellList);

                if (i == 0)
                {
                    for (int j = 0; j < lTableCellList.Count; j++)
                    {
                        Match lTableCellMatch = lTableCellList[j];
                        DataColumn lDTCol = new DataColumn(lTableCellMatch.Groups[2].Value, System.Type.GetType("System.String"));
                        pDT.Columns.Add(lDTCol);
                    }
                }
                else
                {
                    DataRow lDTRow = pDT.NewRow();
                    for (int j = 0; j < lTableCellList.Count; j++)
                    {
                        Match lTableCellMatch = lTableCellList[j];
                        lDTRow[j] = lTableCellMatch.Groups[2].Value;
                    }
                    pDT.Rows.Add(lDTRow);
                }
            }

            return lRet;
        }
    }
}
