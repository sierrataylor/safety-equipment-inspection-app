using Google.Cloud.Firestore;

namespace SafetyEquipmentInspectionAPI.DTOs
{
    [FirestoreData]
    public class InspectionDto
    {
        [FirestoreProperty]
        public string InspectionId { get; set; }
        [FirestoreProperty]
        public string EquipmentId { get; set; }
        [FirestoreProperty]
        public DateTime LastInspectionDate { get; set; }
        [FirestoreProperty]
        public Boolean PassedInspection { get; set; }
        [FirestoreProperty]
        public string ReviewerId { get; set; }
    }
}
