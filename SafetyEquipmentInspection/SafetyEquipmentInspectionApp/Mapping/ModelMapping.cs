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
                Response = answerDto.Response,
                isResponseNo = answerDto.isResponseNo
            };
            return answer;
        }
        public Inspection MapInspectionDtoToModel(InspectionDto inspectionDto)
        {
            Inspection inspection = new Inspection
            {
                InspectionId = inspectionDto.InspectionId,
                EquipmentId = inspectionDto.EquipmentId,
                PassedInspection = inspectionDto.PassedInspection ? "passed" : "failed",
                ReviewerId = int.Parse(inspectionDto.ReviewerId), 
                LastInspectionDate = inspectionDto.LastInspectionDate
            };
            return inspection;
        }
        public List<Equipment> MapEquipmentDtoListToModels(List<EquipmentDto> equipmentDtos)
        {
            List<Equipment> equipmentModelList = new List<Equipment>();
            foreach (var dto in equipmentDtos)
            {
                Equipment model = MapEquipmentDtoToModel(dto);
                equipmentModelList.Add(model);
            }
            return equipmentModelList;
        }
        public List<Question> MapQuestionDtoListToModels(List<QuestionDto> questionDtos)
        {
            List<Question> questionModelList = new List<Question>();
            foreach (var dto in questionDtos)
            {
                Question model = MapQuestionDtoToModel(dto);
                questionModelList.Add(model);
            }
            return questionModelList;
        }
        public List<Answer> MapAnswerDtoListToModels(List<AnswerDto> answerDtos)
        {
            List<Answer> answerModelList = new List<Answer>();
            foreach (var dto in answerDtos)
            {
                Answer model = MapAnswerDtoToModel(dto);
                answerModelList.Add(model);
            }
            return answerModelList;
        }
        public List<Inspection> MapInspectionDtoListToModels(List<InspectionDto> inspectionDtos)
        {
            List<Inspection> inspectionDtoList = new List<Inspection>();
            foreach (var dto in inspectionDtos)
            {
                Inspection model = MapInspectionDtoToModel(dto);
                inspectionDtoList.Add(model);
            }
            return inspectionDtoList;
        }

    }
}
