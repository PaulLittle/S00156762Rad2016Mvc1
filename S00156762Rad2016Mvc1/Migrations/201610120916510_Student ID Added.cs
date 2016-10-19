namespace S00156762Rad2016Mvc1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class StudentIDAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "StudentID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "StudentID");
        }
    }
}
