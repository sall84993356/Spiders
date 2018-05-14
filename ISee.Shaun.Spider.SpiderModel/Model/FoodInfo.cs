using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISee.Shaun.Spiders.SpiderModel.Model
{
    public class FoodInfo
    {
        [Key]
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public string Code { get; set; }
        public string RestaurantCode { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string FoodImageUrl { get; set; }
        [ForeignKey("RestaurantId")]
        public Restaurant restaurant { get; set; }
    }
}
