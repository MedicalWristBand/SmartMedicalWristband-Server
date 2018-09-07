namespace PoliceServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class doctorOptional : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Patient", new[] { "DoctorID" });
            AlterColumn("dbo.Patient", "DoctorID", c => c.Int());
            CreateIndex("dbo.Patient", "DoctorID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Patient", new[] { "DoctorID" });
            AlterColumn("dbo.Patient", "DoctorID", c => c.Int(nullable: false));
            CreateIndex("dbo.Patient", "DoctorID");
        }
    }
}
