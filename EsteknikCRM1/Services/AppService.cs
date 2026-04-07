using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EsteknikCRM1.Services.Infrastructure;
using Google.Cloud.Firestore;

namespace EsteknikCRM1.Services
{
    public static class AppServices
    {
        private static readonly FirebaseContext _context = new FirebaseContext();

        public static AuthService AuthService { get; } = new AuthService(_context.Db);
        public static HomeService HomeService { get; } = new HomeService(_context.Db);
        public static CustomerService CustomerService { get; } = new CustomerService(_context.Db);
        public static AddressService AddressService { get; } = new AddressService(_context.Db);
        public static DeviceService DeviceService { get; } = new DeviceService(_context.Db);
        public static WorkflowService WorkflowService { get; } = new WorkflowService(_context.Db);
        public static TeamService TeamService { get; } = new TeamService(_context.Db);
        public static LookupService LookupService { get; } = new LookupService(_context.Db);
        public static CustomerDeviceService CustomerDeviceService { get; } = new CustomerDeviceService(_context.Db);
        public static OperationService OperationService { get; } = new OperationService(_context.Db);
        public static AnnouncementService AnnouncementService { get; } = new AnnouncementService(_context.Db);


    }
}
