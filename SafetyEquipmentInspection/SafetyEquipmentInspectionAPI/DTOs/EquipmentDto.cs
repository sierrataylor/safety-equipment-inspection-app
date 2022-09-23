namespace SafetyEquipmentInspectionAPI.DTOs
{
    public class EquipmentDto
    {
        public Guid EquipmentId { get; set; }
        public string EquipmentType { get; set; }
        public string Location { get; set; }
        public string Building { get; set; }
        public int Floor { get; set; }
    }
}
