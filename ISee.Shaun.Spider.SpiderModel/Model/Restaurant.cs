using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISee.Shaun.Spiders.SpiderModel.Model
{
    public class Restaurant
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Star { get; set; }
        public string ImageUrl { get; set; }
        public string Area { get; set; }
        public string Recommendation { get; set; }
        public string Position { get; set; }
        public string Address { get; set; }
        public string Taste { get; set; }
        public string Environment { get; set; }
        public string ServiceScore { get; set; }
        public string AveragePrice { get; set; }
        public string SourceWebsite { get; set; }

        public DbSet<FoodInfo> foodList { get; set; }
    }
}
