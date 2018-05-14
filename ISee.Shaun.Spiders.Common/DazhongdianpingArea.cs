using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISee.Shaun.Spiders.Common
{
    public static class DazhongdianpingArea
    {
        private static Dictionary<string, string> areaDic = null;
        public static Dictionary<string, string> GetAreaDic()
        {
            if (areaDic == null)
            {
                areaDic = new Dictionary<string, string>();
                areaDic.Add("r16", "西城区");
                areaDic.Add("r15", "东城区");
                areaDic.Add("r17", "海淀区");
                areaDic.Add("r328", "石景山区");
                areaDic.Add("r14", "朝阳区");
                areaDic.Add("r20", "丰台区");
                areaDic.Add("r9158", "顺义区");
                areaDic.Add("r5950", "昌平区");
                areaDic.Add("r5952", "大兴区");
                areaDic.Add("r9157", "房山区");
                areaDic.Add("r5951", "通州区");
                areaDic.Add("c4453", "怀柔区");
                areaDic.Add("c435", "延庆区");
                areaDic.Add("c434", "密云区");
                areaDic.Add("c4454", "门头沟区");
                areaDic.Add("c4455", "平谷区");
            }
            return areaDic;
        }
    }
}
