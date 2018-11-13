namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asdff : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ogloszenie_dla_rodzicow", "Rodzic_ID", "dbo.Rodzic");
            DropIndex("dbo.Ogloszenie_dla_rodzicow", new[] { "Rodzic_ID" });
            RenameColumn(table: "dbo.Ogloszenie_dla_rodzicow", name: "Rodzic_ID", newName: "RodzicID");
            AlterColumn("dbo.Ogloszenie_dla_rodzicow", "RodzicID", c => c.Int(nullable: false));
            CreateIndex("dbo.Ogloszenie_dla_rodzicow", "RodzicID");
            AddForeignKey("dbo.Ogloszenie_dla_rodzicow", "RodzicID", "dbo.Rodzic", "ID", cascadeDelete: true);
            DropColumn("dbo.Ogloszenie_dla_rodzicow", "IDRodzic");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ogloszenie_dla_rodzicow", "IDRodzic", c => c.Int(nullable: false));
            DropForeignKey("dbo.Ogloszenie_dla_rodzicow", "RodzicID", "dbo.Rodzic");
            DropIndex("dbo.Ogloszenie_dla_rodzicow", new[] { "RodzicID" });
            AlterColumn("dbo.Ogloszenie_dla_rodzicow", "RodzicID", c => c.Int());
            RenameColumn(table: "dbo.Ogloszenie_dla_rodzicow", name: "RodzicID", newName: "Rodzic_ID");
            CreateIndex("dbo.Ogloszenie_dla_rodzicow", "Rodzic_ID");
            AddForeignKey("dbo.Ogloszenie_dla_rodzicow", "Rodzic_ID", "dbo.Rodzic", "ID");
        }
    }
}
