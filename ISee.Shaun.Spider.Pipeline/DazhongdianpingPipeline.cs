using DotnetSpider.Core;
using DotnetSpider.Core.Pipeline;
using ISee.Shaun.Spiders.SpiderModel.Model;
using ISee.Shaun.Spiders.SpiderModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISee.Shaun.Spiders.Pipeline
{
    public class DazhongdianpingPipeline : BasePipeline
    {
        /// <summary>
        /// 处理餐厅信息
        /// </summary>
        /// <param name="resultItems"></param>
        /// <param name="spider"></param>
        public override void Process(IEnumerable<ResultItems> resultItems, ISpider spider)
        {
            //便利结果集
            foreach (ResultItems entry in resultItems)
            {
                //定义EF实体
                using (var rEntity = new FoodInfoEntity())
                {
                    List<Restaurant> resList = new List<Restaurant>();
                    foreach (Restaurant result in entry.Results["RestaurantList"])
                    {
                        //通过餐厅名称和地址作为筛重条件
                        var resultList = rEntity.RestaurantInfo.Where(o => o.Name == result.Name && o.Address == result.Address).ToList();
                        if (resultList.Count == 0)
                        {
                            resList.Add(result);
                        }
                    }
                    if (resList.Count > 0)
                    {
                        rEntity.RestaurantInfo.AddRange(resList);
                        rEntity.SaveChanges();
                    }
                }
            }

        }
    }
}
