namespace SafetyEquipmentInspectionApp.Models
{
    public class Inspection
    {
        public virtual int InspectionId { get; set; }
        public virtual int EquipmentId { get; set; }
        public virtual DateTime LastInspectionDate { get; set; }
        public virtual string InspectionResult { get; set; }
        public virtual int ReviewerId { get; set; }
    }
}
