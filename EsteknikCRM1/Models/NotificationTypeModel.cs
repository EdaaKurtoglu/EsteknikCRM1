using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EsteknikCRM1.Models
{
    public class NotificationTypeModel
    {
        public string ID { get; set; }

        [JsonPropertyName("notifyType")]
        public string NotificationType {  get; set; }
    }
}
