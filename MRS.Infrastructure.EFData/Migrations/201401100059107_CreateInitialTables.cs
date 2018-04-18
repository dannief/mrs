namespace MRS.Infrastructure.EFData.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateInitialTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        ParentLocation_ID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Locations", t => t.ParentLocation_ID)
                .Index(t => t.ParentLocation_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        IDNumber = c.String(nullable: false, maxLength: 128),
                        EmailAddress = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        SupervisedLocation_ID = c.String(maxLength: 128),
                        Supervisor_IDNumber = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IDNumber)
                .ForeignKey("dbo.Locations", t => t.SupervisedLocation_ID)
                .ForeignKey("dbo.Users", t => t.Supervisor_IDNumber)
                .Index(t => t.SupervisedLocation_ID)
                .Index(t => t.Supervisor_IDNumber);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        ParentCategory_ID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Categories", t => t.ParentCategory_ID)
                .Index(t => t.ParentCategory_ID);
            
            CreateTable(
                "dbo.Requests",
                c => new
                    {
                        RequestNumber = c.Guid(nullable: false),
                        Title = c.String(),
                        Description = c.String(),
                        RequestDate = c.DateTime(nullable: false),
                        Severity = c.Int(nullable: false),
                        Category_ID = c.String(maxLength: 128),
                        Requester_IDNumber = c.String(maxLength: 128),
                        LocationToService_ID = c.String(maxLength: 128),
                        WorkOrder_WorkOrderNumber = c.Guid(),
                        State_ID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.RequestNumber)
                .ForeignKey("dbo.Categories", t => t.Category_ID)
                .ForeignKey("dbo.Users", t => t.Requester_IDNumber)
                .ForeignKey("dbo.Locations", t => t.LocationToService_ID)
                .ForeignKey("dbo.WorkOrders", t => t.WorkOrder_WorkOrderNumber)
                .ForeignKey("dbo.RequestStates", t => t.State_ID)
                .Index(t => t.Category_ID)
                .Index(t => t.Requester_IDNumber)
                .Index(t => t.LocationToService_ID)
                .Index(t => t.WorkOrder_WorkOrderNumber)
                .Index(t => t.State_ID);
            
            CreateTable(
                "dbo.WorkOrders",
                c => new
                    {
                        WorkOrderNumber = c.Guid(nullable: false),
                        Description = c.String(),
                        Priority = c.Int(nullable: false),
                        AssignedWorker_IDNumber = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.WorkOrderNumber)
                .ForeignKey("dbo.Users", t => t.AssignedWorker_IDNumber)
                .Index(t => t.AssignedWorker_IDNumber);
            
            CreateTable(
                "dbo.RequestStates",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.RequestChangeLogs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ChangeID = c.String(),
                        RequestNumber = c.String(),
                        UserIDNumber = c.String(),
                        Timestamp = c.DateTime(nullable: false),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Tenancies",
                c => new
                    {
                        User_IDNumber = c.String(nullable: false, maxLength: 128),
                        Room_ID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.User_IDNumber, t.Room_ID })
                .ForeignKey("dbo.Users", t => t.User_IDNumber, cascadeDelete: true)
                .ForeignKey("dbo.Locations", t => t.Room_ID, cascadeDelete: true)
                .Index(t => t.User_IDNumber)
                .Index(t => t.Room_ID);
            
            CreateTable(
                "dbo.WorkerCategories",
                c => new
                    {
                        Worker_IDNumber = c.String(nullable: false, maxLength: 128),
                        Category_ID = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Worker_IDNumber, t.Category_ID })
                .ForeignKey("dbo.Users", t => t.Worker_IDNumber, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.Category_ID, cascadeDelete: true)
                .Index(t => t.Worker_IDNumber)
                .Index(t => t.Category_ID);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.WorkerCategories", new[] { "Category_ID" });
            DropIndex("dbo.WorkerCategories", new[] { "Worker_IDNumber" });
            DropIndex("dbo.Tenancies", new[] { "Room_ID" });
            DropIndex("dbo.Tenancies", new[] { "User_IDNumber" });
            DropIndex("dbo.WorkOrders", new[] { "AssignedWorker_IDNumber" });
            DropIndex("dbo.Requests", new[] { "State_ID" });
            DropIndex("dbo.Requests", new[] { "WorkOrder_WorkOrderNumber" });
            DropIndex("dbo.Requests", new[] { "LocationToService_ID" });
            DropIndex("dbo.Requests", new[] { "Requester_IDNumber" });
            DropIndex("dbo.Requests", new[] { "Category_ID" });
            DropIndex("dbo.Categories", new[] { "ParentCategory_ID" });
            DropIndex("dbo.Users", new[] { "Supervisor_IDNumber" });
            DropIndex("dbo.Users", new[] { "SupervisedLocation_ID" });
            DropIndex("dbo.Locations", new[] { "ParentLocation_ID" });
            DropForeignKey("dbo.WorkerCategories", "Category_ID", "dbo.Categories");
            DropForeignKey("dbo.WorkerCategories", "Worker_IDNumber", "dbo.Users");
            DropForeignKey("dbo.Tenancies", "Room_ID", "dbo.Locations");
            DropForeignKey("dbo.Tenancies", "User_IDNumber", "dbo.Users");
            DropForeignKey("dbo.WorkOrders", "AssignedWorker_IDNumber", "dbo.Users");
            DropForeignKey("dbo.Requests", "State_ID", "dbo.RequestStates");
            DropForeignKey("dbo.Requests", "WorkOrder_WorkOrderNumber", "dbo.WorkOrders");
            DropForeignKey("dbo.Requests", "LocationToService_ID", "dbo.Locations");
            DropForeignKey("dbo.Requests", "Requester_IDNumber", "dbo.Users");
            DropForeignKey("dbo.Requests", "Category_ID", "dbo.Categories");
            DropForeignKey("dbo.Categories", "ParentCategory_ID", "dbo.Categories");
            DropForeignKey("dbo.Users", "Supervisor_IDNumber", "dbo.Users");
            DropForeignKey("dbo.Users", "SupervisedLocation_ID", "dbo.Locations");
            DropForeignKey("dbo.Locations", "ParentLocation_ID", "dbo.Locations");
            DropTable("dbo.WorkerCategories");
            DropTable("dbo.Tenancies");
            DropTable("dbo.RequestChangeLogs");
            DropTable("dbo.RequestStates");
            DropTable("dbo.WorkOrders");
            DropTable("dbo.Requests");
            DropTable("dbo.Categories");
            DropTable("dbo.Users");
            DropTable("dbo.Locations");
        }
    }
}
