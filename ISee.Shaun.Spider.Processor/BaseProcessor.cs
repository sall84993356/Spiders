using DotnetSpider.Core;
using DotnetSpider.Core.Processor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ISee.Shaun.Spiders.Common.DelegeteDefine;

namespace ISee.Shaun.Spiders.Processor
{
    public class BaseProcessor : BasePageProcessor
    {
        protected List<string> foodUrls = null;
        public CallbackEventHandler InvokeFoodUrls { get; set; }

        protected string SourceWebsite { get; set; }

        public BaseProcessor() { foodUrls = new List<string>(); }

        protected override void Handle(Page page)
        {
            throw new NotImplementedException();
        }

        protected virtual void InvokeCallback(string processorName, string pipeLineName)
        {
            if (InvokeFoodUrls != null && this.foodUrls.Count > 0)
            {
                InvokeFoodUrls(processorName, pipeLineName, this.foodUrls);
            }
        }
    }
}
