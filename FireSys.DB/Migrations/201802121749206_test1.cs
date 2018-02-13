namespace FireSys.DB.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.RadniNalog", new[] { "Status_Id" });
            RenameColumn(table: "dbo.RadniNalog", name: "Status_Id", newName: "StatusId");
            AlterColumn("dbo.RadniNalog", "StatusId", c => c.Int(nullable: true, defaultValue:2));
            CreateIndex("dbo.RadniNalog", "StatusId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.RadniNalog", new[] { "StatusId" });
            AlterColumn("dbo.RadniNalog", "StatusId", c => c.Int());
            RenameColumn(table: "dbo.RadniNalog", name: "StatusId", newName: "Status_Id");
            CreateIndex("dbo.RadniNalog", "Status_Id");
        }
    }
}
