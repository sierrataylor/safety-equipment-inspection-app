namespace SafetyEquipmentInspectionApp.Models
{
    public class Inspection
    {
        public virtual string InspectionId { get; set; }
        public virtual string EquipmentId { get; set; }
        public virtual DateTime LastInspectionDate { get; set; }
        public virtual string PassedInspection { get; set; }
        public virtual int ReviewerId { get; set; }
    }
}
