using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SafetyEquipmentInspectionAPI.Constants;
using SafetyEquipmentInspectionAPI.DTOs;

namespace SafetyEquipmentInspectionAPI.Controllers
{
    [ApiController]
    public class QuestionController
    {
        public readonly FirestoreDb _db;
        public QuestionController()
        {
            Environment.SetEnvironmentVariable(FirestoreConstants.GoogleApplicationCredentials, FirestoreConstants.GoogleApplicationCredentialsPath);
            _db = FirestoreDb.Create(FirestoreConstants.ProjectId);
        }

        [HttpGet("inspection/{equipmentId}/")]
        public async Task<List<QuestionDto>> GetAllQuestions(string equipmentType)
        {
            try
            {
                List<QuestionDto> questions = new List<QuestionDto>();
                CollectionReference questionCollection = _db.Collection("Questions");
                QuerySnapshot questionQuery = await questionCollection.WhereEqualTo("EquipmentType", equipmentType).GetSnapshotAsync();
                foreach (DocumentSnapshot questionDoc in questionQuery)
                {
                    QuestionDto question = questionDoc.ConvertTo<QuestionDto>();
                    questions.Add(question);
                }
                return questions;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost("admin/questions/")]
        public async Task<string> AddQuestion(string equipmentType, string field, int questionNum)
        {
            try
            {
                QuestionDto questionDto = new()
                {
                    QuestionId = Guid.NewGuid().ToString(),
                    EquipmentType = equipmentType,
                    Field = field,
                    QuestionNumber = questionNum
                };
                string message;
                CollectionReference questionsCollection = _db.Collection("Questions");
                string questionJson = JsonConvert.SerializeObject(questionDto);
                Dictionary<string, object> questionDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(questionJson);
                DocumentSnapshot questionDoc = await questionsCollection.Document(questionDto.QuestionId).GetSnapshotAsync();
                if (!questionDoc.Exists)
                {
                    await questionsCollection.Document(questionDto.QuestionId).SetAsync(questionDict);
                    JsonSerializerSettings settings = new JsonSerializerSettings { 
                        Formatting = Formatting.Indented, 
                        ContractResolver = new DefaultContractResolver { 
                            NamingStrategy = new CamelCaseNamingStrategy() 
                            } 
                        };
                    message = JsonConvert.SerializeObject(questionDto, settings);
                }
                else
                {
                    message = $"This question already exists for this item";
                }
                return message;

            }
            catch (Exception ex)
            {


                return JsonConvert.SerializeObject(new { error = ex.Message });
            }
        }

        [HttpPut("admin/questions/editQuestion/{questionId}")]

        public async Task<string> UpdateQuestion(QuestionDto questionDto)
        {
            try
            {
                CollectionReference questionsCollection = _db.Collection("Questions");
                DocumentSnapshot questiontoBeUpdated = await questionsCollection.Document(questionDto.QuestionId).GetSnapshotAsync();
                if (questiontoBeUpdated.Exists)
                {
                    string updateJson = JsonConvert.SerializeObject(questionDto);
                    Dictionary<string, object> updatesDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(updateJson);
                    await questionsCollection.Document(questionDto.QuestionId).UpdateAsync(updatesDictionary);
                    return JsonConvert.SerializeObject(new { message = $"Update of Question {questionDto.Field} with ID {questionDto.QuestionId} successfully" });
                }
                else
                {
                    return "Question not found";
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }


        [HttpDelete("admin/questions/deleteQuestion/{questionId}")]
        public async Task<string> DeleteQuestion(string questionId, string equipmentId = null)
        {
            try
            {
                CollectionReference questionsCollection = _db.Collection("Questions");
                DocumentSnapshot questiontoBeDeleted = await questionsCollection.Document(questionId).GetSnapshotAsync();
                if (questiontoBeDeleted.Exists)
                {
                    Dictionary<string, object> result = questiontoBeDeleted.ToDictionary();
                    string questionJson = JsonConvert.SerializeObject(result);
                    QuestionDto questionDataTransferObj = JsonConvert.DeserializeObject<QuestionDto>(questionJson);
                    await questionsCollection.Document(questionId).DeleteAsync();
                    return $"Question {questionDataTransferObj.Field} for {questionDataTransferObj.EquipmentType} deleted";
                }
                else
                {
                    return "Question not found";
                }

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
