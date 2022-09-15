﻿namespace SafetyEquipmentInspectionApp.Models
{
    public class Answer
    {
        public virtual int AnswerId { get; set; }
        public virtual int EquipmentId { get; set; }
        public virtual int QuestionNumber { get; set; }
        public virtual string Response { get; set; }
    }
}
