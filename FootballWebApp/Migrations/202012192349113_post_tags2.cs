namespace FootballWebApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class post_tags2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PostTags", "Post_id", "dbo.posts");
            DropForeignKey("dbo.PostTags", "Tag_id", "dbo.tags");
            DropIndex("dbo.PostTags", new[] { "Post_id" });
            DropIndex("dbo.PostTags", new[] { "Tag_id" });
            RenameColumn(table: "dbo.PostTags", name: "Post_id", newName: "postId");
            RenameColumn(table: "dbo.PostTags", name: "Tag_id", newName: "tagId");
            DropPrimaryKey("dbo.PostTags");
            AlterColumn("dbo.PostTags", "postId", c => c.Int(nullable: false));
            AlterColumn("dbo.PostTags", "tagId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.PostTags", new[] { "postId", "tagId" });
            CreateIndex("dbo.PostTags", "postId");
            CreateIndex("dbo.PostTags", "tagId");
            AddForeignKey("dbo.PostTags", "postId", "dbo.posts", "id", cascadeDelete: true);
            AddForeignKey("dbo.PostTags", "tagId", "dbo.tags", "id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostTags", "tagId", "dbo.tags");
            DropForeignKey("dbo.PostTags", "postId", "dbo.posts");
            DropIndex("dbo.PostTags", new[] { "tagId" });
            DropIndex("dbo.PostTags", new[] { "postId" });
            DropPrimaryKey("dbo.PostTags");
            AlterColumn("dbo.PostTags", "tagId", c => c.Int());
            AlterColumn("dbo.PostTags", "postId", c => c.Int());
            AddPrimaryKey("dbo.PostTags", new[] { "post_id", "tag_id" });
            RenameColumn(table: "dbo.PostTags", name: "tagId", newName: "Tag_id");
            RenameColumn(table: "dbo.PostTags", name: "postId", newName: "Post_id");
            CreateIndex("dbo.PostTags", "Tag_id");
            CreateIndex("dbo.PostTags", "Post_id");
            AddForeignKey("dbo.PostTags", "Tag_id", "dbo.tags", "id");
            AddForeignKey("dbo.PostTags", "Post_id", "dbo.posts", "id");
        }
    }
}
