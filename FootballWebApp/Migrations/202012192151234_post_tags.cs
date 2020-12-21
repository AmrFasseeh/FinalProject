namespace FootballWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class post_tags : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tags", "post_id", "dbo.posts");
            DropIndex("dbo.tags", new[] { "post_id" });
            CreateTable(
                "dbo.PostTags",
                c => new
                    {
                        post_id = c.Int(nullable: false),
                        tag_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.post_id, t.tag_id })
                .ForeignKey("dbo.posts", t => t.post_id)
                .ForeignKey("dbo.tags", t => t.tag_id)
                .Index(t => t.post_id)
                .Index(t => t.tag_id);
            
            DropColumn("dbo.tags", "post_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tags", "post_id", c => c.Int(nullable: false));
            DropForeignKey("dbo.PostTags", "tag_id", "dbo.tags");
            DropForeignKey("dbo.PostTags", "post_id", "dbo.posts");
            DropIndex("dbo.PostTags", new[] { "tag_id" });
            DropIndex("dbo.PostTags", new[] { "post_id" });
            DropTable("dbo.PostTags");
            CreateIndex("dbo.tags", "post_id");
            AddForeignKey("dbo.tags", "post_id", "dbo.posts", "id", cascadeDelete: true);
        }
    }
}
