namespace Photicon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedComments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        Date = c.DateTime(nullable: false),
                        PictureFK = c.Int(nullable: false),
                        UserFK = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Pictures", t => t.PictureFK, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserFK)
                .Index(t => t.PictureFK)
                .Index(t => t.UserFK);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "UserFK", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "PictureFK", "dbo.Pictures");
            DropIndex("dbo.Comments", new[] { "UserFK" });
            DropIndex("dbo.Comments", new[] { "PictureFK" });
            DropTable("dbo.Comments");
        }
    }
}
