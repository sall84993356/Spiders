using DotnetSpider.Core;
using DotnetSpider.Core.Selector;
using ISee.Shaun.Spiders.SpiderModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISee.Shaun.Spiders.Processor
{
    public class DazhongdianpingFoodProcessor : BaseProcessor
    {
        public DazhongdianpingFoodProcessor() : base()
        {

        }

        protected override void Handle(Page page)
        {
            var totalVideoElements = page.Selectable.SelectList(Selectors.XPath(".//ul[@class='first-cate J-primary-menu']/li")).Nodes();
            List<FoodInfo> foodList = new List<FoodInfo>();
        }
    }
}