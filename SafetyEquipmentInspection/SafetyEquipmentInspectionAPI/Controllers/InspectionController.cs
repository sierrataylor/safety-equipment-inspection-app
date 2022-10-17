using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SafetyEquipmentInspectionAPI.Constants;
using SafetyEquipmentInspectionAPI.DTOs;
using SafetyEquipmentInspectionAPI.Controllers;

namespace SafetyEquipmentInspectionAPI.Controllers
{
    [ApiController]
    public class InspectionController
    {
        public readonly FirestoreDb _db;
        public InspectionController()
        {
            Environment.SetEnvironmentVariable(FirestoreConstants.GoogleApplicationCredentials, FirestoreConstants.GoogleApplicationCredentialsPath);
            _db = FirestoreDb.Create(FirestoreConstants.ProjectId);

        }

        /// <summary>
        /// Generates inspections based on equipment type,
        /// which is taken from the equipment ID.
        /// Equipment ID can be entered manually or returned from
        /// the QR code.
        /// </summary>
        /// <param name="equipmentId">The equipment ID returned from the QR scanner</param>
        /// <returns></returns>
        [HttpPost("inspection/{equipmentId}")]
        public async Task<List<string>> GenerateInspectionForm(string equipmentId)
        {
            var questions = _db.Collection("Questions");
            var equipment = _db.Collection("Equipment");
            //get item by ID
            var equipmentDoc = await equipment.Document(equipmentId).GetSnapshotAsync();
            var equipmentItemObj = equipmentDoc.ConvertTo<EquipmentDto>();
            var equipmentType = equipmentItemObj.EquipmentType;
            //get question by that item's type
            var questionsByEquipmentType = await questions.WhereEqualTo("EquipmentType", equipmentType).GetSnapshotAsync();
            List<string> inspectionFormQuestions = new List<string>();
            //add question from each document to questions list
            foreach (var formQuestion in questionsByEquipmentType.Documents)
            {
                var question = formQuestion.ConvertTo<QuestionDto>();
                inspectionFormQuestions.Add(question.Field);
            }
            return inspectionFormQuestions;
        }

        /// <summary>
        /// HTTP Post for submitted the answers input during an inspection.
        /// Will be performed once the user selects submit.
        /// </summary>
        /// <param name="equipmentId">ID of equipment being inspected</param>
        /// <param name="reviewer">the employee performing the inspection</param>
        /// <param name="responses">array of responses gathered from the form</param>
        /// <returns></returns>
        [HttpPost("/inspection/")]
        public async Task SubmitInspection(string equipmentId, string reviewer, List<string> responses)
        {
            try
            {
                AnswerController answerController = new AnswerController();
                bool hasPassedInspection = false;
                int questionNum = 1;

                var inspectionCollection = _db.Collection("Inspection");
                var equipmentCollection = _db.Collection("Equipment");
                var equipmentBeingInspected = await equipmentCollection.Document(equipmentId).GetSnapshotAsync();

                if (equipmentBeingInspected.Exists)
                {
                    responses = responses.ConvertAll(r => r.ToLower());
                    hasPassedInspection = responses.Any() & responses.Contains("no") ? false : true;
                    foreach (string response in responses)
                    {

                        await answerController.InputAnswer(equipmentId, questionNum++, response);
                    }

                    InspectionDto inspectionDto = new InspectionDto
                    {
                        InspectionId = Guid.NewGuid().ToString(),
                        EquipmentId = equipmentId,
                        PassedInspection = hasPassedInspection,
                        ReviewerId = reviewer,
                        LastInspectionDate = DateTime.UtcNow 
                    };
                    var inspectionDocument = await inspectionCollection.Document(inspectionDto.InspectionId).GetSnapshotAsync();
                    var inspectJson = JsonConvert.SerializeObject(inspectionDto);
                    Dictionary<string, object> inspectionDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(inspectJson);
                    inspectionDictionary["LastInspectionDate"] = Timestamp.FromDateTime(inspectionDto.LastInspectionDate);
                    await inspectionCollection.Document(inspectionDto.InspectionId).SetAsync(inspectionDictionary);
                }

            }
            catch (Exception)
            {

                throw;
            }        
        }

        [HttpGet("inspections/past/{equipmentId}")]
        public async Task<List<InspectionDto>> GetPastInspections(string equipmentId)
        {
            try
            {
                var inspectionCollection = _db.Collection("Inspection");
                List<InspectionDto> pastInspections = new List<InspectionDto>();
                var getInspectionsBasedOnItemIdQuery = await inspectionCollection.WhereEqualTo("EquipmentId", equipmentId).GetSnapshotAsync();
                if (getInspectionsBasedOnItemIdQuery.Any())
                {
                    foreach (var inspectionDoc in getInspectionsBasedOnItemIdQuery.Documents)
                    {
                        var inspection = inspectionDoc.ConvertTo<InspectionDto>();
                        pastInspections.Add(inspection);
                    }
                }
                return pastInspections;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
