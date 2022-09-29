using Google.Cloud.Firestore;

namespace SafetyEquipmentInspectionAPI.DTOs
{
    [FirestoreData]
    public class AnswerDto
    {
        [FirestoreProperty]
        public Guid AnswerId { get; set; }
        [FirestoreProperty]
        public string EquipmentId { get; set; }
        [FirestoreProperty]
        public int QuestionNumber { get; set; }
        [FirestoreProperty]
        public string Response { get; set; }
    }
}
