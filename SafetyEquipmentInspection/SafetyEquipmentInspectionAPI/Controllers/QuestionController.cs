using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SafetyEquipmentInspectionAPI.Constants;
using SafetyEquipmentInspectionAPI.DTOs;
using SafetyEquipmentInspectionAPI.Interfaces;

namespace SafetyEquipmentInspectionAPI.Controllers
{
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
                var questionCollection = _db.Collection("Questions");
                var questionQuery = await questionCollection.WhereEqualTo("EquipmentType", equipmentType).GetSnapshotAsync();
                foreach (var questionDoc in questionQuery)
                {
                    var question = questionDoc.ConvertTo<QuestionDto>();
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
                QuestionDto questionDto = new QuestionDto
                {
                    QuestionId = Guid.NewGuid().ToString(),
                    EquipmentType = equipmentType,
                    Field = field,
                    QuestionNumber = questionNum
                };
                string message;
                var questionsCollection = _db.Collection("Questions");
                var questionJson = JsonConvert.SerializeObject(questionDto);
                var questionDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(questionJson);
                var questionDoc = await questionsCollection.Document(questionDto.QuestionId).GetSnapshotAsync();
                if (!questionDoc.Exists)
                {
                    await questionsCollection.Document(questionDto.QuestionId).SetAsync(questionDict);
                    message = JsonConvert.SerializeObject(new { addedQuestion = questionDto });
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
                var questionsCollection = _db.Collection("Questions");
                var questiontoBeUpdated = await questionsCollection.Document(questionDto.QuestionId).GetSnapshotAsync();
                if (questiontoBeUpdated.Exists)
                {
                    var updateJson = JsonConvert.SerializeObject(questionDto);
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
                var questionsCollection = _db.Collection("Questions");
                var questiontoBeDeleted = await questionsCollection.Document(questionId).GetSnapshotAsync();
                if (questiontoBeDeleted.Exists)
                {
                    Dictionary<string, object> result = questiontoBeDeleted.ToDictionary();
                    var questionJson = JsonConvert.SerializeObject(result);
                    var questionDataTransferObj = JsonConvert.DeserializeObject<QuestionDto>(questionJson);
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
