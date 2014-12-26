namespace TaxCalculator.Dal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTaxThreshold : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TaxThreshold", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TaxThreshold", "IsActive");
        }
    }
}
