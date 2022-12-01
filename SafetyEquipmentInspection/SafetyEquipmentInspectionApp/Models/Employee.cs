namespace SafetyEquipmentInspectionApp.Models
{
    public class Employee
    {
        public virtual string EmployeeId { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Role { get; set; }
        public virtual string Password { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsAdmin { get; set; }
    }
}
