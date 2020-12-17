namespace FootballWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRequiredToSomeProperties : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.matches", "date", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.players", "fullname", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.players", "age", c => c.Int(nullable: false));
            AlterColumn("dbo.players", "number", c => c.Int(nullable: false));
            AlterColumn("dbo.teams", "name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.teams", "coach", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.leagues", "name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.leagues", "countries", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.posts", "post_content", c => c.String(nullable: false, unicode: false, storeType: "text"));
            AlterColumn("dbo.posts", "post_type", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.tags", "tag_title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tags", "tag_title", c => c.String());
            AlterColumn("dbo.posts", "post_type", c => c.String(maxLength: 50));
            AlterColumn("dbo.posts", "post_content", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("dbo.leagues", "countries", c => c.String(maxLength: 255));
            AlterColumn("dbo.leagues", "name", c => c.String(maxLength: 255));
            AlterColumn("dbo.teams", "coach", c => c.String(maxLength: 255));
            AlterColumn("dbo.teams", "name", c => c.String(maxLength: 255));
            AlterColumn("dbo.players", "number", c => c.Int());
            AlterColumn("dbo.players", "age", c => c.Int());
            AlterColumn("dbo.players", "fullname", c => c.String(maxLength: 255));
            AlterColumn("dbo.matches", "date", c => c.String(maxLength: 50));
        }
    }
}
