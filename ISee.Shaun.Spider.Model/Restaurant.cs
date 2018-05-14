using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISee.Shaun.Spider.Model
{
    public partial class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Recommendation { get; set; }
        public string Position { get; set; }
        public string Taste { get; set; }
        public string Environment { get; set; }
        public string ServiceScore { get; set; }
        public string AveragePrice { get; set; }
    }
}
