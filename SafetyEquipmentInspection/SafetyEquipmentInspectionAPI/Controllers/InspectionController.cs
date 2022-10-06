using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SafetyEquipmentInspectionAPI.Constants;
using SafetyEquipmentInspectionAPI.DTOs;

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
        /// <param name="equipmentId">The ID of the item being inspected</param>
        /// <param name="reviewer">The person performing the inspection</param>
        /// <param name="inspectionDate">Date of inspection</param>
        /// <param name="answers">All the answers being submitted through the form, passed as a List</param>
        /// <returns></returns>
        [HttpPost("/inspection/")]
        public async Task SubmitInspection(string equipmentId, string reviewer, string inspectionDate, List<AnswerDto> answers)
        {
            bool hasPassedInspection = false;

            string message;
            var inspectionCollection = _db.Collection("Inspection");
            var equipmentCollection = _db.Collection("Equipment");
            var equipmentBeingInspected = await equipmentCollection.Document(equipmentId).GetSnapshotAsync();

            //check if the equipment extist
            if (equipmentBeingInspected.Exists)
            {
                //if answers are correct, boolean is set to true; else, false
                hasPassedInspection = answers.Any(); //WIP

                //create instance of inspection and add to the database
                InspectionDto inspectionDto = new InspectionDto
                {
                    InspectionId = Guid.NewGuid().ToString(),
                    EquipmentId = equipmentId,
                    PassedInspection = hasPassedInspection,
                    ReviewerId = reviewer,
                    LastInspectionDate = DateTime.Parse(inspectionDate)
                };
                var inspectionDocument = await inspectionCollection.Document(inspectionDto.InspectionId).GetSnapshotAsync();
                Dictionary<string, object> inspectionDictionary = inspectionDocument.ToDictionary();
                await inspectionCollection.Document(inspectionDto.InspectionId).SetAsync(inspectionDictionary);
                message = JsonConvert.SerializeObject(inspectionDictionary);
            }
            else
            {
                message = $"Cannot complete this inspection, item not found";
            }
        }
    }
}
