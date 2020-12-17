namespace FootballWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PostDatesEdit : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.posts", "post_date", c => c.String());
            AlterColumn("dbo.posts", "updated_at", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.posts", "updated_at", c => c.DateTime(nullable: false));
            AlterColumn("dbo.posts", "post_date", c => c.DateTime(nullable: false));
        }
    }
}
