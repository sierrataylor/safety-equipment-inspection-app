namespace SafetyEquipmentInspectionApp.Models
{
    public class Answer
    {
        public int AnswerId { get; set; }
        public int EquipmentId { get; set; }
        public int QuestionNumber { get; set; }
        public string Response { get; set; }
    }
}
