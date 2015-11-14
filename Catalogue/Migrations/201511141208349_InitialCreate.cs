namespace Catalogue.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Records",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Author = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rating = c.Int(nullable: false),
                        Comment = c.String(),
                        RecordId_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Records", t => t.RecordId_Id)
                .Index(t => t.RecordId_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reviews", "RecordId_Id", "dbo.Records");
            DropIndex("dbo.Reviews", new[] { "RecordId_Id" });
            DropTable("dbo.Reviews");
            DropTable("dbo.Records");
        }
    }
}
