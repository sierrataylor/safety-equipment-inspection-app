using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SafetyEquipmentInspectionAPI.Constants;
using SafetyEquipmentInspectionAPI.DTOs;

namespace SafetyEquipmentInspectionAPI.Controllers
{
    [ApiController]
    public class AnswerController
    {
        public readonly FirestoreDb _db;
        public AnswerController()
        {
            Environment.SetEnvironmentVariable(FirestoreConstants.GoogleApplicationCredentials, FirestoreConstants.GoogleApplicationCredentialsPath);
            _db = FirestoreDb.Create(FirestoreConstants.ProjectId);
        }

        [HttpPost("/inspections/answers/{equipmentId}/{questionNumber}")]
        public async Task<string> InputAnswer(string equipmentId, int questionNum, string response)
        {
            var answerCollection = _db.Collection("Answers");
            AnswerDto answer = new AnswerDto
            {
                AnswerId = new Guid(equipmentId),
                EquipmentId = equipmentId,
                QuestionNumber = questionNum,
                Response = response
            };
            var answerJson = JsonConvert.SerializeObject(answer);
            var answerDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(answerJson);
            var newAnswer = await answerCollection.Document(answer.AnswerId.ToString()).SetAsync(answerDict);
            return JsonConvert.SerializeObject(new { addedAnswer = answer }, Formatting.Indented);
        }
    }
}
