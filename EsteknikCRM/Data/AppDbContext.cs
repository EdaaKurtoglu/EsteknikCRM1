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


    }
}