namespace SafetyEquipmentInspectionAPP.Models
{
    public class Equipment
    {
        public virtual int EquipmentID { get; set; }
        public virtual string EquipmentType { get; set; }
        public virtual string Location { get; set; }
        public virtual string Building { get; set; }
        public virtual string Floor { get; set; }
    }
}
