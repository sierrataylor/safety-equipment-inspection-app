using Google.Cloud.Firestore;

namespace SafetyEquipmentInspectionAPI.DTOs
{
    [FirestoreData]
    public class EmployeeDto
    {
        [FirestoreProperty]
        public string EmployeeId { get; set; }
        [FirestoreProperty]
        public string FirstName { get; set; }
        [FirestoreProperty]
        public string LastName { get; set; }
        [FirestoreProperty]
        public string Email { get; set; }
        [FirestoreProperty]
        public string Role { get; set; }
        [FirestoreProperty]
        public string Password { get; set; }
        [FirestoreProperty]
        public Boolean IsAdmin { get; set; }
        [FirestoreProperty]
        public Boolean IsSuperAdmin { get; set; }
    }
}
