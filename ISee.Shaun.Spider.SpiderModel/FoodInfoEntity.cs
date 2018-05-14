using ISee.Shaun.Spiders.SpiderModel.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ISee.Shaun.Spiders.SpiderModel
{
    public class FoodInfoEntity: DbContext
    {
        public DbSet<Restaurant> RestaurantInfo { get; set; }
        public DbSet<FoodInfo> foodInfo { get; set; }

        public FoodInfoEntity()
            : base("name=ConnectionStr")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
