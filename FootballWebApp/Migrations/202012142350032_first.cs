namespace FootballWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.goals",
                c => new
                    {
                        goal_id = c.Int(nullable: false, identity: true),
                        match_id = c.Int(),
                        player_id = c.Int(),
                    })
                .PrimaryKey(t => t.goal_id)
                .ForeignKey("dbo.matches", t => t.match_id, cascadeDelete: true)
                .ForeignKey("dbo.players", t => t.player_id, cascadeDelete: true)
                .Index(t => t.match_id)
                .Index(t => t.player_id);
            
            CreateTable(
                "dbo.matches",
                c => new
                    {
                        match_id = c.Int(nullable: false, identity: true),
                        team1_score = c.Int(),
                        team2_score = c.Int(),
                        date = c.String(maxLength: 50),
                        status = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.match_id);
            
            CreateTable(
                "dbo.red_cards",
                c => new
                    {
                        red_card_id = c.Int(nullable: false, identity: true),
                        match_id = c.Int(),
                        player_id = c.Int(),
                        team_id = c.Int(),
                    })
                .PrimaryKey(t => t.red_card_id)
                .ForeignKey("dbo.players", t => t.player_id)
                .ForeignKey("dbo.teams", t => t.team_id, cascadeDelete: true)
                .ForeignKey("dbo.matches", t => t.match_id, cascadeDelete: true)
                .Index(t => t.match_id)
                .Index(t => t.player_id)
                .Index(t => t.team_id);
            
            CreateTable(
                "dbo.players",
                c => new
                    {
                        player_id = c.Int(nullable: false, identity: true),
                        fullname = c.String(maxLength: 255),
                        age = c.Int(),
                        number = c.Int(),
                        team_id = c.Int(),
                    })
                .PrimaryKey(t => t.player_id)
                .ForeignKey("dbo.teams", t => t.team_id, cascadeDelete: true)
                .Index(t => t.team_id);
            
            CreateTable(
                "dbo.teams",
                c => new
                    {
                        team_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 255),
                        coach = c.String(maxLength: 255),
                        goals_for = c.Int(),
                        goals_against = c.Int(),
                        points = c.Int(),
                        wins = c.Int(),
                        draws = c.Int(),
                        loss = c.Int(),
                        league_id = c.Int(),
                    })
                .PrimaryKey(t => t.team_id)
                .ForeignKey("dbo.leagues", t => t.league_id, cascadeDelete: true)
                .Index(t => t.league_id);
            
            CreateTable(
                "dbo.leagues",
                c => new
                    {
                        league_id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 255),
                        countries = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.league_id);
            
            CreateTable(
                "dbo.TeamMatches",
                c => new
                    {
                        match_id = c.Int(nullable: false),
                        team_id = c.Int(nullable: false),
                        home_Away = c.String(),
                    })
                .PrimaryKey(t => new { t.match_id, t.team_id })
                .ForeignKey("dbo.matches", t => t.match_id, cascadeDelete: true)
                .ForeignKey("dbo.teams", t => t.team_id, cascadeDelete: true)
                .Index(t => t.match_id)
                .Index(t => t.team_id);
            
            CreateTable(
                "dbo.yellow_cards",
                c => new
                    {
                        yellow_card_id = c.Int(nullable: false, identity: true),
                        match_id = c.Int(),
                        player_id = c.Int(),
                        team_id = c.Int(),
                    })
                .PrimaryKey(t => t.yellow_card_id)
                .ForeignKey("dbo.players", t => t.player_id)
                .ForeignKey("dbo.teams", t => t.team_id, cascadeDelete: true)
                .ForeignKey("dbo.matches", t => t.match_id, cascadeDelete: true)
                .Index(t => t.match_id)
                .Index(t => t.player_id)
                .Index(t => t.team_id);
            
            CreateTable(
                "dbo.posts",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        post_title = c.String(nullable: false),
                        post_content = c.String(unicode: false, storeType: "text"),
                        post_image = c.String(maxLength: 50),
                        post_type = c.String(maxLength: 50),
                        post_date = c.DateTime(nullable: false),
                        updated_at = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.tags",
                c => new
                    {
                        id = c.Int(nullable: false),
                        tag_title = c.String(),
                        post_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.posts", t => t.post_id, cascadeDelete: true)
                .Index(t => t.post_id);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tags", "post_id", "dbo.posts");
            DropForeignKey("dbo.yellow_cards", "match_id", "dbo.matches");
            DropForeignKey("dbo.red_cards", "match_id", "dbo.matches");
            DropForeignKey("dbo.yellow_cards", "team_id", "dbo.teams");
            DropForeignKey("dbo.yellow_cards", "player_id", "dbo.players");
            DropForeignKey("dbo.TeamMatches", "team_id", "dbo.teams");
            DropForeignKey("dbo.TeamMatches", "match_id", "dbo.matches");
            DropForeignKey("dbo.red_cards", "team_id", "dbo.teams");
            DropForeignKey("dbo.players", "team_id", "dbo.teams");
            DropForeignKey("dbo.teams", "league_id", "dbo.leagues");
            DropForeignKey("dbo.red_cards", "player_id", "dbo.players");
            DropForeignKey("dbo.goals", "player_id", "dbo.players");
            DropForeignKey("dbo.goals", "match_id", "dbo.matches");
            DropIndex("dbo.tags", new[] { "post_id" });
            DropIndex("dbo.yellow_cards", new[] { "team_id" });
            DropIndex("dbo.yellow_cards", new[] { "player_id" });
            DropIndex("dbo.yellow_cards", new[] { "match_id" });
            DropIndex("dbo.TeamMatches", new[] { "team_id" });
            DropIndex("dbo.TeamMatches", new[] { "match_id" });
            DropIndex("dbo.teams", new[] { "league_id" });
            DropIndex("dbo.players", new[] { "team_id" });
            DropIndex("dbo.red_cards", new[] { "team_id" });
            DropIndex("dbo.red_cards", new[] { "player_id" });
            DropIndex("dbo.red_cards", new[] { "match_id" });
            DropIndex("dbo.goals", new[] { "player_id" });
            DropIndex("dbo.goals", new[] { "match_id" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.tags");
            DropTable("dbo.posts");
            DropTable("dbo.yellow_cards");
            DropTable("dbo.TeamMatches");
            DropTable("dbo.leagues");
            DropTable("dbo.teams");
            DropTable("dbo.players");
            DropTable("dbo.red_cards");
            DropTable("dbo.matches");
            DropTable("dbo.goals");
        }
    }
}
