using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.IO;

namespace ClassLibrary
{
    //国网电子商务平台招投标信息抓取类
    public class ecp_sgcc_com_cn_re
    {
        //【物资招标公告网址】
        private string m_Url = "http://ecp.sgcc.com.cn/topic_project_list.jsp?columnName=topic10&site=global&company_id=52&status=00&project_name=%E5%9B%BD%E5%AE%B6%E7%94%B5%E7%BD%91%E6%9C%89%E9%99%90%E5%85%AC%E5%8F%B8%E8%BE%93%E5%8F%98%E7%94%B5%E9%A1%B9%E7%9B%AE2018%E5%B9%B4&pageNo=9";

        //抓取物资招标公告
        public Boolean WZ_Zbgg()
        {
            Boolean lRet = false;
            String lContent = "";

            //获取招标公告页面
            //Crawler lCrawler = new Crawler();
            //lCrawler.SendRequest(m_Url, ref lContent);

            lContent = File.ReadAllText("1.txt", Encoding.UTF8);
            
            WZ_Zbgg_Main(ref lContent);

            return lRet;
        }

        //物资招标公告内容处理
        //
        private Boolean WZ_Zbgg_Main(ref String p_Content)
        {
            Boolean lRet = false;
            String lTablePattern = "<table.*?</table>";
            MatchCollection lTables = null;

            lTables = Regex.Matches(p_Content, lTablePattern, RegexOptions.Singleline);

            DataTable lDT = null;

            if (lTables.Count > 0)
            {
                WZ_Zbgg_Import(lTables[0].Value, ref lDT);
            }

            
            return lRet;
        }

        private Boolean WZ_Zbgg_Import(String p_Zbgg_Content, ref DataTable pDT)
        {
            Boolean lRet = false;
            String lTrPattern = "<tr.*?</tr>";
            String lTdPattern = "(<td[^>]*>)(.*?)(</td>)";
            String lTdTitlePattern = "(\\btitle\\b)(\\s*=\\s*)(\")([^\"]*)(\")";

            MatchCollection lTrs = null;
            MatchCollection lTds = null;
            MatchCollection lTdTitle = null;

            lTrs = Regex.Matches(p_Zbgg_Content, lTrPattern, RegexOptions.Singleline);

            pDT = new DataTable();
            for (int i = 0; i < lTrs.Count; i++)
            {
                String lTableRowStr = lTrs[i].Value;
                lTds = Regex.Matches(lTableRowStr, lTdPattern, RegexOptions.Singleline);

                if (i == 0)
                {
                    for (int j = 0; j < lTds.Count; j++)
                    {
                        Match lTableCellMatch = lTds[j];
                        DataColumn lDTCol = new DataColumn(lTableCellMatch.Groups[2].Value, System.Type.GetType("System.String"));
                        pDT.Columns.Add(lDTCol);
                    }
                }
                else
                {
                    DataRow lDTRow = pDT.NewRow();
                    for (int j = 0; j < lTds.Count; j++)
                    {
                        if (j == 2)
                        {
                            Match lTableCellMatch = lTds[j];
                            lTdTitle = Regex.Matches(lTableCellMatch.Value, lTdTitlePattern, RegexOptions.Singleline);
                            lDTRow[j] = lTdTitle[0].Groups[4].Value.Trim();
                        }
                        else
                        {
                            Match lTableCellMatch = lTds[j];
                            lDTRow[j] = lTableCellMatch.Groups[2].Value.Trim();
                        }
                    }
                    pDT.Rows.Add(lDTRow);
                }
            }

            return lRet;
        }
    }
}
