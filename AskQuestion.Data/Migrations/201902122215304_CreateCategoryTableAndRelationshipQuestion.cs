namespace AskQuestion.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateCategoryTableAndRelationshipQuestion : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Questions", new[] { "Category_CategoryId" });
            DropColumn("dbo.Questions", "CategoryId");
            RenameColumn(table: "dbo.Questions", name: "Category_CategoryId", newName: "CategoryId");
            AlterColumn("dbo.Questions", "CategoryId", c => c.Int());
            CreateIndex("dbo.Questions", "CategoryId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Questions", new[] { "CategoryId" });
            AlterColumn("dbo.Questions", "CategoryId", c => c.String());
            RenameColumn(table: "dbo.Questions", name: "CategoryId", newName: "Category_CategoryId");
            AddColumn("dbo.Questions", "CategoryId", c => c.String());
            CreateIndex("dbo.Questions", "Category_CategoryId");
        }
    }
}
