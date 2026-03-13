using Google.Cloud.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteknikCRM1.Models
{
    public class WorkflowModel
    {
        public string Id { get; set; }
        public string WorkflowStatus { get; set; }
        public string FlowType { get; set; }
        public string LastTransaction { get; set; }
        public string Subject { get; set; }
        public CustomerModel Customer { get; set; }
        public DateTime CreatedDate { get; set; }
        public UserModel CreatedBy { get; set; }
        public string StartType { get; set;}


    }
}
