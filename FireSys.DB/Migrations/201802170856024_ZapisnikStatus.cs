namespace FireSys.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ZapisnikStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Zapisnik", "StatusId", c => c.Int(nullable: false, defaultValue: 1));
            CreateIndex("dbo.Zapisnik", "StatusId");
            AddForeignKey("dbo.Zapisnik", "StatusId", "dbo.Status", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Zapisnik", "StatusId", "dbo.Status");
            DropIndex("dbo.Zapisnik", new[] { "StatusId" });
            DropColumn("dbo.Zapisnik", "StatusId");
        }
    }
}
