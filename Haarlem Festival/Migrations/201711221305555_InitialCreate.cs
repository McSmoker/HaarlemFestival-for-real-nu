namespace Haarlem_Festival.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        EventDateTime = c.DateTime(nullable: false),
                        Location = c.String(),
                        Price = c.Double(nullable: false),
                        Seats = c.Int(nullable: false),
                        TicketsSold = c.Int(nullable: false),
                        Jazz_EventId = c.Int(),
                        Talking_EventId = c.Int(),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.Jazz", t => t.Jazz_EventId)
                .ForeignKey("dbo.Talking", t => t.Talking_EventId)
                .Index(t => t.Jazz_EventId)
                .Index(t => t.Talking_EventId);
            
            CreateTable(
                "dbo.Jazz",
                c => new
                    {
                        EventId = c.Int(nullable: false),
                        Artist = c.String(),
                        Hall = c.String(),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.Events", t => t.EventId)
                .Index(t => t.EventId);
            
            CreateTable(
                "dbo.Talking",
                c => new
                    {
                        EventId = c.Int(nullable: false),
                        Speaker1 = c.String(),
                        Speaker2 = c.String(),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.Events", t => t.EventId)
                .Index(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Talking", "EventId", "dbo.Events");
            DropForeignKey("dbo.Jazz", "EventId", "dbo.Events");
            DropForeignKey("dbo.Events", "Talking_EventId", "dbo.Talking");
            DropForeignKey("dbo.Events", "Jazz_EventId", "dbo.Jazz");
            DropIndex("dbo.Talking", new[] { "EventId" });
            DropIndex("dbo.Jazz", new[] { "EventId" });
            DropIndex("dbo.Events", new[] { "Talking_EventId" });
            DropIndex("dbo.Events", new[] { "Jazz_EventId" });
            DropTable("dbo.Talking");
            DropTable("dbo.Jazz");
            DropTable("dbo.Events");
        }
    }
}
