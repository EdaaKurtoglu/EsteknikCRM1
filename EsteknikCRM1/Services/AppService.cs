using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EsteknikCRM1.Services.Api;
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
        public static ApiTeamService ApiTeamService { get; } = new ApiTeamService();
        public static ApiCustomerService ApiCustomerService { get; } = new ApiCustomerService();
        public static ApiAddressService ApiAddressService { get; } = new ApiAddressService();
        public static ApiWorkflowService ApiWorkflowService { get; } = new ApiWorkflowService();
        public static ApiCustomerDeviceService ApiCustomerDeviceService { get; } = new ApiCustomerDeviceService();
        public static ApiDeviceService ApiDeviceService { get; } = new ApiDeviceService();
        public static ApiLookupService ApiLookupService { get; } = new ApiLookupService();
        public static ApiAnnouncementService ApiAnnouncementService { get; } = new ApiAnnouncementService();
        public static ApiWorkflowFileService ApiWorkflowFileService { get; } = new ApiWorkflowFileService();
        public static ApiAuthService ApiAuthService { get; } = new ApiAuthService();
        public static ApiOperationService ApiOperationService { get; } = new ApiOperationService();
        public static ApiHakedisSetService ApiHakedisSetService { get; } = new ApiHakedisSetService();
        public static ApiProductService ApiProductService { get; } = new ApiProductService();
        public static ApiProductLaborPriceService ApiProductLaborPriceService { get; } = new ApiProductLaborPriceService();

    }
}
