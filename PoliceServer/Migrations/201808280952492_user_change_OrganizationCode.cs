namespace PoliceServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class user_change_OrganizationCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "OrganizationCode", c => c.String());
            DropColumn("dbo.User", "NzCoding");
        }
        
        public override void Down()
        {
            AddColumn("dbo.User", "NzCoding", c => c.String());
            DropColumn("dbo.User", "OrganizationCode");
        }
    }
}
