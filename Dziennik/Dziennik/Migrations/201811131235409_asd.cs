namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Klasa", "Wychowawca_ID", "dbo.Nauczyciel");
            DropIndex("dbo.Klasa", new[] { "Wychowawca_ID" });
            RenameColumn(table: "dbo.Klasa", name: "Wychowawca_ID", newName: "NauczycielID");
            AlterColumn("dbo.Klasa", "NauczycielID", c => c.Int(nullable: false));
            CreateIndex("dbo.Klasa", "NauczycielID");
            AddForeignKey("dbo.Klasa", "NauczycielID", "dbo.Nauczyciel", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Klasa", "NauczycielID", "dbo.Nauczyciel");
            DropIndex("dbo.Klasa", new[] { "NauczycielID" });
            AlterColumn("dbo.Klasa", "NauczycielID", c => c.Int());
            RenameColumn(table: "dbo.Klasa", name: "NauczycielID", newName: "Wychowawca_ID");
            CreateIndex("dbo.Klasa", "Wychowawca_ID");
            AddForeignKey("dbo.Klasa", "Wychowawca_ID", "dbo.Nauczyciel", "ID");
        }
    }
}
