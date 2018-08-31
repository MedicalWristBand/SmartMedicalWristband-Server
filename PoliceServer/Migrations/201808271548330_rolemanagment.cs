namespace PoliceServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rolemanagment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Patient",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Family = c.String(),
                        PatientCode = c.String(),
                        Status = c.String(),
                        DoctorID = c.Int(nullable: false),
                        LastUpdateDateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.DoctorID)
                .Index(t => t.DoctorID);
            
            DropColumn("dbo.User", "PasgahID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "PasgahID", c => c.Int(nullable: false));
            DropForeignKey("dbo.Patient", "DoctorID", "dbo.User");
            DropIndex("dbo.Patient", new[] { "DoctorID" });
            DropTable("dbo.Patient");
        }
    }
}
