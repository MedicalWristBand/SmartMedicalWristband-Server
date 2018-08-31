namespace PoliceServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ticket : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Ticket",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Serial = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        UserID = c.Int(nullable: false),
                        RoleType = c.Int(nullable: false),
                        ExpirationDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserID)
                .Index(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ticket", "UserID", "dbo.User");
            DropIndex("dbo.Ticket", new[] { "UserID" });
            DropTable("dbo.Ticket");
        }
    }
}
