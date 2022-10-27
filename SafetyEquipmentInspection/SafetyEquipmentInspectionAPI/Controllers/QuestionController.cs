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
        readonly JsonSerializerSettings settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }
        };

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
            catch (Exception ex)
            {

                throw new Exception($"The exception {ex.GetBaseException().Message} is being thrown from {ex.TargetSite} in {ex.Source}. Please refer to {ex.HelpLink} to search for this exception.");
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

                    message = JsonConvert.SerializeObject(questionDto, settings:settings);

                }
                else
                {
                    message = $"This question, {field}, already exists for this item. Please add a different questions";
                }
                return message;

            }
            catch (Exception ex)
            {


                return $"The exception {ex.GetBaseException().Message} is being thrown from {ex.TargetSite} in {ex.Source}. Please refer to {ex.HelpLink} to search for this exception.";
            }
        }

        [HttpPut("admin/questions/editQuestion/{questionId}")]

        public async Task<string> UpdateQuestion(string questionId, string equipmentType, int questionNum, string field)
        {
            try
            {
                CollectionReference questionsCollection = _db.Collection("Questions");
                DocumentSnapshot questiontoBeUpdated = await questionsCollection.Document(questionId).GetSnapshotAsync();
                if (questiontoBeUpdated.Exists)
                {
                    QuestionDto questionUpdates = questiontoBeUpdated.ConvertTo<QuestionDto>();
                    questionUpdates.QuestionId = questionId;
                    questionUpdates.EquipmentType = equipmentType;
                    questionUpdates.QuestionNumber = questionNum;
                    questionUpdates.Field = field;
                    string updateJson = JsonConvert.SerializeObject(questionUpdates);
                    Dictionary<string, object> updatesDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(updateJson);
                    await questionsCollection.Document(questionId).UpdateAsync(updatesDictionary);
                    return JsonConvert.SerializeObject(new { message = $"Update of Question {field} with ID {questionId} successfully" });
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
                QuestionDto questionDataTransferObj = questiontoBeDeleted.ConvertTo<QuestionDto>();

                if (questiontoBeDeleted.Exists)
                {
                    await questionsCollection.Document(questionId).DeleteAsync();
                    return $"Question {questionDataTransferObj.Field} for {questionDataTransferObj.EquipmentType} deleted";
                }
                else
                {
                    return $"This question, {questionDataTransferObj.Field}, was not found. It may have already been deleted, or you may have entered an invalid ID.";
                }

            }
            catch (Exception ex)
            {
                return $"The exception {ex.GetBaseException().Message} is being thrown from {ex.TargetSite} in {ex.Source}. Please refer to {ex.HelpLink} to search for this exception.";
            }
        }
    }
}
