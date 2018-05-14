using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISee.Shaun.Spiders.Common
{
    public class DelegeteDefine
    {
        public delegate void CallbackEventHandler(string processorName, string pipeLineName, List<string> foodUrls);
    }
}
