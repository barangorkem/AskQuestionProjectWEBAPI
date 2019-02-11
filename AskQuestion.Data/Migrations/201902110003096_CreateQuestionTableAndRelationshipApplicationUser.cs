namespace AskQuestion.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateQuestionTableAndRelationshipApplicationUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        QuestionTitle = c.String(nullable: false),
                        QuestionTime = c.String(nullable: false),
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.User", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "Id", "dbo.User");
            DropIndex("dbo.Questions", new[] { "Id" });
            DropTable("dbo.Questions");
        }
    }
}
