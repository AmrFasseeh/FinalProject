namespace FootballWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addapis : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.tags");
            AlterColumn("dbo.matches", "team1_score", c => c.Int(nullable: false));
            AlterColumn("dbo.matches", "team2_score", c => c.Int(nullable: false));
            AlterColumn("dbo.matches", "date", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.matches", "status", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.players", "fullname", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.players", "age", c => c.Int(nullable: false));
            AlterColumn("dbo.players", "number", c => c.Int(nullable: false));
            AlterColumn("dbo.teams", "name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.teams", "coach", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.teams", "goals_for", c => c.Int(nullable: false));
            AlterColumn("dbo.teams", "goals_against", c => c.Int(nullable: false));
            AlterColumn("dbo.teams", "points", c => c.Int(nullable: false));
            AlterColumn("dbo.teams", "wins", c => c.Int(nullable: false));
            AlterColumn("dbo.teams", "draws", c => c.Int(nullable: false));
            AlterColumn("dbo.teams", "loss", c => c.Int(nullable: false));
            AlterColumn("dbo.leagues", "name", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.leagues", "countries", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.posts", "post_content", c => c.String(nullable: false, unicode: false, storeType: "text"));
            AlterColumn("dbo.posts", "post_type", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.posts", "post_date", c => c.String());
            AlterColumn("dbo.posts", "updated_at", c => c.String());
            AlterColumn("dbo.tags", "id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.tags", "tag_title", c => c.String(nullable: false));
            AddPrimaryKey("dbo.tags", "id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.tags");
            AlterColumn("dbo.tags", "tag_title", c => c.String());
            AlterColumn("dbo.tags", "id", c => c.Int(nullable: false));
            AlterColumn("dbo.posts", "updated_at", c => c.DateTime(nullable: false));
            AlterColumn("dbo.posts", "post_date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.posts", "post_type", c => c.String(maxLength: 50));
            AlterColumn("dbo.posts", "post_content", c => c.String(unicode: false, storeType: "text"));
            AlterColumn("dbo.leagues", "countries", c => c.String(maxLength: 255));
            AlterColumn("dbo.leagues", "name", c => c.String(maxLength: 255));
            AlterColumn("dbo.teams", "loss", c => c.Int());
            AlterColumn("dbo.teams", "draws", c => c.Int());
            AlterColumn("dbo.teams", "wins", c => c.Int());
            AlterColumn("dbo.teams", "points", c => c.Int());
            AlterColumn("dbo.teams", "goals_against", c => c.Int());
            AlterColumn("dbo.teams", "goals_for", c => c.Int());
            AlterColumn("dbo.teams", "coach", c => c.String(maxLength: 255));
            AlterColumn("dbo.teams", "name", c => c.String(maxLength: 255));
            AlterColumn("dbo.players", "number", c => c.Int());
            AlterColumn("dbo.players", "age", c => c.Int());
            AlterColumn("dbo.players", "fullname", c => c.String(maxLength: 255));
            AlterColumn("dbo.matches", "status", c => c.String(maxLength: 50));
            AlterColumn("dbo.matches", "date", c => c.String(maxLength: 50));
            AlterColumn("dbo.matches", "team2_score", c => c.Int());
            AlterColumn("dbo.matches", "team1_score", c => c.Int());
            AddPrimaryKey("dbo.tags", "id");
        }
    }
}
