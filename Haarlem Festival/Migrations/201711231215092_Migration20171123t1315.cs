namespace Haarlem_Festival.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration20171123t1315 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "Talking_EventId", "dbo.Talking");
            DropForeignKey("dbo.Jazz", "EventId", "dbo.Events");
            DropForeignKey("dbo.Talking", "EventId", "dbo.Events");
            DropIndex("dbo.Events", new[] { "Talking_EventId" });
            DropIndex("dbo.Jazz", new[] { "EventId" });
            DropIndex("dbo.Talking", new[] { "EventId" });
            CreateIndex("dbo.Jazz", "EventId");
            CreateIndex("dbo.Talking", "EventId");
            AddForeignKey("dbo.Jazz", "EventId", "dbo.Events", "EventId");
            AddForeignKey("dbo.Talking", "EventId", "dbo.Events", "EventId");
            DropColumn("dbo.Events", "Talking_EventId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Talking_EventId", c => c.Int());
            DropForeignKey("dbo.Talking", "EventId", "dbo.Events");
            DropForeignKey("dbo.Jazz", "EventId", "dbo.Events");
            DropIndex("dbo.Talking", new[] { "EventId" });
            DropIndex("dbo.Jazz", new[] { "EventId" });
            CreateIndex("dbo.Talking", "EventId");
            CreateIndex("dbo.Jazz", "EventId");
            CreateIndex("dbo.Events", "Talking_EventId");
            AddForeignKey("dbo.Talking", "EventId", "dbo.Events", "EventId");
            AddForeignKey("dbo.Jazz", "EventId", "dbo.Events", "EventId");
            AddForeignKey("dbo.Events", "Talking_EventId", "dbo.Talking", "EventId");
        }
    }
}
