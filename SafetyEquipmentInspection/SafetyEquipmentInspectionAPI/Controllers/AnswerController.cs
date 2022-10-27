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
        public async Task InputAnswer(string equipmentId, int questionNum, string response)
        {
            CollectionReference answerCollection = _db.Collection("Answers");
            AnswerDto answer = new AnswerDto
            {
                AnswerId = new Guid(equipmentId),
                EquipmentId = equipmentId,
                QuestionNumber = questionNum,
                Response = response,
                isResponseNo = response.ToLower() == "yes" ? false : true           
            };
            string answerJson = JsonConvert.SerializeObject(answer);
            Dictionary<string, object> answerDict = JsonConvert.DeserializeObject<Dictionary<string, object>>(answerJson);
            await answerCollection.Document(answer.AnswerId.ToString()).SetAsync(answerDict);
        }
    }
}
