namespace EsteknikCRM.Api.Entities
{
    public class User
    {
        public required string Id { get; set; }
        public string UserMail { get; set; }
        public string Password { get; set; }
        public string UserRole { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }

        public string Surname { get; set; }

        public string FullName => $"{Name} {MiddleName} {Surname}".Trim();

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
