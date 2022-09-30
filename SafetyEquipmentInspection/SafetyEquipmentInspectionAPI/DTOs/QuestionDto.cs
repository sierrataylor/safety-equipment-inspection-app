using Google.Cloud.Firestore;

namespace SafetyEquipmentInspectionAPI.DTOs
{
    [FirestoreData]
    public class QuestionDto
    {
        [FirestoreProperty]
        public string QuestionId { get; set; }
        [FirestoreProperty]
        public string EquipmentType { get; set; }
        [FirestoreProperty]
        public int QuestionNumber { get; set; }
        [FirestoreProperty]
        public string Field { get; set; }
    }
}
