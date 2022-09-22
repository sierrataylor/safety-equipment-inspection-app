namespace SafetyEquipmentInspectionAPI.DTOs
{
    public class InspectionDto
    {
        public string InspectionId { get; set; }
        public string EquipmentId { get; set; }
        public DateTime LastInspectionDate { get; set; }
        public string InspectionResult { get; set; }
        public int ReviewerId { get; set; }
    }
}
