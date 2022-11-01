using Google.Cloud.Firestore;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SafetyEquipmentInspectionAPI.Constants;
using SafetyEquipmentInspectionAPI.DTOs;
using SafetyEquipmentInspectionAPI.Controllers;
using Newtonsoft.Json.Serialization;
using System.Globalization;

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

        readonly JsonSerializerSettings settings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            }
        };
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
            try
            {
                CollectionReference questions = _db.Collection("Questions");
                CollectionReference equipment = _db.Collection("Equipment");
                //get item by ID
                DocumentSnapshot equipmentDoc = await equipment.Document(equipmentId).GetSnapshotAsync();
                EquipmentDto equipmentItemObj = equipmentDoc.ConvertTo<EquipmentDto>();
                string equipmentType = equipmentItemObj.EquipmentType;
                //get question by that item's type
                QuerySnapshot questionsByEquipmentType = await questions.WhereEqualTo("EquipmentType", equipmentType).GetSnapshotAsync();
                List<string> inspectionFormQuestions = new List<string>();
                //add question from each document to questions list
                foreach (DocumentSnapshot formQuestion in questionsByEquipmentType.Documents)
                {
                    QuestionDto question = formQuestion.ConvertTo<QuestionDto>();
                    inspectionFormQuestions.Add(question.Field);
                }
                return inspectionFormQuestions;
            }
            catch (Exception ex)
            {

                throw new Exception($"The exception {ex.GetBaseException().Message} is being thrown from {ex.TargetSite} in {ex.Source}. Please refer to {ex.HelpLink} to search for this exception.");
            }
 
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

                CollectionReference inspectionCollection = _db.Collection("Inspection");
                CollectionReference equipmentCollection = _db.Collection("Equipment");
                DocumentSnapshot equipmentBeingInspected = await equipmentCollection.Document(equipmentId).GetSnapshotAsync();

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
                    DocumentSnapshot inspectionDocument = await inspectionCollection.Document(inspectionDto.InspectionId).GetSnapshotAsync();
                    string inspectJson = JsonConvert.SerializeObject(inspectionDto);
                    Dictionary<string, object> inspectionDictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(inspectJson);
                    inspectionDictionary["LastInspectionDate"] = Timestamp.FromDateTime(inspectionDto.LastInspectionDate);
                    await inspectionCollection.Document(inspectionDto.InspectionId).SetAsync(inspectionDictionary);
                }

            }
            catch (Exception ex)
            {

                throw new Exception($"The exception {ex.GetBaseException().Message} is being thrown from {ex.TargetSite} in {ex.Source}. Please refer to {ex.HelpLink} to search for this exception.");
            }        
        }

        [HttpGet("inspections/past/")]
        public async Task<List<InspectionDto>> GetPastInspections(string equipmentId="")
        {
            try
            {
                CollectionReference inspectionCollection = _db.Collection("Inspection");
                List<InspectionDto> pastInspections = new List<InspectionDto>();
                QuerySnapshot getInspectionsBasedOnItemIdQuery = !String.IsNullOrEmpty(equipmentId) ?
                    await inspectionCollection.WhereEqualTo("EquipmentId", equipmentId).GetSnapshotAsync() :
                    await inspectionCollection.GetSnapshotAsync();
                if (getInspectionsBasedOnItemIdQuery.Any())
                {
                    foreach (DocumentSnapshot inspectionDoc in getInspectionsBasedOnItemIdQuery.Documents)
                    {
                        InspectionDto inspection = inspectionDoc.ConvertTo<InspectionDto>();
                        pastInspections.Add(inspection);
                    }
                }
                return pastInspections;

            }
            catch (Exception ex)
            {

                throw new Exception($"The exception {ex.GetBaseException().Message} is being thrown from {ex.TargetSite} in {ex.Source}. Please refer to {ex.HelpLink} to search for this exception.");
            }
        }
    }
}
