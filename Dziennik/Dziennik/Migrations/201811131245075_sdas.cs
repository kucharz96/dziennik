namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdas : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ogloszenie_dla_rodzicow", "Nauczyciel_ID", "dbo.Nauczyciel");
            DropIndex("dbo.Ogloszenie_dla_rodzicow", new[] { "Nauczyciel_ID" });
            RenameColumn(table: "dbo.Ogloszenie_dla_rodzicow", name: "Nauczyciel_ID", newName: "NauczycielID");
            AlterColumn("dbo.Ogloszenie_dla_rodzicow", "NauczycielID", c => c.Int(nullable: false));
            CreateIndex("dbo.Ogloszenie_dla_rodzicow", "NauczycielID");
            AddForeignKey("dbo.Ogloszenie_dla_rodzicow", "NauczycielID", "dbo.Nauczyciel", "ID", cascadeDelete: true);
            DropColumn("dbo.Ogloszenie_dla_rodzicow", "IDNauczyciel");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ogloszenie_dla_rodzicow", "IDNauczyciel", c => c.Int(nullable: false));
            DropForeignKey("dbo.Ogloszenie_dla_rodzicow", "NauczycielID", "dbo.Nauczyciel");
            DropIndex("dbo.Ogloszenie_dla_rodzicow", new[] { "NauczycielID" });
            AlterColumn("dbo.Ogloszenie_dla_rodzicow", "NauczycielID", c => c.Int());
            RenameColumn(table: "dbo.Ogloszenie_dla_rodzicow", name: "NauczycielID", newName: "Nauczyciel_ID");
            CreateIndex("dbo.Ogloszenie_dla_rodzicow", "Nauczyciel_ID");
            AddForeignKey("dbo.Ogloszenie_dla_rodzicow", "Nauczyciel_ID", "dbo.Nauczyciel", "ID");
        }
    }
}
