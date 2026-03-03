using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteknikCRM1.Models
{
    public class UserModel
    {
        public string UserMail { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
