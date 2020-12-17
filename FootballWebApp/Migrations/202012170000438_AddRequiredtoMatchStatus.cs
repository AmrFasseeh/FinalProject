namespace FootballWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredtoMatchStatus : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.matches", "status", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.matches", "status", c => c.String(maxLength: 50));
        }
    }
}
