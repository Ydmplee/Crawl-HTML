using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp6
{
    class Program
    {
        static void Main(string[] args)
        {
            GetHtml().Wait();
            
        }
        public static async Task GetHtml()
        {
            
            //https://kyfw.12306.cn/otn/leftTicket/init?linktypeid=dc&fs=%E5%B9%BF%E5%B7%9E,GZQ&ts=%E4%B8%8A%E6%B5%B7,SHH&date=2019-12-28&flag=N,N,Y
            //http://grad.cnu.edu.cn/tzgg/index.htm
            //https://www.12306.cn/index/
            using(HttpClient http = new HttpClient())
            {
                var response = await http.GetByteArrayAsync("https://www.12306.cn/index/");
                String source = Encoding.GetEncoding("utf-8").GetString(response, 0, response.Length - 1);
                source = WebUtility.HtmlDecode(source);
                HtmlDocument resultat = new HtmlDocument();
                resultat.LoadHtml(source);
                List<HtmlNode> toftitle = resultat.DocumentNode.Descendants().Where
                    (x => (x.Name == "div" && x.Attributes["class"] != null &&
                    x.Attributes["class"].Value.Contains("form-item"))).ToList();
                var li = toftitle[0].Descendants("input").ToList();
            }     
        }
    }
}
