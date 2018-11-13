namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdsd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Uwaga", "Uczen_ID", "dbo.Uczen");
            DropForeignKey("dbo.Uwaga", "Nauczyciel_ID", "dbo.Nauczyciel");
            DropIndex("dbo.Uwaga", new[] { "Nauczyciel_ID" });
            DropIndex("dbo.Uwaga", new[] { "Uczen_ID" });
            RenameColumn(table: "dbo.Uwaga", name: "Uczen_ID", newName: "UczenID");
            RenameColumn(table: "dbo.Uwaga", name: "Nauczyciel_ID", newName: "NauczycielID");
            AlterColumn("dbo.Uwaga", "NauczycielID", c => c.Int(nullable: false));
            AlterColumn("dbo.Uwaga", "UczenID", c => c.Int(nullable: false));
            CreateIndex("dbo.Uwaga", "NauczycielID");
            CreateIndex("dbo.Uwaga", "UczenID");
            AddForeignKey("dbo.Uwaga", "UczenID", "dbo.Uczen", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Uwaga", "NauczycielID", "dbo.Nauczyciel", "ID", cascadeDelete: true);
            DropColumn("dbo.Uwaga", "IDNauczyciel");
            DropColumn("dbo.Uwaga", "IDUczen");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Uwaga", "IDUczen", c => c.Int(nullable: false));
            AddColumn("dbo.Uwaga", "IDNauczyciel", c => c.Int(nullable: false));
            DropForeignKey("dbo.Uwaga", "NauczycielID", "dbo.Nauczyciel");
            DropForeignKey("dbo.Uwaga", "UczenID", "dbo.Uczen");
            DropIndex("dbo.Uwaga", new[] { "UczenID" });
            DropIndex("dbo.Uwaga", new[] { "NauczycielID" });
            AlterColumn("dbo.Uwaga", "UczenID", c => c.Int());
            AlterColumn("dbo.Uwaga", "NauczycielID", c => c.Int());
            RenameColumn(table: "dbo.Uwaga", name: "NauczycielID", newName: "Nauczyciel_ID");
            RenameColumn(table: "dbo.Uwaga", name: "UczenID", newName: "Uczen_ID");
            CreateIndex("dbo.Uwaga", "Uczen_ID");
            CreateIndex("dbo.Uwaga", "Nauczyciel_ID");
            AddForeignKey("dbo.Uwaga", "Nauczyciel_ID", "dbo.Nauczyciel", "ID");
            AddForeignKey("dbo.Uwaga", "Uczen_ID", "dbo.Uczen", "ID");
        }
    }
}
