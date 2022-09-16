namespace SafetyEquipmentInspectionApp.Models
{
    public class Equipment
    {
        public virtual string EquipmentID { get; set; }
        public virtual string EquipmentType { get; set; }
        public virtual string Location { get; set; }
        public virtual string Building { get; set; }
        public virtual int Floor { get; set; }
    }
}
