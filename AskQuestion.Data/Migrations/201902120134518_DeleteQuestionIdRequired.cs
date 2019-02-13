namespace AskQuestion.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteQuestionIdRequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions", "Id", "dbo.User");
            DropIndex("dbo.Questions", new[] { "Id" });
            AlterColumn("dbo.Questions", "Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Questions", "Id");
            AddForeignKey("dbo.Questions", "Id", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "Id", "dbo.User");
            DropIndex("dbo.Questions", new[] { "Id" });
            AlterColumn("dbo.Questions", "Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Questions", "Id");
            AddForeignKey("dbo.Questions", "Id", "dbo.User", "Id", cascadeDelete: true);
        }
    }
}
