namespace FootballWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MatchesDateEdit : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.matches", "date", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.matches", "date", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
