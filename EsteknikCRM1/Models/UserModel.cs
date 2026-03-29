using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EsteknikCRM1.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string UserMail { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string FullName { get; set; }
        public string Department { get; set; }

        public string DisplayRole
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(UserRole) && !string.IsNullOrWhiteSpace(Department))
                    return UserRole + ", " + Department;

                if (!string.IsNullOrWhiteSpace(UserRole))
                    return UserRole;

                if (!string.IsNullOrWhiteSpace(Department))
                    return Department;

                return "-";
            }
        }
    }

}
