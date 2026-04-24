using Microsoft.EntityFrameworkCore;
using EsteknikCRM.Api.Entities;
using EsteknikCRM.Entities;
namespace EsteknikCRM.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Workflow> Workflows { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<CustomerDevice> CustomerDevices { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<NotificationType> NotificationTypes { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<OperationPrice> OperationPrices { get; set; }
        public DbSet<WorkflowTeamOperation> WorkflowTeamOperations { get; set; }
        public DbSet<HakedisSet> HakedisSets { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<WorkflowFile> WorkflowFiles { get; set; }
        public DbSet<LaborOperation> LaborOperations { get; set; }
        public DbSet<ProductLaborPrice> ProductLaborPrices { get; set; }
        public DbSet<AnnouncementFile> AnnouncementFiles { get; set; }

    }
}