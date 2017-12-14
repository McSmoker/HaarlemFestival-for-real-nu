using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Haarlem_Festival.Models
{
    public class HaarlemFestivalDB : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public HaarlemFestivalDB() : base("name=HaarlemFestivalDB")
        {
        }

        public System.Data.Entity.DbSet<Haarlem_Festival.Models.Event> Events { get; set; }

        public System.Data.Entity.DbSet<Haarlem_Festival.Models.Jazz> Jazz { get; set; }

        public System.Data.Entity.DbSet<Haarlem_Festival.Models.Talking> Talking { get; set; }

        public System.Data.Entity.DbSet<Haarlem_Festival.Models.Volunteer> Volunteers { get; set; }

        public System.Data.Entity.DbSet<Haarlem_Festival.Models.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<Haarlem_Festival.Models.OrderItem> OrderItems { get; set; }

        public System.Data.Entity.DbSet<Haarlem_Festival.Models.SiteContent> SiteContent { get; set; }

        public System.Data.Entity.DbSet<Haarlem_Festival.Models.Performer> Performer { get; set; }

    }
}
