namespace Haarlem_Festival.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration20171123t1307 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "Jazz_EventId", "dbo.Jazz");
            DropIndex("dbo.Events", new[] { "Jazz_EventId" });
            AddColumn("dbo.Events", "Content", c => c.String());
            AddColumn("dbo.Talking", "SpeakerOneName", c => c.String());
            AddColumn("dbo.Talking", "SpeakerTwoName", c => c.String());
            DropColumn("dbo.Events", "Jazz_EventId");
            DropColumn("dbo.Talking", "Speaker1");
            DropColumn("dbo.Talking", "Speaker2");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Talking", "Speaker2", c => c.String());
            AddColumn("dbo.Talking", "Speaker1", c => c.String());
            AddColumn("dbo.Events", "Jazz_EventId", c => c.Int());
            DropColumn("dbo.Talking", "SpeakerTwoName");
            DropColumn("dbo.Talking", "SpeakerOneName");
            DropColumn("dbo.Events", "Content");
            CreateIndex("dbo.Events", "Jazz_EventId");
            AddForeignKey("dbo.Events", "Jazz_EventId", "dbo.Jazz", "EventId");
        }
    }
}
