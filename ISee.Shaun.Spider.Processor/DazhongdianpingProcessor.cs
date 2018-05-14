using DotnetSpider.Core;
using DotnetSpider.Core.Processor;
using DotnetSpider.Core.Selector;
using ISee.Shaun.Spiders.Common;
using ISee.Shaun.Spiders.SpiderModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ISee.Shaun.Spiders.Common.DelegeteDefine;

namespace ISee.Shaun.Spiders.Processor
{
    public class DazhongdianpingProcessor : BaseProcessor
    {
        public DazhongdianpingProcessor() : base()
        {
            //标记当前数据来源
            SourceWebsite = "大众点评";
        }

        /// <summary>
        /// 重新父类方法，开始执行数据获取操作
        /// </summary>
        /// <param name="page"></param>
        protected override void Handle(Page page)
        {
            // 利用 Selectable 查询并构造自己想要的数据对象
            var totalVideoElements = page.Selectable.SelectList(Selectors.XPath(".//div[@class='shop-list J_shop-list shop-all-list']/ul/li")).Nodes();
            if (totalVideoElements == null)
            {
                return;
            }
            //定义需处理数据集合
            List<Restaurant> restaurantList = new List<Restaurant>();
            foreach (var restElement in totalVideoElements)
            {
                var restaurant = new Restaurant() { SourceWebsite = SourceWebsite };
                //下面通过xpath开始获取餐厅信息
                restaurant.Name = restElement.Select(Selectors.XPath(".//h4")).GetValue();
                var price= restElement.Select(Selectors.XPath(".//div[@class='txt']/div/a[@class='mean-price']/b")).GetValue();
                restaurant.AveragePrice = string.IsNullOrEmpty(price) ? "0" : price.Replace("￥","");
                restaurant.Type = restElement.Select(Selectors.XPath(".//div[@class='txt']/div[@class='tag-addr']/a/span[@class='tag']")).GetValue();
                restaurant.Star = restElement.Select(Selectors.XPath(".//div[@class='txt']/div[@class='comment']/span/@title")).GetValue();
                restaurant.ImageUrl = restElement.Select(Selectors.XPath(".//div[@class='pic']/a/img/@src")).GetValue();
                var areaCode = page.Url.Substring(page.Url.LastIndexOf('/')+1);
                if (!string.IsNullOrEmpty(areaCode) && (areaCode.Contains("r")|| areaCode.Contains("c")))
                {
                    Dictionary<string, string> areaDic = DazhongdianpingArea.GetAreaDic();
                    string result= areaCode.Substring(0, areaCode.IndexOf('p'));
                    if (areaDic.ContainsKey(result))
                    {
                        restaurant.Area = areaDic[result];
                    }
                }

                List<ISelectable> infoList = restElement.SelectList(Selectors.XPath("./div[@class='txt']/span[@class='comment-list']/span/b")).Nodes() as List<ISelectable>;
                if (infoList != null && infoList.Count > 0)
                {
                    var result = infoList[0].GetValue();
                    restaurant.Taste = string.IsNullOrEmpty(result) ? string.Empty : result;
                    result = infoList[1].GetValue();
                    restaurant.Environment = string.IsNullOrEmpty(result) ? string.Empty : result;
                    result = infoList[2].GetValue();
                    restaurant.ServiceScore = string.IsNullOrEmpty(result) ? string.Empty : result;
                }

                var recommetList = restElement.SelectList(Selectors.XPath(".//div[@class='txt']/div[@class='recommend']/a")).Nodes();
                restaurant.Recommendation = string.Join(",", recommetList.Select(o => o.GetValue()));
                restaurant.Address = restElement.Select(Selectors.XPath(".//div[@class='txt']/div[@class='tag-addr']/span")).GetValue();
                restaurant.Position= restElement.Select(Selectors.XPath(".//div[@class='txt']/div[@class='tag-addr']/a[@data-click-name='shop_tag_region_click']/span[@class='tag']")).GetValue();

                var shopUrl = restElement.Select(Selectors.XPath(".//div[@class='txt']/div/a/@href")).GetValue();
                restaurant.Code = shopUrl.Substring(shopUrl.LastIndexOf('/') + 1);
                restaurantList.Add(restaurant);

                //add next links
                if (!string.IsNullOrEmpty(shopUrl))
                {
                    this.foodUrls.Add(shopUrl);
                }
            }
            // 如果进行二级爬虫，取消注释，并且实现对应的两个类
            //InvokeCallback("DazhongdianpingFoodProcessor", "DazhongdianpingFoodPipeline");
            // Save data object by key. 以自定义KEY存入page对象中供Pipeline调用
            page.AddResultItem("RestaurantList", restaurantList);
        }
    }
}
