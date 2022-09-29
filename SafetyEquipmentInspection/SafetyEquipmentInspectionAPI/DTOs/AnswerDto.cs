namespace SafetyEquipmentInspectionAPI.DTOs
{
    public class AnswerDto
    {
        public Guid AnswerId { get; set; }
        public string EquipmentId { get; set; }
        public int QuestionNumber { get; set; }
        public string Response { get; set; }
    }
}
