using ISee.Shaun.Spiders.Business;
using ISee.Shaun.Spiders.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetSpider
{
    class Program
    {
        private static string urlInfo = ConfigurationManager.AppSettings["WebUrls"];
        private static string urlAreaInfo = ConfigurationManager.AppSettings["WebAreaUrls"];
        static void Main(string[] args)
        {
            Run();
        }

        /// <summary>
        /// Begin spider
        /// </summary>
        private static void Run()
        {
            //Add other areaInfo
            Dictionary<string, string> areaDic = DazhongdianpingArea.GetAreaDic();
            List<string> urls = new List<string>();
            foreach (var key in areaDic.Keys)
            {
                for (int i = 1; i <= 50; i++)
                {
                    urls.Add(string.Format(urlAreaInfo, key, i));
                }
            }
            RunSpider runSpiders = new RunSpider("DazhongdianpingProcessor", "DazhongdianpingPipeline", "UTF-8", true);
            runSpiders.Run(urls);

            //RunSpider runSpider = new RunSpider("DazhongdianpingProcessor", "DazhongdianpingPipeline", "UTF-8", true);
            //runSpider.Run(urlInfo, 50);
        }
    }
}
