namespace Rent2Tour.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Cars : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CarNo = c.String(),
                        Make = c.String(),
                        Model = c.String(),
                        Available = c.Boolean(nullable: false),
                        Image = c.String(),
                        categID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.categID, cascadeDelete: true)
                .Index(t => t.categID);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustName = c.String(),
                        Address = c.String(),
                        Phone = c.String(),
                        categID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.categID, cascadeDelete: true)
                .Index(t => t.categID);
            
            CreateTable(
                "dbo.Rents",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CarID = c.Int(),
                        CategID = c.Int(),
                        CustID = c.Int(),
                        Fee = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cars", t => t.CarID)
                .ForeignKey("dbo.Categories", t => t.CategID)
                .ForeignKey("dbo.Customers", t => t.CustID)
                .Index(t => t.CarID)
                .Index(t => t.CategID)
                .Index(t => t.CustID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rents", "CustID", "dbo.Customers");
            DropForeignKey("dbo.Rents", "CategID", "dbo.Categories");
            DropForeignKey("dbo.Rents", "CarID", "dbo.Cars");
            DropForeignKey("dbo.Customers", "categID", "dbo.Categories");
            DropForeignKey("dbo.Cars", "categID", "dbo.Categories");
            DropIndex("dbo.Rents", new[] { "CustID" });
            DropIndex("dbo.Rents", new[] { "CategID" });
            DropIndex("dbo.Rents", new[] { "CarID" });
            DropIndex("dbo.Customers", new[] { "categID" });
            DropIndex("dbo.Cars", new[] { "categID" });
            DropTable("dbo.Rents");
            DropTable("dbo.Customers");
            DropTable("dbo.Categories");
            DropTable("dbo.Cars");
        }
    }
}
