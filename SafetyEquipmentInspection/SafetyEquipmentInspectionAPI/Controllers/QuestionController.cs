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
        public async Task<string> GetAllQuestions(string equipmentType)
        {
            var questionCollection = _db.Collection("Questions");
            var query = await questionCollection.WhereEqualTo("EquipmentType", equipmentType).GetSnapshotAsync();
            var questions = query.Documents;
            return JsonConvert.SerializeObject(questions);

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

    }
}
