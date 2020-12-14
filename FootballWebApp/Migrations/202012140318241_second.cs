namespace FootballWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.goals", "player_id", "dbo.players");
            AddForeignKey("dbo.goals", "player_id", "dbo.players", "player_id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.goals", "player_id", "dbo.players");
            AddForeignKey("dbo.goals", "player_id", "dbo.players", "player_id");
        }
    }
}
