using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotnetSpider.Core;
using DotnetSpider.Core.Downloader;
using DotnetSpider.Core.Pipeline;
using DotnetSpider.Core.Processor;
using DotnetSpider.Core.Scheduler;
using ISee.Shaun.Spiders.Common;
using ISee.Shaun.Spiders.Pipeline;
using ISee.Shaun.Spiders.Processor;

namespace ISee.Shaun.Spiders.Business
{
    public class RunSpider
    {
        private const string ASSEMBLY_PROCESSOR_NAME = "ISee.Shaun.Spiders.Processor";
        private const string ASSEMBLY_PIPELINE_NAME = "ISee.Shaun.Spiders.Pipeline";
        private BaseProcessor processor = null;
        private BasePipeline pipeline = null;
        private Site site = null;
        private string encoding = string.Empty;
        private bool removeOutBound = false;

        private int spiderThreadNums = 1;
        public int SpiderThreadNums { get => spiderThreadNums; set => spiderThreadNums = value; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="processorName"></param>
        /// <param name="pipeLineName"></param>
        public RunSpider(string processorName, string pipeLineName, string encoding, bool removeOutBound)
        {
            //通过反射，获取当前处理类
            processor = ReflectionInvoke.GetInstance(ASSEMBLY_PROCESSOR_NAME, processorName, null) as BaseProcessor;
            //如果需要回写信息，使用当前委托，如这里，继续子页面的抓取调用
            processor.InvokeFoodUrls = this.InvokeNext;
            pipeline = ReflectionInvoke.GetInstance(ASSEMBLY_PIPELINE_NAME, pipeLineName, null) as BasePipeline;
            this.encoding = encoding;
            this.removeOutBound = removeOutBound;
        }

        /// <summary>
        /// 执行，按照页号
        /// </summary>
        /// <param name="urlInfo"></param>
        /// <param name="times"></param>
        public void Run(string urlInfo, int times)
        {
            SetSite(encoding, removeOutBound, urlInfo, times);
            Run();
        }

        /// <summary>
        /// 执行，按照地址集合
        /// </summary>
        /// <param name="urlList"></param>
        public void Run(List<string> urlList)
        {
            SetSite(encoding, removeOutBound, urlList);
            Run();
        }

        /// <summary>
        /// Begin spider
        /// </summary>
        private void Run()
        {
            Spider spider = Spider.Create(site, new QueueDuplicateRemovedScheduler(), processor);
            spider.AddPipeline(pipeline);
            spider.Downloader = new HttpClientDownloader();
            spider.ThreadNum = this.spiderThreadNums;
            spider.EmptySleepTime = 3000;
            spider.Deep = 3;
            spider.Run();
        }

        private void InvokeNext(string processorName, string pipeLineName, List<string> foodUrls)
        {
            RunSpider runSpider = new RunSpider(processorName, pipeLineName, this.encoding, true);
            runSpider.Run(foodUrls);
        }

        /// <summary>
        /// 通过可变页号，设定站点URL
        /// </summary>
        /// <param name="encoding"></param>
        /// <param name="removeOutBound"></param>
        /// <param name="urlInfo"></param>
        /// <param name="times"></param>
        private void SetSite(string encoding, bool removeOutBound, string urlInfo, int times)
        {
            this.site = new Site { EncodingName = encoding, RemoveOutboundLinks = false };
            if (times == 0)
            {
                this.site.AddStartUrl(urlInfo);
            }
            else
            {
                List<string> urls = new List<string>();
                for (int i = 1; i <= times; ++i)
                {
                    urls.Add(string.Format(urlInfo, i));
                }
                this.site.AddStartUrls(urls);
            }
        }

        /// <summary>
        /// 通过URL集合设置站点URL
        /// </summary>
        /// <param name="encoding"></param>
        /// <param name="removeOutBound"></param>
        /// <param name="urlList"></param>
        private void SetSite(string encoding, bool removeOutBound, List<string> urlList)
        {
            this.site = new Site { EncodingName = encoding, RemoveOutboundLinks = false };
            this.site.AddStartUrls(urlList);
        }
    }
}
