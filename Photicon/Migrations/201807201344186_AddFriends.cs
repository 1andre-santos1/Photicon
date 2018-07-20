namespace Photicon.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFriends : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Users_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Users_Id1", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Users_Id2", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "Users_Id");
            CreateIndex("dbo.AspNetUsers", "Users_Id1");
            CreateIndex("dbo.AspNetUsers", "Users_Id2");
            AddForeignKey("dbo.AspNetUsers", "Users_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "Users_Id1", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "Users_Id2", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Users_Id2", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Users_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Users_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AspNetUsers", new[] { "Users_Id2" });
            DropIndex("dbo.AspNetUsers", new[] { "Users_Id1" });
            DropIndex("dbo.AspNetUsers", new[] { "Users_Id" });
            DropColumn("dbo.AspNetUsers", "Users_Id2");
            DropColumn("dbo.AspNetUsers", "Users_Id1");
            DropColumn("dbo.AspNetUsers", "Users_Id");
        }
    }
}
