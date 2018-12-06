using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace ClassLibrary
{
    //国网电子商务平台招投标信息抓取类
    public class ecp_sgcc_com_cn_xml
    {
        //【物资招标公告网址】
        private string m_Url = "http://ecp.sgcc.com.cn/topic_project_list.jsp?columnName=topic10&site=global&company_id=52&status=00&project_name=%E5%9B%BD%E5%AE%B6%E7%94%B5%E7%BD%91%E6%9C%89%E9%99%90%E5%85%AC%E5%8F%B8%E8%BE%93%E5%8F%98%E7%94%B5%E9%A1%B9%E7%9B%AE2018%E5%B9%B4&pageNo=9";

        //抓取物资招标公告
        public Boolean WZ_Zbgg()
        {
            Boolean lRet = false;
            String lContent = "";

            //获取招标公告页面
            Crawler lCrawler = new Crawler();
            lCrawler.SendRequest(m_Url, ref lContent);

            //WZ_Zbgg_Main(ref lContent);

            return lRet;
        }

        //物资招标公告内容处理
        //
        //private Boolean WZ_Zbgg_Main(ref String p_Content)
        //{
        //    Boolean lRet = false;

        //    Crawler lCrawler = new Crawler();
        //    List<Match> lTableList = new List<Match>();

        //    lCrawler.Matches(p_Content, "<table.*?</table>", ref lTableList);

        //    if (lTableList.Count > 0)
        //    {
        //        DataTable lDT = null;
        //        WZ_Zbgg_Import(lTableList[0].Value, ref lDT);
        //    }

        //    return lRet;
        //}


        //物资招标公告内容处理
        //
        private Boolean WZ_Zbgg_Import(String p_Zbgg_Content, ref DataTable pDT)
        {
            Boolean lRet = false;
            XmlDocument doc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            //settings.i
            try
            {
                doc.LoadXml(p_Zbgg_Content);
            }
            catch (Exception ex)
            {

            }
            // 得到根节点bookstore
            XmlNode xn = doc.SelectSingleNode("table");

            // 得到根节点的所有子节点
            XmlNodeList xnl = xn.ChildNodes;

            //foreach (XmlNode xn1 in xnl)
            //{
            //    ecp_sgcc_com_cn_model bookModel = new ecp_sgcc_com_cn_model();
            //    // 将节点转换为元素，便于得到节点的属性值
            //    XmlElement xe = (XmlElement)xn1;
            //    // 得到Type和ISBN两个属性的属性值
            //    bookModel.BookISBN = xe.GetAttribute("ISBN").ToString();
            //    bookModel.BookType = xe.GetAttribute("Type").ToString();
            //    // 得到Book节点的所有子节点
            //    XmlNodeList xnl0 = xe.ChildNodes;
            //    bookModel.BookName = xnl0.Item(0).InnerText;
            //    bookModel.BookAuthor = xnl0.Item(1).InnerText;
            //    bookModel.BookPrice = Convert.ToDouble(xnl0.Item(2).InnerText);
            //    bookModeList.Add(bookModel);
            //}
            //dgvBookInfo.DataSource = bookModeList;

            return lRet;
        }
    }
}
