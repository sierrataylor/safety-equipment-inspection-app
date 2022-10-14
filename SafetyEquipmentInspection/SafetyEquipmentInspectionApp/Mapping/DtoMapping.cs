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
                Response = answer.Response,
                isResponseNo = answer.isResponseNo
            };
            return answerDto;
        }
        public InspectionDto MapInspectionModelToDto(Inspection inspection)
        {
            InspectionDto inspectionDto = new InspectionDto
            {
                InspectionId = inspection.InspectionId,
                EquipmentId = inspection.EquipmentId,
                PassedInspection = inspection.PassedInspection.ToLower().Equals("true"), 
                ReviewerId = inspection.ReviewerId.ToString(),
                LastInspectionDate = inspection.LastInspectionDate
            };
            return inspectionDto;
        }

        public List<EquipmentDto> MapEquipmentModelListToDtos(List<Equipment> equipmentModels)
        {
            List<EquipmentDto> equipmentDtoList = new List<EquipmentDto>();
            foreach (var model in equipmentModels)
            {
                EquipmentDto dto = MapEquipmentModelToDto(model);
                equipmentDtoList.Add(dto);
            }
            return equipmentDtoList;
        }
        public List<EmployeeDto> MapEmployeeModelListToDtos(List<Employee> employeeeModels)
        {
            List<EmployeeDto> employeeDtoList = new List<EmployeeDto>();
            foreach (var model in employeeeModels)
            {
                EmployeeDto dto = MapEmployeeModelToDto(model);
                employeeDtoList.Add(dto);
            }
            return employeeDtoList;
        }
        public List<QuestionDto> MapQuestionModelListToDtos(List<Question> questionModels)
        {
            List<QuestionDto> questionDtoList = new List<QuestionDto>();
            foreach (var model in questionModels)
            {
                QuestionDto dto = MapQuestionModelToDto(model);
                questionDtoList.Add(dto);
            }
            return questionDtoList;
        }
        public List<AnswerDto> MapAnswerModelListToDtos(List<Answer> answerModels)
        {
            List<AnswerDto> answerDtoList = new List<AnswerDto>();
            foreach (var model in answerModels)
            {
                AnswerDto dto = MapAnswerModelToDto(model);
                answerDtoList.Add(dto);
            }
            return answerDtoList;
        }
        public List<InspectionDto> MapInspectionModelListToDtos(List<Inspection> inspectionModels)
        {
            List<InspectionDto> inspectionDtoList = new List<InspectionDto>();
            foreach (var model in inspectionModels)
            {
                InspectionDto dto = MapInspectionModelToDto(model);
                inspectionDtoList.Add(dto);
            }
            return inspectionDtoList;
        }
    }
}
