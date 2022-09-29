using SafetyEquipmentInspectionAPI.DTOs;
using SafetyEquipmentInspectionApp.Models;
namespace SafetyEquipmentInspectionApp.Mapping
{
    public class DtoMapping
    {
        public EquipmentDto MapEquipmentModelToDto(Equipment equipment)
        {
            EquipmentDto equipmentDto = new EquipmentDto
            {
                EquipmentId = Guid.Parse(equipment.EquipmentID),
                EquipmentType = equipment.EquipmentType,
                Location = equipment.Location,
                Floor = equipment.Floor,
                Building = equipment.Building
            };
            return equipmentDto;
        }
        public EmployeeDto MapEmployeeModelToDto(Employee employee)
        {
            EmployeeDto employeeDto = new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Role = employee.Role
            };
            return employeeDto;
        }
        public QuestionDto MapQuestionModelToDto(Question question)
        {
            QuestionDto questionDto = new QuestionDto
            {
                QuestionId = question.QuestionId,
                EquipmentType = question.EquipmentType,
                QuestionNumber = question.QuestionNumber,
                Field = question.Field
            };
            return questionDto;
        }
        public AnswerDto MapAnswerModelToDto(Answer answer)
        {
            AnswerDto answerDto = new AnswerDto
            {
                AnswerId = Guid.Parse(answer.AnswerId),
                QuestionNumber = answer.QuestionNumber,
                EquipmentId = answer.EquipmentId,
                Response = answer.Response
            };
            return answerDto;
        }
        public InspectionDto MapInspectionModelToDto(Inspection inspection)
        {
            InspectionDto inspectionDto = new InspectionDto
            {
                InspectionId = inspection.InspectionId,
                EquipmentId = inspection.EquipmentId,
                InspectionResult = inspection.InspectionResult,
                ReviewerId = inspection.ReviewerId.ToString(),
                LastInspectionDate = inspection.LastInspectionDate
            };
            return inspectionDto;
        }
    }
}
