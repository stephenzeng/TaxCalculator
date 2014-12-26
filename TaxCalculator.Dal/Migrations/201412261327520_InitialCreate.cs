namespace TaxCalculator.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaxRateItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        TaxRate_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaxRate", t => t.TaxRate_Id)
                .Index(t => t.TaxRate_Id);
            
            CreateTable(
                "dbo.TaxThreshold",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Start = c.Decimal(nullable: false, precision: 18, scale: 2),
                        End = c.Decimal(precision: 18, scale: 2),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TaxRateItem_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TaxRateItem", t => t.TaxRateItem_Id)
                .Index(t => t.TaxRateItem_Id);
            
            CreateTable(
                "dbo.TaxRate",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        CreateAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaxRateItem", "TaxRate_Id", "dbo.TaxRate");
            DropForeignKey("dbo.TaxThreshold", "TaxRateItem_Id", "dbo.TaxRateItem");
            DropIndex("dbo.TaxThreshold", new[] { "TaxRateItem_Id" });
            DropIndex("dbo.TaxRateItem", new[] { "TaxRate_Id" });
            DropTable("dbo.TaxRate");
            DropTable("dbo.TaxThreshold");
            DropTable("dbo.TaxRateItem");
        }
    }
}
