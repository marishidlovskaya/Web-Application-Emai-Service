namespace WebApplicationEmailService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Email : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateMessage = c.DateTime(nullable: false),
                        TitleMessage = c.String(maxLength: 50),
                        TextMessage = c.String(maxLength: 400),
                        User_Id = c.Int(),
                        Receiver_Id = c.Int(),
                        Sender_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.User_Id)
                .ForeignKey("dbo.User", t => t.Receiver_Id)
                .ForeignKey("dbo.User", t => t.Sender_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Receiver_Id)
                .Index(t => t.Sender_Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Message", "Sender_Id", "dbo.User");
            DropForeignKey("dbo.Message", "Receiver_Id", "dbo.User");
            DropForeignKey("dbo.Message", "User_Id", "dbo.User");
            DropIndex("dbo.Message", new[] { "Sender_Id" });
            DropIndex("dbo.Message", new[] { "Receiver_Id" });
            DropIndex("dbo.Message", new[] { "User_Id" });
            DropTable("dbo.User");
            DropTable("dbo.Message");
        }
    }
}
