using SafetyEquipmentInspectionAPI.DTOs;
using SafetyEquipmentInspectionApp.Models;

namespace SafetyEquipmentInspectionApp.Mapping
{
    public class ModelMapping
    {
        public Equipment MapEquipmentDtoToModel(EquipmentDto eqDto)
        {
            Equipment equipment = new Equipment
            {
                EquipmentID = eqDto.EquipmentId.ToString(),
                EquipmentType = eqDto.EquipmentType,
                Location = eqDto.Location,
                Floor = eqDto.Floor,
                Building = eqDto.Building
            };
            return equipment;
        }
        public Employee MapEmplyeeDtoToModel(EmployeeDto employeeDto)
        {
            Employee employee = new Employee
            {
                EmployeeId = employeeDto.EmployeeId.ToString(),
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Email = employeeDto.Email,
                Role = employeeDto.Role
            };
            return employee;
        }
        public Question MapQuestionDtoToModel(QuestionDto questionDto)
        {
            Question question = new Question
            {
                QuestionId = questionDto.QuestionId,
                EquipmentType = questionDto.EquipmentType,
                QuestionNumber = questionDto.QuestionNumber,
                Field = questionDto.Field
            };
            return question;
        }
        public Answer MapAnswerDtoToModel(AnswerDto answerDto)
        {
            Answer answer = new Answer
            {
                AnswerId = answerDto.AnswerId.ToString(),
                QuestionNumber = answerDto.QuestionNumber,
                EquipmentId = answerDto.EquipmentId,
                Response = answerDto.Response
            };
            return answer;
        }
        public Inspection MapInsepctionDtoToModel(InspectionDto inspectionDto)
        {
            Inspection inspection = new Inspection
            {
                InspectionId = inspectionDto.InspectionId,
                EquipmentId = inspectionDto.EquipmentId,
                InspectionResult = inspectionDto.InspectionResult,
                ReviewerId = int.Parse(inspectionDto.ReviewerId), 
                LastInspectionDate = inspectionDto.LastInspectionDate
            };
            return inspection;
        }
    }
}
