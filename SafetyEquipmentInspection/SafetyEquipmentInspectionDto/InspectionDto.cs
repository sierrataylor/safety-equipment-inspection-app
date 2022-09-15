namespace SafetyEquipmentInspectionDto
{
    public class InspectionDto
    {
        public int InspectionId { get; set; }
        public int EquipmentId { get; set; }
        public DateTime LastInspectionDate { get; set; }
        public string InspectionResult { get; set; }
        public int ReviewerId { get; set; }
    }
}
